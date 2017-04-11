using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(PenaltyMetadata))]
    public partial class tPenalty
    {
        public class PenaltyMetadata
        {

            
            public int fPe_ID { get; set; }
            
            public int fTC_ID { get; set; }
            [DisplayName("取消時間")]
            public int fPe_Day { get; set; }
            [DisplayName("罰款")]
            public decimal fPe_Fine { get; set; }

        }
    }
}