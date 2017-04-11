using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(DependentsTravelMetadata))]
    public partial class tDependentsTravel
    {
        public class DependentsTravelMetadata
        {

            [DisplayName("備註")]
            [DataType(DataType.MultilineText)]
            public string fDT_Note { get; set; }
            [DisplayName("緊急聯絡人")]
            public string fDT_Urgent { get; set; }
            [DisplayName("緊急連絡人電話")]
            public string fDT_UTel { get; set; }
            [DisplayName("保險受益人")]
            public string fDT_Insurance { get; set; }
            [DisplayName("特殊身分")]
            public Nullable<int> fSI_ID { get; set; }
            [DisplayName("用餐")]
            public Nullable<int> fTE_ID { get; set; }
            [DisplayName("車位")]
            public Nullable<bool> fDT_Car { get; set; }
            [DisplayName("住宿")]
            public Nullable<bool> fDT_Hotel { get; set; }
            

        }
    }
}