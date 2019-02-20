using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [DataContract]
    public class AllSpecificAddingFilm
    {
        [DataMember]
        public string[] Actors { set; get; }
        [DataMember]
        public string[] Geners { set; get; }
        [DataMember]
        public string[] Produsers { set; get; }
    }
}
