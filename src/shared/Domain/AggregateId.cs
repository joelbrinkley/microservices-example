using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates
{
    public class AggregateId<T>
    {
        private T value;
        public T Value => value;

        public AggregateId(T value)
        {
            this.value = value;
        }

        public static implicit operator AggregateId<T>(T value)
        {
            return new AggregateId<T>(value);
        }
    }
}
