namespace Server_Films.Film_finder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaitingList")]
    public partial class WaitingList
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? FilmId { get; set; }

        public int? PriorityId { get; set; }

        public virtual Film Film { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual User User { get; set; }
    }
}
