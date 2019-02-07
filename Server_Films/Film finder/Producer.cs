namespace Server_Films
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producer")]
    public partial class Producer
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int Surname { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }
    }
}
