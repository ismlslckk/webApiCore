using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore
{
    public class Deneme2 : IDeneme<Deneme2>
    {
        private  int sayac = 0;
        public string yap()
        {
            sayac++;
            return "bbb"+sayac;
        }
    }
}
