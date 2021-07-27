namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class feedback
    {
        [Key]
        public int fbID { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(250)]
        public string content { get; set; }

        public DateTime? creatDate { get; set; }

        public bool? status { get; set; }
    }
}
