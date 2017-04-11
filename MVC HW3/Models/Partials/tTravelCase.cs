using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(TravelCaseMetadata))]
    public partial class tTravelCase
    {
        public class TravelCaseMetadata
        {
            [DisplayName("標題")]
            [Required(ErrorMessage = "請輸入內容")]
            public string fTC_Title { get; set; }
            [DisplayName("上限")]
            [Required(ErrorMessage = "請輸入人數")]
            public int fTC_Top { get; set; }
            [DisplayName("門檻")]
            [Required(ErrorMessage = "請輸入人數")]
            public int fTC_Gate { get; set; }
            [DisplayName("旅遊時間")]
            public string fTC_TDate { get; set; }
            [DisplayName("旅遊詳情")]
            [DataType(DataType.MultilineText)]
            [AllowHtml]
            public string fTC_Content { get; set; }
            [DisplayName("備註")]
            [DataType(DataType.MultilineText)]
            [AllowHtml]
            public string fTC_Notes { get; set; }
            [DisplayName("期別/狀態")]
            public int fTC_LorD { get; set; }
            [DisplayName("封面照")]
            public byte[] fTC_Picture { get; set; }
            [DisplayName("地點")]
            [Required(ErrorMessage = "請選擇地點")]
            public int fCC_ID { get; set; }
            [DisplayName("攜眷上限")]
            [Required(ErrorMessage = "請輸入人數")]
            public int fTC_PerL { get; set; }
            [DisplayName("車費")]
            [Required(ErrorMessage = "請輸金額")]
            [DisplayFormat(DataFormatString = "{0:c0}")]
            public decimal fTC_Car { get; set; }
            [DisplayName("餐費")]
            [Required(ErrorMessage = "請輸金額")]
            [DisplayFormat(DataFormatString = "{0:c0}")]
            public decimal fTC_Eat { get; set; }
            [DisplayName("住宿費")]
            [Required(ErrorMessage = "請輸金額")]
            [DisplayFormat(DataFormatString = "{0:c0}")]
            public decimal fTC_hotel { get; set; }
            [DisplayName("基本團費")]
            [Required(ErrorMessage = "請輸金額")]
            [DisplayFormat(DataFormatString = "{0:c0}")]
            
            public decimal fTC_Cost { get; set; }
        }
    }
}