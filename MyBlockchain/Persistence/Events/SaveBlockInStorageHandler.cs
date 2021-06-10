using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Repositories.Interfaces;

namespace MyBlockChain.Persistence.Events
{
    public class SaveBlockInStorageHandler : IRequestHandler<SaveBlockInStorageHandler.SaveBlockInStorageCommand, bool>
    {
        private readonly IBlockRepository _blockRepository;

        public SaveBlockInStorageHandler(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public async Task<bool> Handle(SaveBlockInStorageCommand request, CancellationToken cancellationToken)
        {
            await _blockRepository.Insert(request.Block);
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