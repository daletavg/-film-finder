namespace Server_Films
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coment")]
    public partial class Coment
    {
        public int Id { get; set; }

        public int? FilmId { get; set; }

        public int? UserId { get; set; }

        public string Сommentary { get; set; }

        public virtual Film Film { get; set; }

        public virtual User User { get; set; }
    }
}
