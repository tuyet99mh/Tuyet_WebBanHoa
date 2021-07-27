namespace Anemone.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class contact
    {
        public int contactID { get; set; }

        [DisplayName ( "Số điện thoại")]
        [StringLength(50)]
        public string phone { get; set; }


        [DisplayName ( "Email shop")]
        [StringLength(50)]
        public string email { get; set; }

        [DisplayName ( "Địa chỉ")]
        [StringLength(250)]
        public string address { get; set; }

        [DisplayName ( "Lời cám ơn")]
        [StringLength(50)]
        public string endThanks { get; set; }

        [DisplayName ("Lời chào")]
        [StringLength(50)]
        public string welcome { get; set; }


        [DisplayName("Mô tả")]
        [Column(TypeName = "ntext")]
        public string descrip { get; set; }

        [DisplayName("Ảnh logo")]
        [StringLength(250)]
        public string logo { get; set; }

        
        [DisplayName ("Tên Shop")]
        [StringLength(250)]
        public string name { get; set; }

    }
}
