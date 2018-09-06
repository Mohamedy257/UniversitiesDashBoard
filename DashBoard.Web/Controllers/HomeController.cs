using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversitiesDashBoardDemo.Models;

namespace DashBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string ca = "";
            DashBoardModel dashBoardModel = new DashBoardModel
            {

                barChart = LineChartGeneration(out ca),
                //barChartDrill = BarChartGenerator(),
                catogries = ca,
                pieChart = PieChartGenerator(),
                barChartDrill = DrilDownGenerator(),

            };
            return View(dashBoardModel);
        }

        public ActionResult search(string criteria)
        {
            string[] criteriaList = criteria.Split(',');
            List<ChartsModel> model = null;
            if (criteriaList.Length == 1)
            {
                model = new List<ChartsModel> {
                new ChartsModel { name="All Student",data = new List<int> { 123} },
                new ChartsModel { name="Male Student",data = new List<int> { 123} },
                new ChartsModel { name="Female Student",data = new List<int> { 123} },
                new ChartsModel { name="Saudi Student",data = new List<int> { 123} },
                new ChartsModel { name="non-Saudi Student",data = new List<int> { 123} }
            };
            }

            else if (criteriaList.Length == 2)
            {
                model = new List<ChartsModel> {
                new ChartsModel { name="All Student",data = new List<int> { 123,100} },
                new ChartsModel { name="Male Student",data = new List<int> { 123,125} },
                new ChartsModel { name="Female Student",data = new List<int> { 123,10} },
                new ChartsModel { name="Saudi Student",data = new List<int> { 123,10} },
                new ChartsModel { name="non-Saudi Student",data = new List<int> { 123,10} }
            };
            }

            else if (criteriaList.Length == 3)
            {
                model = new List<ChartsModel> {
                new ChartsModel { name="All Student",data = new List<int> { 123,100, 100 } },
                new ChartsModel { name="Male Student",data = new List<int> { 123,125,100} },
                new ChartsModel { name="Female Student",data = new List<int> { 123,10,139} },
                new ChartsModel { name="Saudi Student",data = new List<int> { 123,10,92} },
                new ChartsModel { name="non-Saudi Student",data = new List<int> { 123,10,43} }
            };
            }

            else if (criteriaList.Length == 4)
            {
                model = new List<ChartsModel> {
                new ChartsModel { name="All Student",data = new List<int> { 123,100, 123, 100 } },
                new ChartsModel { name="Male Student",data = new List<int> { 123,125, 123, 100 } },
                new ChartsModel { name="Female Student",data = new List<int> { 123,10, 123, 100 } },
                new ChartsModel { name="Saudi Student",data = new List<int> { 123,10, 123, 100 } },
                new ChartsModel { name="non-Saudi Student",data = new List<int> { 123,10, 123, 100 } }
            };
            }
            return Json(JsonConvert.SerializeObject(model), JsonRequestBehavior.AllowGet);
        }

        public string LineChartGeneration(out string catogries)
        {
            List<ChartsModel> model = new List<ChartsModel> {
                new ChartsModel { name="All Student",data = new List<int> { 1223,1100,170,1003, 1723, 190, 1239, 1000 } },
                new ChartsModel { name="Male Student",data = new List<int> { 933,2125,305,1263, 1233, 1688, 4325, 180 } },
                new ChartsModel { name="Female Student",data = new List<int> {823,310,1332,1004, 123, 2090, 2356, 180 } },
                new ChartsModel { name="Saudi Student",data = new List<int> { 7123,410,1442,5003, 123, 870, 9864, 180 } },
                new ChartsModel { name="non-Saudi Student",data = new List<int> { 123,910,1542,2600, 123, 8100, 980, 150 } }

            };
            catogries = JsonConvert.SerializeObject(new List<string> { "ALBaha University", "DAMMAM University", "Jazan University", "King Abdulaziz University", "ALBaha University", "DAMMAM University", "Jazan University", "King Abdulaziz University" });
            return JsonConvert.SerializeObject(model);
        }
        public string PieChartGenerator()
        {
            List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="ALBaha University",y=23,drilldown="ALBaha University"},
                new BarChartDataModel { name="DAMMAM University",y=123,drilldown="DAMMAM University"},
                new BarChartDataModel { name="Jazan University",y=223,drilldown="Jazan University"},
                new BarChartDataModel { name="King Abdulaziz University",y=63,drilldown="King Abdulaziz University"},
            };
            string result = JsonConvert.SerializeObject(barChartDataModel);
            return result;
        }
        public string BarChartGenerator()
        {
            List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            {
                new BarChartDataModel { name="ALBaha University",y=23,drilldown="ALBaha University"},
                new BarChartDataModel { name="DAMMAM University",y=123,drilldown="DAMMAM University"},
                new BarChartDataModel { name="Jazan University",y=223,drilldown="Jazan University"},
                new BarChartDataModel { name="King Abdulaziz University",y=63,drilldown="King Abdulaziz University"},
            };

            List<BarChartDrillDownModel> barChartDrillDownModel = new List<BarChartDrillDownModel>()
            {
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
            };
            var serializer = new JsonSerializer();
            string result = JsonConvert.SerializeObject(barChartDataModel) + ",'drilldown': {" + JsonConvert.SerializeObject(barChartDrillDownModel) + "}";

            return result;
        }

        public string DrilDownGenerator()
        {
            //List<BarChartDataModel> barChartDataModel = new List<BarChartDataModel>()
            //{
            //    new BarChartDataModel { name="ALBaha University",y=23,drilldown="ALBaha University"},
            //    new BarChartDataModel { name="DAMMAM University",y=123,drilldown="DAMMAM University"},
            //    new BarChartDataModel { name="Jazan University",y=223,drilldown="Jazan University"},
            //    new BarChartDataModel { name="King Abdulaziz University",y=63,drilldown="King Abdulaziz University"},
            //};

            List<BarChartDrillDownModel> barChartDrillDownModel = new List<BarChartDrillDownModel>()
            {
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
                new BarChartDrillDownModel{name="ALBaha University",id="ALBaha University",data=new List<Tuple<string, double>>(){ new Tuple<string, double>("Male",100), new Tuple<string, double>("Female", 100) , new Tuple<string, double>("Saudi", 100) } },
            };
            var serializer = new JsonSerializer();
            string result = JsonConvert.SerializeObject(barChartDrillDownModel);

            return result;
        }

    }
}