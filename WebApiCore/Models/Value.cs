using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace WebApiCore.Models
{
    public class Value
    {
        public Value()
        {
            this.TwoTables=new HashSet<TwoTable>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }

        public ICollection<TwoTable> TwoTables { get; set; }

    }
}
