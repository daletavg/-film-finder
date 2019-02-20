namespace Server_Films
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActorToFilm")]
    public partial class ActorToFilm
    {
        public int id { get; set; }

        public int? FilmID { get; set; }

        public int? ActorID { get; set; }

        public virtual Actor Actor { get; set; }

        public virtual Film Film { get; set; }
    }
}
