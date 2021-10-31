using System;
using Microsoft.EntityFrameworkCore;
using PWP.Domain;

namespace DbMigrations
{
    public class DbCtx:DbContext
    {
        //public DbCtx(DbContextOptions<DbCtx> options)
        //     : base(options)
        //{

        //}
        public DbSet<Diatari> Diatari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PWP;User Id=sa;Password=@thisIsAp0c",
                    x => x.UseNetTopologySuite());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Diatari>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Agenda).IsRequired().HasMaxLength(10);
                entity.Property(e => e.DataInici).IsRequired().HasColumnType("date");
                entity.Property(e => e.HoraInici).IsRequired().HasColumnType("time");
                entity.Property(e => e.HoraFi).IsRequired().HasColumnType("time");

                entity.Property(e => e.Minuts).IsRequired();
                entity.Property(e => e.PuntInici).IsRequired().HasColumnType("geometry");
                entity.Property(e => e.PuntFi).IsRequired().HasColumnType("geometry"); ;
               



                entity.HasIndex(i => i.DataInici);
               

                entity.ToTable("Diatari", "Agendes");

            });

        }
    }
}
