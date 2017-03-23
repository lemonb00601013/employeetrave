using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Areas.EmployeeTrave.Models
{
    [MetadataType(typeof(TravelCaseMetadata))]
    public partial class tTravelCase
    {
        public class TravelCaseMetadata
        {
            [DisplayFormat(DataFormatString = "{0:c0}")]
            public decimal fTC_Cost { get; set; }

        }
    }
}