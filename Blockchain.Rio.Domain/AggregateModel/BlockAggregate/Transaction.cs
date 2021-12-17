using Blockchain.Rio.Shared.SeedWork;
using System;

namespace Blockchain.Rio.Domain.AggregateModel.BlockAggregate
{
    public class Transaction: Entity
    {
        public Transaction() { }
        public Transaction(string from, string to, string value)
        {
            From = from;
            To = to;
            Value = value;
        }
        public string From { get; set; }
        public string To { get; set; }
        public string Value { get; set; }
        public Block Block { get; set; }
        public Guid BlockId { get; set; }
    }
}
