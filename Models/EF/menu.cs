namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("menu")]
    public partial class menu
    {
        public int menuID { get; set; }

        [StringLength(50)]
        public string text { get; set; }

        [StringLength(250)]
        public string link { get; set; }

        public int? displayOrder { get; set; }

        [StringLength(50)]
        public string target { get; set; }

        public bool? status { get; set; }

        public int? menuTypeID { get; set; }

        public virtual menuType menuType { get; set; }
    }
}
