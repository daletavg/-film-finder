using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [DataContract]
    public class FilmContent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsFavorit { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public string ReleaseDate { get; set; }
        [DataMember]
        public string[] Actors { set; get; }
        [DataMember]
        public string[] Geners { set; get; }
        [DataMember]
        public string[] Produsers { set; get; }

    }
}