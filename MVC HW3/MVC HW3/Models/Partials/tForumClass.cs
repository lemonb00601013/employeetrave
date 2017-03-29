using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models.Partials
{


    [MetadataType(typeof(tForumClassMetadata))]
    public partial class tForumClass
    {
        public class tForumClassMetadata
        {
            [DisplayName("文章類別編號")]
            public int fFC_ID { get; set; }

            [DisplayName("文章類別")]
            public string fFC_Class { get; set; }

            [DisplayName("文章類別從屬")]
            public Nullable<int> fFC_Dad { get; set; }
        }
    }
}