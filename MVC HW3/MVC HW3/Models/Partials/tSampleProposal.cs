using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tSampleProposalMetadata))]
    public partial class tSampleProposal
    {
        public class tSampleProposalMetadata
        {
            [DisplayName("城市")]
            [Required(ErrorMessage = "請填寫城市")]
            public Nullable<int> fCC_ID { get; set; }
        }
    }
}