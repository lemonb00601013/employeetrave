//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_HW3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tFamily
    {
        public int fFa_ID { get; set; }
        public int fRD_ID { get; set; }
        public string fFa_Name { get; set; }
        public int fRe_ID { get; set; }
        public string fFa_Tel { get; set; }
        public string fFa_SSNumber { get; set; }
        public int fSt_ID { get; set; }
        public string fFa_Urgent { get; set; }
        public string fFa_UTel { get; set; }
        public string fFa_Note { get; set; }
        public string fFa_Insurance { get; set; }
        public System.DateTime fFa_Birth { get; set; }
        public Nullable<int> fSI_ID { get; set; }
        public Nullable<int> fTE_ID { get; set; }
        public Nullable<bool> fFa_Car { get; set; }
        public Nullable<bool> fFa_Hotel { get; set; }
        public string fFa_Relief { get; set; }
        public Nullable<decimal> fFa_RMoney { get; set; }
    
        public virtual tRegistrationDetail tRegistrationDetail { get; set; }
        public virtual tRelationship tRelationship { get; set; }
        public virtual tSpecialIdentity tSpecialIdentity { get; set; }
        public virtual tStatus tStatus { get; set; }
        public virtual tTraveEat tTraveEat { get; set; }
    }
}
