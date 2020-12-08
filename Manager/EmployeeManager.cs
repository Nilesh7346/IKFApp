using System;
using System.Collections.Generic;

namespace IKFApp
{
    public class EmployeeManager
    {
        private EmployeeRepository employeeRepository = null;

        public EmployeeManager(string dbConnection)
        {
            this.employeeRepository = new EmployeeRepository(dbConnection);
        }

        /// <summary>
        /// Adds the employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Add(Employee employee)
        {
            return this.employeeRepository.Add(employee);
        }

        /// <summary>
        /// Update the employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Update(Employee employee)
        {
            return this.employeeRepository.Update(employee);
        }

        /// <summary>
        /// Delete the employeee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return this.employeeRepository.Delete(id);
        }

        /// <summary>
        /// Gets the employees list
        /// </summary>
        /// <returns></returns>
        public IList<Employee> Get()
        {
            return this.employeeRepository.Get();
        }
    }
}
