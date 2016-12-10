using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Lib
{
    public class DataEqualityComparer : IEqualityComparer<Data>
    {
        public bool Equals(Data x, Data y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Data obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}