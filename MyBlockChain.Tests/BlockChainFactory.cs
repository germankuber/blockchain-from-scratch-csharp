#region

using MediatR;
using MyBlockChain.Blocks;

#endregion

namespace MyBlockChain.Tests
{
    public class BlockChainFactory : IBlockChainFactory
    {
        private readonly IMediator _mediator;

        public BlockChainFactory(IMediator mediator)
        {
            _mediator = mediator;
        }

        public BlockChain Create()
        {
            return new(_mediator);
        }
    }
}