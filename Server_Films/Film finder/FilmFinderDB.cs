namespace Server_Films
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FilmFinderDB : DbContext
    {
        public FilmFinderDB()
            : base("name=FilmFinderDB")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<ActorToFilm> ActorToFilms { get; set; }
        public virtual DbSet<Coment> Coments { get; set; }
        public virtual DbSet<FavoritList> FavoritLists { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenreToFilm> GenreToFilms { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ProdusserToFilm> ProdusserToFilms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WaitingList> WaitingLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasMany(e => e.ProdusserToFilms)
                .WithOptional(e => e.Producer)
                .HasForeignKey(e => e.ProdusserID);
        }
    }
}
