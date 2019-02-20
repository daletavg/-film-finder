namespace Server_Films
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GenreToFilm")]
    public partial class GenreToFilm
    {
        public int Id { get; set; }

        public int? FilmID { get; set; }

        public int? GenreID { get; set; }

        public virtual Film Film { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
