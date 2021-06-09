﻿using System;
using System.Diagnostics;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain
{
    internal class PowBlockMineStrategy : IBlockMineStrategy
    {
        private const int FirstNonce = 1;
        private readonly string _data;
        private readonly Block _lastBlock;

        public PowBlockMineStrategy(Block lastBlock, string data)
        {
            _lastBlock = lastBlock;
            _data = data;
        }

        public Block Mine(Func<IBlockMineStrategy.BlockData, Block> createBlock)
        {
            return MineBlock(DateTime.Now.TimeOfDay,
                _lastBlock.Header.LastHash,
                _data,
                FirstNonce,
                BlockChainConfig.GetActualDifficulty(),
                createBlock);
        }

        public string GetBlockHash(Block block)
        {
            return CreateHash(block.Header.TimeSpan,
                block.Header.LastHash,
                block.Header.Data,
                block.Header.Nonce,
                block.Header.Difficulty);
        }

        private Block MineBlock(TimeSpan timeSpan,
            string lastHash,
            string data,
            int nonce,
            int difficulty,
            Func<IBlockMineStrategy.BlockData, Block> createBlock)
        {
            string actualHash;
            var watch = Stopwatch.StartNew();
            do
            {
                actualHash = CreateHash(timeSpan, lastHash, data, nonce, difficulty);
                ++nonce;
            } while (actualHash.Substring(0, difficulty) != string.Concat(Enumerable.Repeat("0", difficulty)));

            watch.Stop();
            BlockChainConfig.ActualMineTime(watch.Elapsed);
            Console.WriteLine(
                $"Minado - Tardo : {watch.Elapsed} - Blocke : {actualHash} - Difficulty : {BlockChainConfig.GetActualDifficulty()}");
            return createBlock(
                new IBlockMineStrategy.BlockData(timeSpan, lastHash, actualHash, data, nonce, difficulty));
        }

        private static string CreateHash(TimeSpan timeSpan, string lastHash, string data, int nonce, int difficulty)
        {
            return HashUtilities.Hash(new
            {
                TimeSpan = timeSpan,
                LastHash = lastHash,
                Data = data,
                Difficulty = difficulty,
                Nonce = nonce
            });
        }
    }
}