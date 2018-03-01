using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Policies
{
    public interface IPolicy<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}
