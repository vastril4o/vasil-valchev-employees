namespace Employee.Data.Models.Employee
{
    public class EmployeeProject
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public Employee Employee { get; set; }
        public Project.Project Project { get; set; }
    }
}
