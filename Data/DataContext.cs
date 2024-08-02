using System.Collections.Generic;
using SopalS.Models.ConteneurModel;

using Microsoft.EntityFrameworkCore;
using SopalS.Models;

namespace SopalS.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Conteneur> Conteneur { get; set; }
        public DbSet<HistoEtalonnage> HistoEtalonnages { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Emplacement> Emplacement { get; set; }
        public DbSet<ConteneurViewModel> Conteneurs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conteneur>()
                .HasKey(c => c.Ref);

            modelBuilder.Entity<Conteneur>()
                .Property(c => c.Ref)
                .ValueGeneratedOnAdd();  // Assurez-vous que cette ligne est présente pour auto-incrémenter la clé primaire
            modelBuilder.Entity<Conteneur>()
           .HasOne(c => c.Emplacement)
           .WithMany()
           .HasForeignKey(c => c.EmplacementId);

            modelBuilder.Entity<HistoEtalonnage>()
                .HasKey(h => new { h.Ref, h.Date });

            modelBuilder.Entity<HistoEtalonnage>()
                .HasOne(h => h.Conteneur)
                .WithMany(c => c.HistoEtalonnages)
                .HasForeignKey(h => h.Ref);
            modelBuilder.Entity<ConteneurViewModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("V_conteneurs");
            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
