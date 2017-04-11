using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models.Partials
{
    [MetadataType(typeof(tDependentsMetadata))]
    public partial class tDependents
    {
        public class tDependentsMetadata
        {
                       
            [DisplayName("親屬編號")]
            public int fDe_ID { get; set; }
            [DisplayName("員工編號")]
            public int fEp_ID { get; set; }
            [DisplayName("親屬姓名")]
            public string fDe_Name { get; set; }
            [DisplayName("親屬電話")]
            public string fDe_Tel { get; set; }
            [DisplayName("親屬身分證號碼")]
            public string fDe_SSNumber { get; set; }
            [DisplayName("員工關係")]
            public int fRe_ID { get; set; }
            [DisplayName("員工親屬生日")]
            public System.DateTime fDe_Birth { get; set; }

        }
    }
}