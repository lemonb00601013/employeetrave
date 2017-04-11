using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW3.Models;

namespace MVC_HW3.ViewModel
{
    public class tVoteItemOptionViewModel
    {
        public IEnumerable<tVoteProject> tVoteProjects { get; set; }
        public IEnumerable<tVoteItem> tVoteItems { get; set; }
        public IEnumerable<tVoteOption> tVoteOptions{ get; set; }
        public IEnumerable<tVoteOption> tVoteDetail { get; set; }
    }
}