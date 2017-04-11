using MVC_HW3.Models;
using System.Web.Mvc;
using System.Web.UI;
using System;
using MVC_HW3.ViewModel;
using System.Linq;
using System.Collections.Generic;

namespace MVC_HW3.Controllers
{
    public class VotingController : Controller
    {
        TravelEntities db = new TravelEntities();
        tVoteItemOptionViewModel tVoteItemOptionViewModels = new tVoteItemOptionViewModel();
        // GET: Voting
        [HttpGet]
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;
            tVoteItemOptionViewModels.tVoteProjects = db.tVoteProject.ToList();
            tVoteItemOptionViewModels.tVoteItems= db.tVoteItem.ToList();
            tVoteItemOptionViewModels.tVoteOptions = db.tVoteOption.ToList();
            return View(tVoteItemOptionViewModels);
        }

        [HttpPost,ActionName("Index")]
        public ActionResult Index(tVoteDetail tVoteDetails,string datas, int? id)
        {
        
            string[]a = (datas.Split('"'));
            List<string> b = new List<string>();
            
                for (int i = 1; i < a.Count();i+=2)
            {
                b.Add(a[i]);
            }
            
            var VP_ID = db.tVoteProject.Where(p => p.fVP_ID == id).Single().fVP_ID;

            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "Voting", new { id = VP_ID });
            }

            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;
            if (datas.Count() == 0)
            {
                ViewBag.Message = "Please select eProjects";
            }
            else
            {
                // 2 1 1 
                // 2 1 2
                // 2 2 6
                // 2 2 7
                // 2 3 9

                foreach (var n in b)
                {
                    List<string> voteint = new List<string>();
                    voteint.AddRange(n.Split(','));

                    var save = new tVoteDetail
                    {
                         fVP_ID = int.Parse(voteint[0]),
                        fVI_ID = int.Parse(voteint[1]),
                        fVO_ID = int.Parse(voteint[2]),
                        fEp_ID = EP_ID

                    };

                        db.tVoteDetail.Add(save);
                        db.SaveChanges();
                }
            }

            ViewBag.Message = "您的投票紀錄已完成";


            //return View();
            return RedirectToActionPermanent("tVoteResult",id);
        }

        public ActionResult tVoteResult(int? id) {

            ViewBag.id = id;
            tVoteItemOptionViewModels.tVoteProjects = db.tVoteProject.ToList();
            tVoteItemOptionViewModels.tVoteItems = db.tVoteItem.ToList();
            tVoteItemOptionViewModels.tVoteOptions = db.tVoteOption.ToList();
            return View(tVoteItemOptionViewModels);


        }

    }
}