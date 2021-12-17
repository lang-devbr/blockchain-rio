using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Blockchain.Rio.Shared.SeedWork;
using System;
using System.Collections.Generic;

namespace Blockchain.Rio.Domain.AggregateModel.BlockAggregate
{
    public class Block: Entity, IAggregateRoot
    {
        public long Index { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
        public long Nounce { get; set; }
        public List<Transaction> TransactionList { get; set; }
    }
}
