using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tVoteItemtMetadata))]
    public partial class tVoteItem
    {
        public class tVoteItemtMetadata
        {
            [DisplayName("專案ID")]
            public int fVP_ID { get; set; }

            [DisplayName("問題")]
            [Required(ErrorMessage = "投票問題要輸入")]
            public string fVI_Name { get; set; }

            [DisplayName("問題型態")]
            [Required(ErrorMessage = "請選擇題型")]
            public string fVI_Type { get; set; }
        }
    }
}