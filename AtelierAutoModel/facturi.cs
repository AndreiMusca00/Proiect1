namespace AtelierAutoModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("facturi")]
    public partial class facturi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_factura { get; set; }

        public int id_client { get; set; }

        public double valoare { get; set; }

        [Required]
        [StringLength(100)]
        public string descriere { get; set; }

        [Column(TypeName = "date")]
        public DateTime data { get; set; }

        public virtual clienti clienti { get; set; }
    }
}
