using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using System.Collections.Generic;

namespace Blockchain.Rio.App.Miner
{
    public interface IBlockMiner
    {
        List<Block> Blockchain { get; }

        void Start();

        void Stop();
    }
}
