namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            cart_product = new HashSet<cart_product>();
            orders = new HashSet<order>();
        }

        public int userID { get; set; }

        [Display(Name = "Tên người dùng")]
        [StringLength(250)]
        public string userName { get; set; }

        [Display(Name = "Email đăng nhập")]
        [StringLength(50)]
        public string email { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(50)]
        public string phone { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250)]
        public string address { get; set; }

        [Display(Name = "Trạng thái tài khoản (Chọn True nếu muốn kích hoạt tài khoản)")]
        public bool? status { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(32)]
        public string pass { get; set; }

        [Display(Name = "Loại tài khoản")]
        public int? userTypeID { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createBy { get; set; }

        [StringLength(50)]
        public string modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_product> cart_product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        public virtual userType userType { get; set; }
    }
}
