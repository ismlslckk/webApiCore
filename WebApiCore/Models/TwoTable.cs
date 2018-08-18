using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WebApiCore.Models
{
    public class TwoTable
    {
        public int Id { get; set; }
        public string Alan1 { get; set; }
        public string Alan2 { get; set; }

        public int? ValueId { get; set; }

        //[JsonIgnore] property bazında ignore etmek için kullanılır.
        //[IgnoreDataMember] xml formatında ignore etmek için kullanılır.
        [IgnoreDataMember]
        public virtual Value Value { get; set; }

    }
}
