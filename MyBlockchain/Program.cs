using System;
using System.Linq;
using MyBlockChain.Blocks;

namespace MyBlockChain
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var blockChain = new BlockChain();

            //var wallet1 = new Wallet(blockChain, null);
            //var wallet2 = new Wallet(blockChain, null);

            //var signature = wallet1.Sign("hola como andas");


            //var rr = wallet2.Verify("hola como andas",signature, wallet1.Address);


            //Enumerable.Range(1, 20).Aggregate(Block.Genesis(),
            //    (block, i) =>
            //    {
            //        var newBlock = Block.Mine(new PowBlockMineStrategy(block, i.ToString()));
            //        blockChain.AddBlock(newBlock);
            //        return newBlock;
            //    });


            // Generate new Keys

            Console.ReadKey();
        }
    }
}