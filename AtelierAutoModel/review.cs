namespace AtelierAutoModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_review { get; set; }

        public int id_client { get; set; }

        [Column(TypeName = "date")]
        public DateTime data { get; set; }

        [Column("review")]
        [Required]
        [StringLength(500)]
        public string review1 { get; set; }

        public virtual clienti clienti { get; set; }
    }
}
