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
            [DisplayName("���i�s��")]
            public int fBu_ID { get; set; }
            [DisplayName("���i���")]
            public Nullable<System.DateTime> fBu_Date { get; set; }
            [DisplayName("���i�s��")]
            public int fBC_ID { get; set; }
            [DisplayName("���i���D")]
            public string fBu_Title { get; set; }
            [AllowHtml]
            [DisplayName("���i���e")]
            public string fBu_Content { get; set; }

            public virtual tBulletinClass tBulletinClass { get; set; }
        }
    }
}
