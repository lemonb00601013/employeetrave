using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tBulletinMetadata))]
    public partial class tBulletin
    {
        public class tBulletinMetadata
        {
            [DisplayName("公告編號")]
            public int fBu_ID { get; set; }
            [DisplayName("公告日期")]
            public Nullable<System.DateTime> fBu_Date { get; set; }
            [DisplayName("公告編號")]
            public int fBC_ID { get; set; }
            [DisplayName("公告標題")]
            public string fBu_Title { get; set; }
            [AllowHtml]
            [DisplayName("公告內容")]
            public string fBu_Content { get; set; }

            public virtual tBulletinClass tBulletinClass { get; set; }
        }
    }
}
