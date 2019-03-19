namespace Server_Films.Film_finder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoritList")]
    public partial class FavoritList
    {
        public int Id { get; set; }

        public int? FilmId { get; set; }

        public int? UserId { get; set; }

        public virtual Film Film { get; set; }

        public virtual User User { get; set; }
    }
}
