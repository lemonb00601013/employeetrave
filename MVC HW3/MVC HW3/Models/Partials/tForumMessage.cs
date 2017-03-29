using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tForumMessageMetadata))]
    public partial class tForumMessage
    {
        public class tForumMessageMetadata
        {
            [AllowHtml]
            [DisplayName("文章內容")]
            public string fFM_Content { get; set; }

            [DisplayName("發表員工ID")]
            public int fEp_ID { get; set; }

            [DisplayName("發表時間")]
            public System.DateTime fFM_Date { get; set; }

            [DisplayName("推文從屬")]
            public Nullable<int> fFM_Dad { get; set; }

            [DisplayName("文章代碼")]
            public int fMC_ID { get; set; }
        }
    }
    
}