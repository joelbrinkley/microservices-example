using System;
using System.Collections.Generic;
using System.Text;

namespace Customers
{
    public class Name
    {
        public string First { get; }
        public string Middle { get; }
        public string Last { get; }

        public Name(string first, string middle, string last)
        {
            First = first ?? "";
            Middle = middle ?? "";
            Last = last ?? "";
        }

        public string FullName => $"{this.First} {this.Middle} {this.Last}";
        public string FirstAndLast => $"{this.First} {this.Last}";

        public override bool Equals(object other)
        {
            return this.Equals(other as Name);
        }

        public bool Equals(Name other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            if (this.GetType() != other.GetType()) return false;

            return (string.Equals(First, other.First, StringComparison.CurrentCultureIgnoreCase))
                && (string.Equals(Middle, other.Middle, StringComparison.CurrentCultureIgnoreCase))
                && (string.Equals(Last, other.Last, StringComparison.CurrentCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // Maybe nullity checks, if these are objects not primitives!
                hash = hash * 17 + First.GetHashCode();
                hash = hash * 17 + Middle.GetHashCode();
                hash = hash * 17 + Last.GetHashCode();
                return hash;
            }
        }
    }
}
