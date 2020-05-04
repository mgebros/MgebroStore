using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models
{
    public abstract class IPagination
    {
        public int Pagesize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
