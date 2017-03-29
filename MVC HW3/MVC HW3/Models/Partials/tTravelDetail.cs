using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tTravelDetailMetadata))]
    public partial class tTravelDetail
    {
        public class tTravelDetailMetadata
        {
            [DisplayName("所在城市")]
            [Required(ErrorMessage = "請填寫所在城市")]
            public int fCC_ID { get; set; }

            [DisplayName("所屬種類")]
            [Required(ErrorMessage = "請填寫所屬種類")]
            public int fTC_ID { get; set; }

            [DisplayName("推薦標題")]
            [Required(ErrorMessage = "請填寫標題")]
            public string fTD_Title { get; set; }

            [DisplayName("推薦內容")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "請填寫內容")]
            public string fTD_Content { get; set; }

            [DisplayName("相關圖片")]
            public byte[] fTD_Image { get; set; }

            [DisplayName("地址")]
            //[Required(ErrorMessage = "請填寫地址")]
            public string fTD_site { get; set; }
        }
    }
}