using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(tCityCountryMetadata))]
    public partial class tCityCountry
    {
        public class tCityCountryMetadata
        {
            [DisplayName("城市")]
            public int fCC_ID { get; set; }
        }
    }
}