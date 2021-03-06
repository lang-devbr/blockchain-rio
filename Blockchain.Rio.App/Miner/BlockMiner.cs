using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Blockchain.Rio.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain.Rio.App.Miner
{
    public class BlockMiner : IBlockMiner
    {
        private static int MINING_PERIOD = 1000;

        private readonly TransactionPool transactionPool;
        private readonly ILogger<BlockMiner> logger;

        private readonly string nodeName;

        public List<Block> Blockchain { get; private set; }
        private CancellationTokenSource cancellationToken;

        public BlockMiner(TransactionPool transactionPool, ILoggerFactory loggerFactory, IConfigurationRoot config)
        {
            Blockchain = new List<Block>();
            this.transactionPool = transactionPool;
            logger = loggerFactory.CreateLogger<BlockMiner>();
            nodeName = config.GetSection("NodeName").Get<string>();
        }

        public void Start()
        {
            cancellationToken = new CancellationTokenSource();
            Task.Run(() => DoGenerateBlock(), cancellationToken.Token);
            logger.LogInformation("Mining has started");
        }

        public void Stop()
        {
            cancellationToken.Cancel();
            logger.LogInformation("Mining has stopped");
        }

        private void DoGenerateBlock()
        {
            while (true)
            {
                var startTime = DateTime.Now.Millisecond;
                GenerateBlock();
                var endTime = DateTime.Now.Millisecond;
                var remainTime = MINING_PERIOD - (endTime - startTime);
                Thread.Sleep(remainTime < 0 ? 0 : remainTime);
                Console.WriteLine($"################ Mining ############### - {DateTime.Now.ToUniversalTime() }");
            }
        }
        private void GenerateBlock()
        {
            var lastBlock = Blockchain.LastOrDefault();
            var transactionList = transactionPool.TakeAll();
            if (!transactionList.Any()) return;
            var block = new Block()
            {
                TimeStamp = DateTime.Now,
                Nounce = 0,
                TransactionList = transactionList,
                Index = (lastBlock?.Index + 1 ?? 0),
                PrevHash = lastBlock?.Hash ?? string.Empty
            };
            MineBlock(block);
            Blockchain.Add(block);

            Console.WriteLine("############ New block ############");
            Console.WriteLine(JsonSerializer.Serialize(block));
            Console.WriteLine("###################################");
        }

        private void MineBlock(Block block)
        {
            var merkleRootHash = FindMerkleRootHash(block.TransactionList);
            long nounce = -1;
            var hash = string.Empty;
            do
            {
                nounce++;
                var rowData = block.Index + block.PrevHash + block.TimeStamp.ToString() + nounce + merkleRootHash;
                hash = CalculateHash(CalculateHash(rowData));
            }
            while (!hash.StartsWith("0000"));
            block.Hash = hash;
            block.Nounce = nounce;
        }

        private string FindMerkleRootHash(IList<Transaction> transactionList)
        {
            var transactionStrList = transactionList.Select(tran => CalculateHash(CalculateHash(tran.From + tran.To + tran.Value))).ToList();
            return BuildMerkleRootHash(transactionStrList);
        }

        private string BuildMerkleRootHash(IList<string> merkelLeaves)
        {
            if (merkelLeaves == null || !merkelLeaves.Any())
                return string.Empty;

            if (merkelLeaves.Count() == 1)
                return merkelLeaves.First();

            if (merkelLeaves.Count() % 2 > 0)
                merkelLeaves.Add(merkelLeaves.Last());

            var merkleBranches = new List<string>();

            for (int i = 0; i < merkelLeaves.Count(); i += 2)
            {
                var leafPair = string.Concat(merkelLeaves[i], merkelLeaves[i + 1]);
                merkleBranches.Add(CalculateHash(CalculateHash(leafPair)));
            }
            return BuildMerkleRootHash(merkleBranches);
        }

        private static string CalculateHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
