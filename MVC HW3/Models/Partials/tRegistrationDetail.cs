using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(RegistrationDetailMetadata))]
    public partial class tRegistrationDetail
    {
        public class RegistrationDetailMetadata
        {
  
            [DisplayName("用餐")]
            public Nullable<bool> fRD_Eat { get; set; }       
    
            [DisplayName("緊急聯絡人")]
            public string fRD_Urgent { get; set; }
            [DisplayName("緊急連絡人電話")]
            public string fRD_UTel { get; set; }
            [DisplayName("備註")]
            [DataType(DataType.MultilineText)]
            public string fRD_Note { get; set; }
            [DisplayName("保險受益人")]
            public string fRD_Insurance { get; set; }
          
        }
    }
}