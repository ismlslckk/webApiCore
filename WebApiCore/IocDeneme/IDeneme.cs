using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore
{
    public interface IDeneme<T> where T : class
    {
        string yap();
    }
}
