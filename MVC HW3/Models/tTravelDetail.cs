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
    
    public partial class tTravelDetail
    {
        public int fTD_ID { get; set; }
        public int fCC_ID { get; set; }
        public int fTC_ID { get; set; }
        public string fTD_Title { get; set; }
        public string fTD_Content { get; set; }
        public Nullable<int> fEp_ID { get; set; }
        public byte[] fTD_Image { get; set; }
        public string fTD_site { get; set; }
    
        public virtual tCityCountry tCityCountry { get; set; }
        public virtual tEmployee tEmployee { get; set; }
        public virtual tTravelClass tTravelClass { get; set; }
    }
}