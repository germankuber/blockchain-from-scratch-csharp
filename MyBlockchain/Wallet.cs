using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using EllipticCurve;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain
{
    public class Wallet
    {
        private readonly BlockChain _blockChain;
        private readonly IFeeCalculation _feeCalculation;
        private readonly ITransactionFactory _trsTransactionFactory;

        private readonly List<Output> _unspentTransactionOutputs = new();

        public Wallet(BlockChain blockChain,
            IFeeCalculation feeCalculation,
            ITransactionFactory trsTransactionFactory)
        {
            _blockChain = blockChain;
            _feeCalculation = feeCalculation;
            _trsTransactionFactory = trsTransactionFactory;
        }

        public Amount Amount { get; }
        public Address Address { get; } = new(KeysGenerator.GetPublicKey());
        public string PrivateKey { get; } = KeysGenerator.GetPrivateKey();

        public string Sign(string message)
        {
            return Ecdsa.sign(message,
                EllipticCurve.PrivateKey.fromString(KeysGenerator.GetPrivateKey().ToByte())).toBase64();
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
            if (!HasEnoughAmount(amount))
                return Result.Failure<Transaction>("Does not have enough amount in the wallet");
            return _trsTransactionFactory.Create(this, receiver, amount);
        }

        private bool HasEnoughAmount(Amount amount)
        {
            return _unspentTransactionOutputs.Sum(x => (decimal) x.Amount) > (decimal) Amount;
        }
    }
}