namespace Server_Films
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Genre")]
    public partial class Genre
    {
        public int Id { get; set; }

        public string Genr { get; set; }

        public int? FilmId { get; set; }

        public virtual Film Film { get; set; }
    }
}
