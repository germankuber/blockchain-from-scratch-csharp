using System.Collections.Generic;
using CSharpFunctionalExtensions;

using EllipticCurve;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain
{
    public class Wallet
    {
        private readonly BlockChain _blockChain;
        private readonly IFeeCalculation _feeCalculation;
        private readonly ITransactionFactory _trsTransactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;

        private readonly List<Output> _unspentTransactionOutputs = new();

        public Wallet(BlockChain blockChain,
            IFeeCalculation feeCalculation,
            ITransactionFactory trsTransactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool)
        {
            _blockChain = blockChain;
            _feeCalculation = feeCalculation;
            _trsTransactionFactory = trsTransactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            Address = new(KeysGenerator.GetPublicKey());
            PrivateKey = KeysGenerator.GetPrivateKey();
        }
        public Wallet(BlockChain blockChain,
            IFeeCalculation feeCalculation,
            ITransactionFactory trsTransactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool,
            string privateKey)
        {
            _blockChain = blockChain;
            _feeCalculation = feeCalculation;
            _trsTransactionFactory = trsTransactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            var (pk, publicKey) = KeysGenerator.GetPairKey(privateKey);
            Address = new(publicKey);
            PrivateKey = pk;
        }

        public Address Address { get; }
        public string PrivateKey { get; } 

        public SignatureMessage Sign(string message) => SignatureMessage.Sign(message);

        public Result<bool> Verify(string message, string signature, Address address) =>
            Result.SuccessIf(Ecdsa.verify(message,
                    Signature.fromBase64(signature),
                    PublicKey.fromString(address)),
                true,
                "The verification was bad");


        public Result<Transaction> MakeTransaction(Address receiver, Amount amount) =>
            CreateTransaction(receiver, amount);

        private Result<Transaction> CreateTransaction(Address receiver, Amount amount) =>
            !HasEnoughAmount(amount)
                ? Result.Failure<Transaction>("Does not have enough amount in the wallet")
                : _trsTransactionFactory.Create(this, receiver, amount, _blockChain)
                    .Bind(t => _unconfirmedTransactionPool.AddTransactionToPool(t));

        public Amount GetBalance() =>
            _blockChain.GetBalance(Address);
        private bool HasEnoughAmount(Amount amount) =>
            GetBalance() > amount;
    }
}