#region

using CSharpFunctionalExtensions;
using EllipticCurve;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.MemoryPool;

#endregion

namespace MyBlockChain
{
    public class Wallet
    {
        private readonly BlockChain                  _blockChain;
        private readonly IFeeCalculation             _feeCalculation;
        private readonly IOutputsRepository          _outputsRepository;
        private readonly ITransactionFactory         _trsTransactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;


        public Wallet(BlockChain                  blockChain,
                      IFeeCalculation             feeCalculation,
                      ITransactionFactory         trsTransactionFactory,
                      IUnconfirmedTransactionPool unconfirmedTransactionPool,
                      IOutputsRepository          outputsRepository)
        {
            _blockChain                 = blockChain;
            _feeCalculation             = feeCalculation;
            _trsTransactionFactory      = trsTransactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _outputsRepository          = outputsRepository;
            Address                     = new Address(KeysGenerator.GetPublicKey());
            PrivateKey                  = KeysGenerator.GetPrivateKey();
        }

        public Wallet(BlockChain                  blockChain,
                      IFeeCalculation             feeCalculation,
                      ITransactionFactory         trsTransactionFactory,
                      IUnconfirmedTransactionPool unconfirmedTransactionPool,
                      IOutputsRepository          outputsRepository,
                      string                      privateKey)
        {
            _blockChain                 = blockChain;
            _feeCalculation             = feeCalculation;
            _trsTransactionFactory      = trsTransactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _outputsRepository          = outputsRepository;
            var (pk, publicKey)         = KeysGenerator.GetPairKey(privateKey);
            Address                     = new Address(publicKey);
            PrivateKey                  = pk;
        }

        public Address Address    { get; }
        public string  PrivateKey { get; }

        public SignatureMessage Sign(string message)
        {
            return SignatureMessage.Sign(message);
        }

        public Result<bool> Verify(string message, string signature, Address address)
        {
            return Result.SuccessIf(Ecdsa.verify(message,
                                                 Signature.fromBase64(signature),
                                                 PublicKey.fromString(address)),
                                    true,
                                    "The verification was bad");
        }


        public Result<Transaction> MakeTransaction(Address receiver, Amount amount)
        {
            return CreateTransaction(receiver, amount);
        }

        private Result<Transaction> CreateTransaction(Address receiver, Amount amount)
        {
            return !HasEnoughAmount(amount)
                       ? Result.Failure<Transaction>("Does not have enough amount in the wallet")
                       : _trsTransactionFactory.Create(this, receiver, amount, _blockChain)
                                               .Bind(t => _unconfirmedTransactionPool.AddTransactionToPool(t));
        }

        public Amount GetBalance()
        {
            return _outputsRepository.GetBalance(Address);
        }

        private bool HasEnoughAmount(Amount amount)
        {
            return GetBalance() > amount;
        }
    }
}