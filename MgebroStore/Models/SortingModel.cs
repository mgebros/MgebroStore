using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models
{
    public class SortingModel
    {
        public string Name { get; set; }
        public string Name_Desc
        {
            get
            {
                return Name + "_Desc";
            }
        }
    }
}
