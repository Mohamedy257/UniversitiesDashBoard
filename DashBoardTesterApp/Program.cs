using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BusinessLayer;

namespace DashBoardTesterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = UniversityService.Obj.GetAllUniversitiesFilters();
            foreach (var item in data)
            {
                Console.WriteLine(item);
               
            }
            Console.ReadKey();
        }
    }
}
