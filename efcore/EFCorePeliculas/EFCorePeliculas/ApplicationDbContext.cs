﻿using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.Configuraciones;
using EFCorePeliculas.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCorePeliculas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new GeneroConfig());
            //modelBuilder.ApplyConfiguration(new ActorConfig());
            //modelBuilder.ApplyConfiguration(new CineConfig());
            //modelBuilder.ApplyConfiguration(new SalaDeCineConfig());
            //modelBuilder.ApplyConfiguration(new PeliculaConfig());
            //modelBuilder.ApplyConfiguration(new CineOfertaConfig());
            //modelBuilder.ApplyConfiguration(new PeliculaActorConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingModuloConsulta.Seed(modelBuilder);
            //modelBuilder.Entity<Log>().Property(l=>l.Id).ValueGeneratedNever();
            //modelBuilder.Ignore<Direccion>();
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<SalaDeCine> SalasDeCine { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
