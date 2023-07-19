using Employee.CQRS.Employee.Models;
using Employee.Data.Models.Employee;

namespace Employee.Service
{
    public static class FileService
    {
        public static List<EmployeeProject> readEmployeeProjectCsv(string file)
        {
            List<EmployeeProject> list = 
                File.ReadAllLines(file).Select(v => FromCsv(v)).ToList();

            EmployeeProject FromCsv(string csvLine)
            {
                string[] values = csvLine.Split(',');
                EmployeeProject model = new EmployeeProject();

                model.EmployeeId = Convert.ToInt32(values[0]);
                model.ProjectId = Convert.ToInt32(values[1]);

                DateTime dtFrom;
                if (DateTime.TryParse(values[2], out dtFrom))
                {
                    model.DateFrom = dtFrom;
                }

                DateTime dtTo;
                if (DateTime.TryParse(values[3], out dtTo))
                {
                    model.DateTo = dtTo;
                }

                return model;
            }

            return list;
        }
    }
}