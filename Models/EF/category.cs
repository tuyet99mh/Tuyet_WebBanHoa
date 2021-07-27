namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int catID { get; set; }
        [Display(Name = "Tên danh mục sản phẩm")]
        [StringLength(250)]
        public string name { get; set; }

        [StringLength(250)]
        public string metaTitle { get; set; }

        public int? parentID { get; set; }

        public int? displayOrder { get; set; }


        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createBy { get; set; }

        [StringLength(50)]
        public string modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? status { get; set; }

        public bool? showHome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
