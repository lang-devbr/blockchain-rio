using Blockchain.Rio.App.Miner;
using System;

namespace Blockchain.Rio.App
{
    public class Startup
    {
        private readonly IBlockMiner blockMiner;

        public Startup(IBlockMiner blockMiner)
        {
            this.blockMiner = blockMiner;
        }

        public void Run()
        {
            blockMiner.Start();
            Console.ReadKey();
            blockMiner.Stop();
        }
    }
}
