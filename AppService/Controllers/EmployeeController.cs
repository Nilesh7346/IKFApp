using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IKFApp
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IConfiguration configuration;
        EmployeeManager employeeManager = null;

        public EmployeeController(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
            string dbConnectionString = configuration.GetConnectionString("DBConnectionString");
            employeeManager = new EmployeeManager(dbConnectionString);
        }

        [HttpPost]
        public bool Add([FromBody] Employee employee)
        {
            return employeeManager.Add(employee);
        }

        [HttpPatch]
        public bool Update([FromBody] Employee employee)
        {
            return employeeManager.Update(employee);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return employeeManager.Delete(id);
        }

        [HttpGet]
        public IList<Employee> Get()
        {
            return employeeManager.Get();
        }
    }
}
