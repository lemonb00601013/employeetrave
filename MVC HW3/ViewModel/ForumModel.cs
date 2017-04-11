using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW3.Models;

namespace MVC_HW3.ViewModel
{
    public class ForumModel
    {
        public IEnumerable<tEmployee> employees { get; set; }

        public IEnumerable<tFieldValue> Fvalue { get; set; }

        public IEnumerable<tForumClass> Fclass { get; set; }

        public IEnumerable<tForumField> Ffield { get; set; }

        public IEnumerable<tForumMessage> Fmessage { get; set; }

        public IEnumerable<tForumTitle> Ftitle { get; set; }

        public IEnumerable<tMessageCode> messagecode { get; set; }

        public IEnumerable<tPushGood> pushGood { get; set; }
    }
}