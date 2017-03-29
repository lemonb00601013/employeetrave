using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW3.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_HW3.ViewModel
{
    public class ActivityModel
    {
        public IEnumerable<tActivityList> ActList { get; set; }

        public IEnumerable<tCityCountry> CityCountry { get; set; }

        public IEnumerable<tActivitySelect> ActSelect { get; set; }

        public IEnumerable<tSampleProposal> Proposal { get; set; }

        [DisplayName("對城市的一些想法")]
        [DataType(DataType.MultilineText)]
        public string fSP_Attraction { get; set; }
    }
}