namespace MVC_HW3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(tEmployeeMetadata))]
    public partial class tEmployee
    {
        public class tEmployeeMetadata
        {

            [DisplayName("員工編號")]
            public int fEp_ID { get; set; }
            [DisplayName("員工編號")]
            public string fEp_Code { get; set; }
            [DisplayName("員工姓名")]
            public string fEp_Name { get; set; }
            [DisplayName("員工身份證字號")]
            public string fEp_SSNumber { get; set; }
            [DisplayName("員工居住地址")]
            public string fEp_Addr { get; set; }
            [DisplayName("員工居住地址電話")]
            public string fEp_Tel { get; set; }
            [DisplayName("員工手機號碼")]
            public string fEp_Phone { get; set; }
            [DisplayName("職稱")]
            public int fId_ID { get; set; }
            [DisplayName("員工年資")]
            [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy年MM月dd日}")]
            public System.DateTime fEp_Seniority { get; set; }
            [DisplayName("部門")]
            public int fSe_ID { get; set; }
            [DisplayName("員工綽號")]
            public string fEp_Nickname { get; set; }
            [DisplayName("員工密碼")]
            public string fEp_Password { get; set; }

            [DisplayName("員工信箱")]
            public string fEp_Email { get; set; }
            [DisplayName("員工大頭照")]
            public byte[] fEp_Picture { get; set; }
            [DisplayName("員工生日")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy年MM月dd日}")]
            public System.DateTime fEp_Birth { get; set; }

        }
    }
}