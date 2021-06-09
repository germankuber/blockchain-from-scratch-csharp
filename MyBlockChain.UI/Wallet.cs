using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using CSharpFunctionalExtensions;

using MyBlockChain.Blocks;
using MyBlockChain.Persistence;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain.UI
{
    public partial class Wallet : Form
    {
        private List<Block> _blocks = new();
        private readonly BlockChain _blockChain;
        private readonly ITransactionFactory _transactionFactory;
        private readonly PowBlockMineStrategy _powBlockMineStrategy = new();
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool
            = new UnconfirmedTransactionPool(new ValidateTransaction());

        private readonly IFeeCalculation _feeCalculation = new FeeCalculation();
        private readonly IScriptBlockFactory _scriptBlockFactory
            = new ScriptBlockFactory();
        private readonly ITransactionIdStrategy _transactionIdStrategy
            = new CalculateTransactionIdStrategy();
        private readonly BlockStorage _blocksStorage;
        private readonly IWalletStorage _walletStorage;
        private Miner _miner;
        private MyBlockChain.Wallet _wallet;
        private List<MyBlockChain.Wallet> _wallets;

        public Wallet()
        {
            InitializeComponent();

            _blockChain = new BlockChain(new BlockStorage(null));

            _transactionFactory = new TransactionFactory(new ValidateTransaction(),
                new CalculateTransactionIdStrategy(),
                new CalculateInputs(_blockChain),
                new CalculateOutputs(_blockChain, new ScriptBlockFactory(), new FeeCalculation()),
                new ScriptBlockFactory());

            _blocksStorage = new BlockStorage(new StorageParser(_transactionFactory,
                _unconfirmedTransactionPool,
                _transactionIdStrategy,
                _scriptBlockFactory));
            _walletStorage = new WalletStorage(_feeCalculation,
                _transactionFactory,
                _unconfirmedTransactionPool);

            LoadBlocks();
            LoadWallets();
            CheckVisible();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private MyBlockChain.Wallet CreateWallet() =>
            new(_blockChain, new FeeCalculation(),
                _transactionFactory,
                _unconfirmedTransactionPool);
        private MyBlockChain.Wallet CreateWallet(string privateKey) =>
            new(_blockChain, new FeeCalculation(),
                _transactionFactory,
                _unconfirmedTransactionPool,
                privateKey);

        private void button1_Click(object sender, EventArgs e)
        {
            var wallet = CreateWallet();
            _walletStorage.Insert(wallet);
            _wallet = wallet;
            SetWalletData();
        }

        private void SetWalletData()
        {
            LblWalletAddress.Text = _wallet.Address;
            LblPrivateKey.Text = _wallet.PrivateKey;
            LblBalance.Text = _wallet.GetBalance();
            CheckVisible();
        }

        private void LoadBlocks() =>
            DataGridBlocks.DataSource = _blocksStorage.GetAll(_blockChain).Select(b =>
                new
                {
                    Hash = b.Header.Hash,
                    TimeSpan = b.Header.TimeSpan,
                    TransactionsCount = b.Transactions.GetAll().ToList().Count
                }).ToList();

        private void LoadWallets()
        {
            _wallets = _walletStorage.GetAll(_blockChain);
            _wallets.ForEach(x => listView2.Items
                    .Add(x.Address, x.PrivateKey));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _miner = new Miner(_blockChain,
                _wallet,
                _unconfirmedTransactionPool,
                _powBlockMineStrategy,
                _transactionFactory);
            var block = _miner.Mine();
            LoadBlocks();
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
    }
}
