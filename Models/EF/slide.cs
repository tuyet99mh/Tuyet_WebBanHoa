namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("slide")]
    public partial class slide
    {
        public int slideID { get; set; }

        [StringLength(250)]
        public string image { get; set; }

        public bool? status { get; set; }
    }
}
