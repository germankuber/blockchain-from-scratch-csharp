using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
        }

        public Wallet(BlockChain blockChain,
            IFeeCalculation feeCalculation,
            ITransactionFactory trsTransactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool,
            Amount initialAmount)
        {
            _blockChain = blockChain;
            _feeCalculation = feeCalculation;
            _trsTransactionFactory = trsTransactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
        }


        public Address Address { get; } = new(KeysGenerator.GetPublicKey());
        public string PrivateKey { get; } = KeysGenerator.GetPrivateKey();

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