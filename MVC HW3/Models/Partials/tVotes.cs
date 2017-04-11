using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tVotesMetadata))]
    public partial class tVotes
    {
        public class tVotesMetadata
        {

            [DisplayName("問題")]
            [Required(ErrorMessage = "請輸入問題")]
            [DataType(DataType.MultilineText)]
            public string fVI_Name { get; set; }

            [DisplayName("問題型態")]
            [Required(ErrorMessage = "請選擇問題型態")]
            [DataType(DataType.Custom)]
            public string fVI_Type { get; set; }

            [DisplayName("選項")]
            public string fVO_OptionName { get; set; }

            [DisplayName("選項圖示")]
            public string fVO_OptionImage { get; set; }

        }
    }

}