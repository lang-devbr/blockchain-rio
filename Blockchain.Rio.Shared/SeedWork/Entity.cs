﻿using System;

namespace Blockchain.Rio.Shared.SeedWork
{
    public abstract class Entity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        private int? _requestedHashCode;
        public Guid Id { get; set; }
        
        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;
            var item = (Entity)obj;
            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
