﻿namespace MyBlockChain.Transactions.InputsOutputs.Scripts
{
    public interface IScriptBlock
    {
        bool Excecute(string privateKey);
    }
}