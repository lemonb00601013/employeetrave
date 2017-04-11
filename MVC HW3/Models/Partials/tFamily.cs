using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(FamilyMetadata))]
    public partial class tFamily
    {
        public class FamilyMetadata
        {
    
            [DisplayName("姓名")]
            public string fFa_Name { get; set; }
            [DisplayName("關係")]
            public int fRe_ID { get; set; }
            [DisplayName("手機")]
            public string fFa_Tel { get; set; }
            [DisplayName("身分證字號")]
            public string fFa_SSNumber { get; set; }
            public int fSt_ID { get; set; }
            [DisplayName("緊急聯絡人")]
            public string fFa_Urgent { get; set; }
            [DisplayName("緊急聯絡人電話")]
            public string fFa_UTel { get; set; }
            [DisplayName("備註")]
            [DataType(DataType.MultilineText)]
            public string fFa_Note { get; set; }
            [DisplayName("保險受益人")]
            public string fFa_Insurance { get; set; }
            [DisplayName("生日")]
            [DataType(DataType.Date)]
            public System.DateTime fFa_Birth { get; set; }
            [DisplayName("特殊身分")]
            public Nullable<int> fSI_ID { get; set; }
            [DisplayName("用餐")]
            public Nullable<int> fTE_ID { get; set; }
            [DisplayName("車位")]
            public Nullable<bool> fFa_Car { get; set; }
            [DisplayName("住宿")]
            public Nullable<bool> fFa_Hotel { get; set; }
          
        }
    }
}