namespace AtelierAutoModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("programari")]
    public partial class programari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_programare { get; set; }

        public int id_client { get; set; }

        public DateTime data { get; set; }

        public int id_specializare { get; set; }

        public virtual clienti clienti { get; set; }

        public virtual specializari_mecanici specializari_mecanici { get; set; }
    }
}
