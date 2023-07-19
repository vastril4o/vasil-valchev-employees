using AutoMapper;
using Employee.Data.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Employee.CQRS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<EmployeeProject, Data.Models.Employee.Employee>()
                .ForMember(x => x.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<EmployeeProject, Data.Models.Project.Project>()
                .ForMember(x => x.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
