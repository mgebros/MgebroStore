using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgebroStore.Models.Error
{
    public class GeneralErrorViewModel : Exception
    {
        public string Text { get; set; }
    }
}
