#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpFunctionalExtensions;
using MediatR;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;

#endregion

namespace MyBlockChain.UI
{
    public partial class Wallet : Form
    {
        private readonly BlockChain         _blockChain;
        private readonly IBlockMineStrategy _blockMineStrategy;
        private readonly IBlockRepository   _blocksRepository;

        private readonly IFeeCalculation             _feeCalculation;
        private readonly IMediator                   _mediator;
        private readonly IOutputsRepository          _outputsRepository;
        private readonly IScriptBlockFactory         _scriptBlockFactory;
        private readonly ITransactionFactory         _transactionFactory;
        private readonly ITransactionIdStrategy      _transactionIdStrategy;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;
        private readonly IWalletStorage              _walletStorage;
        private          List<Block>                 _blocks = new();
        private          Miner                       _miner;
        private          MyBlockChain.Wallet         _wallet;
        private          MyBlockChain.Wallet         _wallet2;
        private          MyBlockChain.Wallet         _wallet3;
        private          List<MyBlockChain.Wallet>   _wallets;

        public Wallet(IFeeCalculation             feeCalculation,
                      ITransactionFactory         transactionFactory,
                      IBlockMineStrategy          blockMineStrategy,
                      IUnconfirmedTransactionPool unconfirmedTransactionPool,
                      IScriptBlockFactory         scriptBlockFactory,
                      ITransactionIdStrategy      transactionIdStrategy,
                      IBlockRepository            blocksRepository,
                      IWalletStorage              walletStorage,
                      IMediator                   mediator,
                      IOutputsRepository          outputsRepository,
                      IStorageParser              storageParser)
        {
            _feeCalculation             = feeCalculation;
            _transactionFactory         = transactionFactory;
            _blockMineStrategy          = blockMineStrategy;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _scriptBlockFactory         = scriptBlockFactory;
            _transactionIdStrategy      = transactionIdStrategy;
            _blocksRepository           = blocksRepository;
            _walletStorage              = walletStorage;
            _mediator                   = mediator;
            _outputsRepository          = outputsRepository;
            InitializeComponent();

            _blockChain = new BlockChain(mediator);


            LoadBlocks();
            LoadWallets();
            CheckVisible();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private MyBlockChain.Wallet CreateWallet()
        {
            return new(_blockChain, new FeeCalculation(),
                       _transactionFactory,
                       _unconfirmedTransactionPool,
                       _outputsRepository);
        }

        private MyBlockChain.Wallet CreateWallet(string privateKey)
        {
            return new(_blockChain, new FeeCalculation(),
                       _transactionFactory,
                       _unconfirmedTransactionPool,
                       _outputsRepository,
                       privateKey);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var wallet = CreateWallet();
            _walletStorage.Insert(wallet);
            LoadWallets();
        }

        private void SetWalletData()
        {
            LblBalance.Text  = _wallet != null ? _wallet.GetBalance() : "";
            LblBalance2.Text = _wallet2 != null ? _wallet2.GetBalance() : "";
            LblBalance3.Text = _wallet3 != null ? _wallet3.GetBalance() : "";
            CheckVisible();
        }

        private void LoadBlocks()
        {
            DataGridBlocks.DataSource = _blocksRepository.GetAll(_blockChain).OrderByDescending(x => x.Header.TimeSpan)
                                                         .Select(b =>
                                                                     new
                                                                     {
                                                                         b.Header.Hash,
                                                                         b.Header.TimeSpan,
                                                                         TransactionsCount = b.Transactions.GetAll()
                                                                                              .ToList().Count,
                                                                         b.Header.Difficulty
                                                                     }).ToList();
        }

        private void LoadWallets()
        {
            _wallets = _walletStorage.GetAll(_blockChain);

            _wallets.ForEach(x =>
                             {
                                 listView2.Items
                                          .Add(x.Address, x.PrivateKey);
                                 listView1.Items
                                          .Add(x.Address, x.PrivateKey);
                                 listView3.Items
                                          .Add(x.Address, x.PrivateKey);
                             });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            await Mine(_wallet);
            button2.Enabled = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            await Mine(_wallet3);
            button2.Enabled = true;
        }

        private async Task Mine(MyBlockChain.Wallet wallet)
        {
            _miner = new Miner(_blockChain,
                               wallet,
                               _unconfirmedTransactionPool,
                               _blockMineStrategy,
                               _transactionFactory);
            var block = await _miner.Mine();
            LoadBlocks();
            SetWalletData();
        }

        private void CheckVisible()
        {
            if (_wallet == null)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                var publicKeySelected = listView2.SelectedItems[0].Text;
                _wallet = CreateWallet(_wallets.First(x => x.Address == publicKeySelected).PrivateKey);
                SetWalletData();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var publicKeySelected = listView1.SelectedItems[0].Text;
                var tmpWallet2        = CreateWallet(_wallets.First(x => x.Address == publicKeySelected).PrivateKey);
                if (_wallet == tmpWallet2)
                {
                    MessageBox.Show("You can't select the same wallet");
                }
                else
                {
                    _wallet2 = tmpWallet2;
                    SetWalletData();
                }
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            await Mine(_wallet2);
            button1.Enabled = true;
        }

        private void BtnToWallet1_Click(object sender, EventArgs e)
        {
            _wallet2.MakeTransaction(_wallet.Address, Amount.Create(int.Parse(TxtTransfer.Text)));
        }

        private void BtnToWallet2_Click(object sender, EventArgs e)
        {
            _wallet.MakeTransaction(_wallet2.Address, Amount.Create(int.Parse(TxtTransfer.Text)));
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 1)
            {
                var publicKeySelected = listView3.SelectedItems[0].Text;
                _wallet3 = CreateWallet(_wallets.First(x => x.Address == publicKeySelected).PrivateKey);
                SetWalletData();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetWalletData();
            LoadBlocks();
        }
    }
}