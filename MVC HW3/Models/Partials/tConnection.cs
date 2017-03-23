using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(ConnectionMetadata))]
    public partial class tConnection
    {
        public class ConnectionMetadata
        {
            [DisplayName("內容分類")]
            [Required(ErrorMessage = "請選擇內容分類")]
            public int fCC_ID { get; set; }

            [DisplayName("主旨")]
            [Required(ErrorMessage = "請輸入主旨")]
            public string fCo_Theme { get; set; }

            [DisplayName("內容")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "請輸入內容")]
            public string fCo_Content { get; set; }

            [DisplayName("附加圖片")]            
            public byte[] fCo_Image { get; set; }
        }
    }
}