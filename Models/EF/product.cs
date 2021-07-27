namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            cart_product = new HashSet<cart_product>();
            orderdetails = new HashSet<orderdetail>();
        }
        [Key]
        public int proID { get; set; }

        [Display(Name = "Mã sản phẩm")]
        [StringLength(10)]
        public string codePr { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [StringLength(250)]
        public string proName { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        public int catID { get; set; }


        [Display(Name = "Mô tả")]
        [Column(TypeName = "ntext")]
        public string description { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(250)]
        public string image { get; set; }

        [Display(Name = "Giá nhập")]
        public int? promotionPrice { get; set; }

        [Display(Name = "Giá bán")]
        public int? price { get; set; }

        public bool? includedVAT { get; set; }

        [StringLength(250)]
        public string metaTitle { get; set; }

        [Display(Name = "Số lượng")]
        public int? quantity { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createBy { get; set; }

        [StringLength(50)]
        public string modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? status { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_product> cart_product { get; set; }

        public virtual category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderdetail> orderdetails { get; set; }
    }
}
