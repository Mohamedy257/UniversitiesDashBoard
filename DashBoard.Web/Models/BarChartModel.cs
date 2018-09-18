using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitiesDashBoardDemo.Models
{
    public class BarChartModel
    {
        public string name { get; set; }
        public bool colorByPoint { get; set; }
        public List<BarChartDataModel> data { get; set; }
        public int MyProperty { get; set; }
    }

    public class BarChartDataModel
    {
        public string name { get; set; }
        public double? y { get; set; }
        public string drilldown { get; set; }
    }

    public class BarChartDrillDownModel
    {
        public string name { get; set; }
        public string id { get; set; }
        public List<Tuple<string,double>> data { get; set; }
    }

    
}

