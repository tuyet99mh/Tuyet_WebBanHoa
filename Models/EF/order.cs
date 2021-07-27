namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            orderdetails = new HashSet<orderdetail>();
        }

        [Key]
        public int idOrder { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public DateTime? orderDate { get; set; }


        [Display(Name = "Trạng thái đơn hàng")]
        public bool? status { get; set; }

        public int idID { get; set; }

        

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderdetail> orderdetails { get; set; }
    }
}
