//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_HW3.Areas.Albums.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tPhoto
    {
        public int fPh_ID { get; set; }
        public byte[] fPh_PicFile { get; set; }
        public int fAl_ID { get; set; }
        public Nullable<System.DateTime> fPh_Date { get; set; }
        public string fPh_Notes { get; set; }
        public int fMC_ID { get; set; }
    
        public virtual tAlbum tAlbum { get; set; }
        public virtual tMessageCode tMessageCode { get; set; }
    }
}
