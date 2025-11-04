using LegalProject.Models.Data;
using LegalProject.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace LegalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MainModelList()
        {
            List<MainModel> list = _db.MainModels.Include(x=> x.DetailsModels).ToList();
            return View(list);
        }
        public ActionResult MainModelDetails(int  id)
        {
            MainModel mainModel = _db.MainModels.Include(x => x.DetailsModels).FirstOrDefault(x => x.Id == id);
            return View(mainModel.DetailsModels);
        }
        

        public ActionResult CreateDetails(int mainModelId) 
        {
            var ActionTypes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "دادخواست", Value = "1" },
                    new SelectListItem { Text = "ارجاع به اجرا", Value = "2" },
                    new SelectListItem { Text = "توقیف", Value = "3" }
                };

            // Put it in ViewBag
            ViewBag.ActionTypeList = ActionTypes;
            DetailsModel model = new DetailsModel()
            {
                MainModelId = mainModelId
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetails(DetailsModel detailsModel)
        {
            if (!ModelState.IsValid) { 
                return View(detailsModel);
            }
            MainModel mainModel = _db.MainModels.FirstOrDefault(f => f.Id == detailsModel.MainModelId);
            if (mainModel != null)
            {
                switch (detailsModel.ActionType)
                {
                        //دادخواست
                    case 1:
                        {
                            mainModel.PatitionDate = detailsModel.ActionDate;
                            break;
                        }
                        //ارجاع به اجرا
                    case 2:
                        {
                            mainModel.ExecArchiveNumber = detailsModel.CaseNumber;
                            break;
                        }
                        //توقیف
                    case 3:
                        {
                            mainModel.SeizureDate = detailsModel.ActionDate;
                            break;
                        }
                    default:
                        break;
                }
                _db.MainModels.AddOrUpdate(mainModel);
                _db.DetailsModels.Add(detailsModel);
                _db.SaveChanges();
            }
            
            return RedirectToAction(nameof(MainModelList));
        }

    }
}