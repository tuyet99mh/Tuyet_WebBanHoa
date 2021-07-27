namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("orderdetail")]
    public partial class orderdetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idOrder { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proID { get; set; }

        public int? unitPrice { get; set; }

        public int? quantity { get; set; }

        public int? discount { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }
    }
}
