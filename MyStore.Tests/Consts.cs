using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests
{
    public static class Consts
    {
        public static int CategoryId = 2;
        public static int TestProduct = 3;
        public static int TestSupplierId = 4;
        public static decimal TestUnitProce = 100.23M;
        public const string ProductName = "Test Product name 1";
        public enum Categories
        {
            Condiments = 2,
            Confections = 3,
          Dairy,
          Grains,
          Meats
        }
    }
}
