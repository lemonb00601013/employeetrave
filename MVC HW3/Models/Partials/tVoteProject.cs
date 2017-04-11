using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tVoteProjectMetadata))]
    public partial class tVoteProject
    {
        public class tVoteProjectMetadata {


            [DisplayName("投票主題")]
            [Required(ErrorMessage = "投票主題要輸入")]
            [DataType(DataType.MultilineText)]
            public string fVP_Name { get; set; }

            [DisplayName("主題描述")]
            [DataType(DataType.MultilineText)]
            public string fVP_Description { get; set; }

            [DisplayName("投票起始")]
            //[DisplayFormat(DataFormatString = "{0:d}")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime fVP_StartDate { get; set; }

            [DisplayName("投票截止")]
            //[DisplayFormat(DataFormatString = "{0:d}")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime fVP_EndDate { get; set; }
        }
    }
}