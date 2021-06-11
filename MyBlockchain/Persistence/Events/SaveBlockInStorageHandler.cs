#region

using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Repositories.Interfaces;

#endregion

namespace MyBlockChain.Persistence.Events
{
    public class SaveBlockInStorageHandler : IRequestHandler<SaveBlockInStorageHandler.SaveBlockInStorageCommand, bool>
    {
        private readonly IBlockRepository   _blockRepository;
        private readonly IOutputsRepository _outputsRepository;

        public SaveBlockInStorageHandler(IBlockRepository blockRepository, IOutputsRepository outputsRepository)
        {
            _blockRepository   = blockRepository;
            _outputsRepository = outputsRepository;
        }

        public async Task<bool> Handle(SaveBlockInStorageCommand request, CancellationToken cancellationToken)
        {
            request.Block.Transactions.GetAll()
                   .Execute(transactions => transactions.ForEach(t => t.Inputs.ForEach(i =>
                                                                                       {
                                                                                           _outputsRepository
                                                                                              .Spend(i.OutputId);
                                                                                       })));

            await _blockRepository.Insert(request.Block);
            await _outputsRepository.SaveChange();

            return true;
        }

        public class SaveBlockInStorageCommand : IRequest<bool>
        {
            public SaveBlockInStorageCommand(Block block)
            {
                Block = block;
            }

            public Block Block { get; }
        }
    }
}