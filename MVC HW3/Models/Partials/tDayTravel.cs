using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Models
{
    [MetadataType(typeof(DayTravelMetadata))]
    public partial class tDayTravel
    {
        public class DayTravelMetadata
        {

            public int fDT_ID { get; set; }
            public int fTC_ID { get; set; }
            [DisplayName("日程內容")]
            [DataType(DataType.MultilineText)]
            [AllowHtml]
            public string fDT_Content { get; set; }
            [DisplayName("日期")]
            [DataType(DataType.Date)]

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
            public System.DateTime fDT_Date { get; set; }

        }
    }
}