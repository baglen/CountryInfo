using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CountryInfo
{
    public class Manager
    {
        public static Frame baseFrame { get; set; }
        public class Country
        {
            public string Title { get; set; }
            public string CountryCode { get; set; }
            public string Capital { get; set; }
            public decimal Square { get; set; }
            public int Population { get; set; }
            public string Region { get; set; }
        }
    }
}
