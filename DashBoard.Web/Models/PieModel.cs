using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitiesDashBoardDemo.Models
{
    public class ChartsModel
    {
        public string name { get; set; }
        //public double y { get; set; }
        //public bool sliced { get; set; }
        //public bool selected { get; set; }
        public List<double?> data { get; set; }
    }
}