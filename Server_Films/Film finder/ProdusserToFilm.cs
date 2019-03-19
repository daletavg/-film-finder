namespace Server_Films.Film_finder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProdusserToFilm")]
    public partial class ProdusserToFilm
    {
        public int Id { get; set; }

        public int? FilmID { get; set; }

        public int? ProdusserID { get; set; }

        public virtual Film Film { get; set; }

        public virtual Producer Producer { get; set; }
    }
}
