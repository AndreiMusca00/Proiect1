using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AtelierAutoModel
{
    public partial class AtelierAutoEntitiesModel : DbContext
    {
        public AtelierAutoEntitiesModel()
            : base("name=AtelierAutoEntitiesModel")
        {
        }

        public virtual DbSet<clienti> clientis { get; set; }
        public virtual DbSet<facturi> facturis { get; set; }
        public virtual DbSet<programari> programaris { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<specializari_mecanici> specializari_mecanici { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
