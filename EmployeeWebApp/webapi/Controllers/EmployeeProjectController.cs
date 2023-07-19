using AutoMapper;
using Employee.CQRS.Employee.Commands;
using Employee.CQRS.Employee.Models;
using Employee.CQRS.Employee.Queries;
using Employee.CQRS.Project.Commands;
using Employee.Data.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Employee.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public EmployeeProjectController(ILogger<EmployeeProjectController> logger, IWebHostEnvironment hostingEnvironment, IMapper mapper) : base(logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProjectModel>>> GetAllEmployeeProjectLongest()
        {
            var response = await Mediator.Send(new GetAllEmployeeProjectLongestQuery());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EmployeeProjectModel>>> PostEmployeeProject(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            string filePath = null;
            if (file.Length > 0)
            {
                filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            await Mediator.Send(new AddCsvCommand() { file = filePath });

            return Ok();
        }
    }
}