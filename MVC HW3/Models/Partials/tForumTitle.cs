using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tForumTitleMetadata))]
    public partial class tForumTitle
    {
        public class tForumTitleMetadata
        {
            [DisplayName("標題內文")]
            public string fFT_Title { get; set; }

            [DisplayName("點擊數")]
            public int fFT_Popul { get; set; }

            [DisplayName("標題類別編號")]
            public int fFC_ID { get; set; }

            [DisplayName("文章代碼")]
            public int fMC_ID { get; set; }

            [DisplayName("發表員工ID")]
            public int fEp_ID { get; set; }

            [DisplayName("發表時間")]
            public Nullable<System.DateTime> fFT_Date { get; set; }
        }
    }

    

}