using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore
{
    public class Deneme:IDeneme<Deneme>
    {
        public string yap()
        {
            return "aaax";
        }
    }
}
