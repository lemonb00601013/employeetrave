using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{

    [MetadataType(typeof(tRegistrationOpenMetadata))]
    public partial class tRegistrationOpen
    {
        public class tRegistrationOpenMetadata
        {

            
            public int fRO_ID { get; set; }
            [DisplayName("截止日期")]
            [DataType(DataType.Date)]

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> fRO_ODate { get; set; }
            [DisplayName("開放日期")]
            [DataType(DataType.Date)]

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime fRO_CDate { get; set; }
            public int fTC_ID { get; set; }

            
            

        }
    }
}