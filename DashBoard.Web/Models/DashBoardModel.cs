using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitiesDashBoardDemo.Models
{
    public class DashBoardModel
    {
        public string pieChart { get; set; }
        public List<ColumnSeriesData> seriesChart { get; set; }
        public List<PyramidSeriesData> pyramidChart { get; set; }
        public CombinationModel combinationModel { get; set; }
        public string barChart { get; set; }
        public string barChartDrill { get; set; }
        public string catogries { get; set; }
        public string SearchCriteria { get; set; }
    }
}