using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model;

namespace DashBoard.BusinessLayer
{
    public class UniversityService
    {
        private static DashboarEntities dashboarEntities;
        private UniversityService()
        {
            Obj = this;
        }
        static UniversityService()
        {
            new UniversityService();
            dashboarEntities = new DashboarEntities();
        }
        public static UniversityService Obj { get; set; }

        //Get All Univercity Data
        public List<DC_UNIVERSITIES> GetAllUniversitiesData()
        {
            return dashboarEntities.DC_UNIVERSITIES.ToList();
        }

        //Get All University Filters
        public List<string> GetAllUniversitiesFilters()
        {
            return dashboarEntities.DC_UNIVERSITIES.Select(s => s.UniversityName).Distinct().ToList(); 
        }

        //Get Selected Universities Data
        public List<DC_UNIVERSITIES> GetAllUniversitiesFilters(List<string> universitiesList)
        {
            return dashboarEntities.DC_UNIVERSITIES.Where(q => universitiesList.Contains(q.UniversityName)).ToList();
        }




    }
}
