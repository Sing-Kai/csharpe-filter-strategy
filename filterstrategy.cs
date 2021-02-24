using System;
using System.Collections.Generic;
using System.Linq;

namespace OnSiteReportLibrary.src.Filter
{
    public class FilterContext
    {
        private List<FullOnSite> _clockedInData { get; set; }

        public FilterContext(List<FullOnSite> data)
        {
            _clockedInData = GetClockedInData(data);
        }

        public List<FullOnSite> GetClockedInData()
        {
            return _clockedInData;
        }

        private List<FullOnSite> GetClockedInData(List<FullOnSite> data)
        {
            return Filter(new ClockedInFilterStrategy(), data);
        }

        public List<FullOnSite> GetOnSiteClockedInData(List<FullOnSite> data)
        {
            return Filter(new ClockedInFilterStrategy(), OnSiteFilter(data));
        }

        public List<FullOnSite> Filter(string department)
        {
            switch(department){
                case "Admin":
                    return Filter(new DeptAFilterStrategy(), OnSiteFilter(_clockedInData));
                case "Pow":
                    return Filter(new DeptBFilterStrategy(), OnSiteFilter(_clockedInData));
                case "Eng":
                    return Filter(new DeptCFilterStrategy(), OnSiteFilter(_clockedInData));
                case "Cea":
                    return Filter(new DeptDFilterStrategy(), OnSiteFilter(_clockedInData));
                case "AllDeptOnSite":
                    return OnSiteFilter(_clockedInData);
                case "Misc":
                    return OffSiteFilter(_clockedInData);
                default:
                    Console.WriteLine("no data found");
                    return new List<FullOnSite>();
            }
        }

        private List<FullOnSite> Filter(IFilterable f, List<FullOnSite> data)
        {
            return f.Fitler(data);
        }

        private List<FullOnSite> OnSiteFilter(List<FullOnSite> data)
        {
            var m = new OnSiteFilterStrategy();

            return m.Fitler(data);
        }

        private List<FullOnSite> OffSiteFilter(List<FullOnSite> data)
        {
            var m = new OffSiteFilterStrategy();

            return m.Fitler(data);
        }
    }


    public interface IFilterable
    {
        List<FullOnSite> Fitler(List<FullOnSite> data);
    }


    public class ClockedInFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var clockedIn = data.FindAll(d => d.ClockOut == null).ToList();

            return clockedIn;
        }
    }

    public class DeptAFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(d => d.DepartmentCode == "A").ToList();

            return filteredData;
        }
    }

    public class DeptBFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(d => d.DepartmentCode == "B").ToList();

            return filteredData;
        }
    }

    public class DeptCFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(d => d.DepartmentCode == "C").ToList();

            return filteredData;
        }
    }

    public class DeptDFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(d => d.DepartmentCode == "D").ToList();

            return filteredData;
        }
    }

    public class OffSiteFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(d => d.DeviceName.Contains("Mobile") || d.DeviceName.Contains("PC")).ToList();

            return filteredData;
        }
    }
    
    public class OnSiteFilterStrategy : IFilterable
    {
        public List<FullOnSite> Fitler(List<FullOnSite> data)
        {
            var filteredData = data.FindAll(
                               d => d.DeviceName.Contains("Location A") 
                               || d.DeviceName.Contains("Location B")
                               || d.DeviceName.Contains("Location C")
                               || d.DeviceName.Contains("?")
                               ).ToList();

            return filteredData;
        }
    }

}
