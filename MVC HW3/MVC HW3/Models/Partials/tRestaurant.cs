using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tRestaurantData))]
    public partial class tRestaurant
    {
        public class tRestaurantData
        {
            [DisplayName("餐廳名稱")]
            [Required(ErrorMessage = "名稱未填")]
            public string fRe_Name { get; set; }
            [DisplayName("餐廳簡介")]
            [Required(ErrorMessage = "簡介未填")]
            public string fRe_Content { get; set; }
            [DisplayName("電話")]
            public string fRe_Tel { get; set; }
            [DisplayName("餐廳地址")]
            [Required(ErrorMessage = "地址未填")]
            public string fRe_Addr { get; set; }
            [DisplayName("是否外送")]
            [DisplayFormat]
            public Nullable<bool> fRe_Delivery { get; set; }
            [DisplayName("平均價位")]
            [DisplayFormat(DataFormatString = "{0:c0}")]
            [Required(ErrorMessage = "平均價位未填")]
            public Nullable<decimal> fR_Price { get; set; }
            [DisplayName("餐廳介紹")]
            [Required(ErrorMessage = "介紹未填")]
            public string fRe_introduction { get; set; }
            [DisplayName("餐廳圖片")]
            public byte[] fRe_Image { get; set; }

            public int fEp_ID { get; set; }

            public int fMC_ID { get; set; }

            public string fRe_site { get; set; }
        }
    }
}