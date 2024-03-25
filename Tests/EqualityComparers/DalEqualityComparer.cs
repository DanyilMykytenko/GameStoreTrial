using Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.EqualityComparers;

public class DalEqualityComparer
{
    internal class GameEqualityComparer : IEqualityComparer<Game>
    {
        public bool Equals([AllowNull] Game x, [AllowNull] Game y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Key == y.Key
                && x.Description == y.Description
                && x.IsDeleted == y.IsDeleted;
        }

        public int GetHashCode([DisallowNull] Game obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class GenreEqualityComparer : IEqualityComparer<Genre>
    {
        public bool Equals([AllowNull] Genre x, [AllowNull] Genre y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.ParentId == y.ParentId;
        }

        public int GetHashCode([DisallowNull] Genre obj)
        {
            return obj.GetHashCode();
        }
        internal class PlatformEqualityComparer : IEqualityComparer<Platform>
        {
            public bool Equals([AllowNull] Platform x, [AllowNull] Platform y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;

                return x.Id == y.Id
                    && x.Type == y.Type;
            }

            public int GetHashCode([DisallowNull] Platform obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
