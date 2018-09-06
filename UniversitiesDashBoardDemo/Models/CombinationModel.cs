using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitiesDashBoardDemo.Models
{
    public class CombinationModel
    {
        public List<ColumnSeriesData> techData { get; set; }
        public List<ColumnSeriesData> adminData { get; set; }
        public List<SplineSeriesData> averageData { get; set; }
        public List<PieSeriesData> pieData4 { get; set; }
    }
}