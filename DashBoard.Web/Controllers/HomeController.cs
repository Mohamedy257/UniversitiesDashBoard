using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitiesDashBoardDemo.Models;
using DashBoard.BusinessLayer;

namespace DashBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string dimention)
        {
           
            return View(new DashBoardModel());
        }
        public ActionResult GetChart(string dimention)
        {
            DashBoardModel dashBoardModel = new DashBoardModel
            {

                barChart = LineChartGeneration(dimention)

            };
            return PartialView("_LineChart",dashBoardModel);
        }

        public ActionResult GetPieChart(string dimention)
        {
            DashBoardModel dashBoardModel = new DashBoardModel
            {

                pieChart = PieChartGenerator(dimention)

            };
            return PartialView("_CombinationChart", dashBoardModel);
        }
        public ActionResult DimentionSearchByUniversity(string searchCriteria,string chartDimention)
        {
            var filters = searchCriteria.Split(',').ToList();
            var data = UniversityService.Obj.GetAllUniversitiesData().Where(w=> filters.Contains(w.UniversityName));
            
            if (chartDimention == "1")
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Female", data =  Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() } };

                ViewData["catogries"] = JsonConvert.SerializeObject(data.Select(s => s.UniversityName).Distinct().ToList());
                ViewData["title"] = "Staff By Gender";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#24CBE5", "#64E572" });
                @ViewData["chartDimention"] = "1";
                return Json(JsonConvert.SerializeObject(model),JsonRequestBehavior.AllowGet);
            }
            else if (chartDimention == "2")
            {
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model),JsonRequestBehavior.AllowGet);
            }

            else if (chartDimention == "3")
            {
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }

            else
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();

                List<ChartsModel> model = new List<ChartsModel> {
                    new ChartsModel { name = "Female", data = Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }

        }
        public string LineChartGeneration(string dimention)
        {
            var data = UniversityService.Obj.GetAllUniversitiesData();
            if (dimention == "Gender")
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT}).GroupBy(g=>g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Female", data =  Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() } };

                ViewData["catogries"] = JsonConvert.SerializeObject(data.Select(s => s.UniversityName).Distinct().ToList());
                ViewData["title"] = "Staff By Gender";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#24CBE5", "#64E572" });
                @ViewData["chartDimention"] = "1";
                return JsonConvert.SerializeObject(model);
            }
            else if (dimention == "Nationality")
            {
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                ViewData["catogries"] = JsonConvert.SerializeObject(data.Select(s => s.UniversityName).Distinct().ToList());
                ViewData["title"] = "Staff By Nationality";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#FF9655", "#FFF263" });
                @ViewData["chartDimention"] = "2";
                return JsonConvert.SerializeObject(model);
            }
            else if (dimention == "Staff Type")
            {
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                ViewData["title"] = "Staff By Staff Type";
                ViewData["catogries"] = JsonConvert.SerializeObject(data.Select(s => s.UniversityName).Distinct().ToList());
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#ED561B", "#DDDF00" });
                @ViewData["chartDimention"] = "3";
                return JsonConvert.SerializeObject(model);
            }
            else
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();

                List<ChartsModel> model = new List<ChartsModel> {
                    new ChartsModel { name = "Female", data = Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                ViewData["catogries"] = JsonConvert.SerializeObject(data.Select(s => s.UniversityName).Distinct().ToList());
                ViewData["title"] = "Staff By Type";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#24CBE5", "#64E572", "#FF9655", "#FFF263", "#ED561B", "#DDDF00" });
                @ViewData["chartDimention"] = "4";
                return JsonConvert.SerializeObject(model);
            }
        }

        public ActionResult ChangeDimention(string dimention)
        {
            var data = UniversityService.Obj.GetAllUniversitiesData();
            if (dimention == "Gender")
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Female", data =  Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() } };
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }
            else if (dimention == "Nationality")
            {
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }
            else if (dimention == "Staff Type")
            {
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var non_Saudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminAndTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();

                List<ChartsModel> model = new List<ChartsModel> {
                    new ChartsModel { name = "Female", data = Female.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Male", data = Male.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Saudi", data = Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Non Saudi", data = non_Saudi.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                new ChartsModel { name = "Teaching Staff", data = teachingStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() },
                 new ChartsModel { name = "Admin and Tech Staff", data = adminAndTechStaff.Select(a=> a.Sum(b=>b.CNT)).ToList() }};
                return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
            }

        }

        public string PieChartGenerator(string dimention)
        {
            var filters = UniversityService.Obj.GetAllUniversitiesFilters();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (var item in filters)
            {
                selectListItem.Add(new SelectListItem() { Text = item, Value = item });
            }
            ViewData["filter"]=new SelectList(selectListItem,  "Text","Value");

            if (dimention == "Gender")
            {
                var data = UniversityService.Obj.GetAllUniversitiesData();
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();

                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="Male",y=(Male.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Female",y=(Female.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Gender";
                ViewData["color"] = JsonConvert.SerializeObject((new List<string>() { "#24CBE5", "#64E572" }).ToList());
                @ViewData["piechartDimention"] = "1";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else if (dimention == "Nationality")
            {
                var data = UniversityService.Obj.GetAllUniversitiesData();
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var NonSaudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="Saudi",y=(Saudi.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Non Saudi",y=(NonSaudi.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Nationality";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#FF9655", "#FFF263" });
                @ViewData["piechartDimention"] = "2";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else if (dimention == "Staff Type")
            {
                var data = UniversityService.Obj.GetAllUniversitiesData();
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminandTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="TeachingST",y=(teachingStaff.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Admin&Tech",y=(adminandTechStaff.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Staff Type";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#ED561B", "#DDDF00" });
                @ViewData["piechartDimention"] = "3";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else
                throw new Exception("No Chart Found");
        }

        public string PieChartSearchByUniversity(string searchCriteria, string chartDimention)
        {
            var filters = searchCriteria.Split(',').ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (var item in filters)
            {
                selectListItem.Add(new SelectListItem() { Text = item, Value = item });
            }
            ViewData["filter"] = new SelectList(selectListItem, "Text", "Value");

            var data = UniversityService.Obj.GetAllUniversitiesData().Where(w => filters.Contains(w.UniversityName));
            if (chartDimention == "1")
            {
                var Female = (data.Where(x => x.Gender == "Female").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var Male = (data.Where(x => x.Gender == "Male").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="Male",y=(Male.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Female",y=(Female.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Gender";
                ViewData["color"] = JsonConvert.SerializeObject((new List<string>() { "#24CBE5", "#64E572" }).ToList());
                @ViewData["piechartDimention"] = "1";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else if (chartDimention == "2")
            {
                var Saudi = (data.Where(x => x.Nationality == "Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var NonSaudi = (data.Where(x => x.Nationality == "Non Saudi").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="Saudi",y=(Saudi.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Non Saudi",y=(NonSaudi.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Nationality";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#FF9655", "#FFF263" });
                @ViewData["piechartDimention"] = "2";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else if (chartDimention == "3")
            {
                var teachingStaff = (data.Where(x => x.Staff_Type == "Teaching Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                var adminandTechStaff = (data.Where(x => x.Staff_Type == "Admin and Tech Staff").Select(s => new { s.UniversityName, s.CNT }).GroupBy(g => g.UniversityName)).ToList();
                
                List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="TeachingST",y=(teachingStaff.Select(a=> a.Sum(b=>b.CNT))).Average()},
                new BarChartDataModel { name="Admin&Tech",y=(adminandTechStaff.Select(a=> a.Sum(b=>b.CNT))).Average()}
            };
                ViewData["title"] = "Average Ratio By Staff Type";
                ViewData["color"] = JsonConvert.SerializeObject(new List<string>() { "#ED561B", "#DDDF00" });
                @ViewData["piechartDimention"] = "3";
                string result = JsonConvert.SerializeObject(barChartDataModel);
                return result;
            }
            else
                throw new Exception("No Chart Found");
        }


    }
}