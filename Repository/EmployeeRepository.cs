using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace IKFApp
{
    /// <summary>
    /// The employee
    /// </summary>
    public class EmployeeRepository
    {
        private string dbConnectionString=string.Empty;
        public EmployeeRepository(string dbConnection)
        {
            dbConnectionString = dbConnection;
        }
        /// <summary>
        /// Adds the employee record in db
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Add(Employee employee)
        {
            try
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext(dbConnectionString))
                {
                    EmployeeRecord employeeRecord = new EmployeeRecord();
                    employeeRecord.Designation = employee.Designation;
                    employeeRecord.DOB = employee.DOB;
                    employeeRecord.Name = employee.Name;
                    employeeRecord.Skills = employee.Skills.ToString();
                    dbContext.EmployeesRecords.Add(employeeRecord);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Updates employee record from db
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Update(Employee employee)
        {
            try
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext(dbConnectionString))
                {

                    EmployeeRecord employeeRecord = dbContext.EmployeesRecords.FirstOrDefault(item => item.Id == employee.Id);
                    if (employeeRecord != null)
                    {
                        employeeRecord.Designation = employee.Designation;
                        employeeRecord.DOB = employee.DOB;
                        employeeRecord.Name = employee.Name;
                        employeeRecord.Skills = employee.Skills.ToString();
                        dbContext.SaveChanges();
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Deletes the employee record based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext(dbConnectionString))
                {

                    EmployeeRecord deletedEmployeeRecord = dbContext.EmployeesRecords.FirstOrDefault(item => item.Id == id);
                    if (deletedEmployeeRecord != null)
                    {
                        dbContext.Remove(deletedEmployeeRecord);
                        dbContext.SaveChanges();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Get the list of all employees from db
        /// </summary>
        /// <returns></returns>
        public IList<Employee> Get()
        {
            IList<Employee> employees = new List<Employee>();
            try
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext(dbConnectionString))
                {

                    IList<EmployeeRecord> employeeRecords = dbContext.EmployeesRecords.ToList();
                    if (employeeRecords?.Count() > 0)
                    {
                        employees = employeeRecords.Select(itemDBRecord => new Employee()
                        {
                            Designation = itemDBRecord.Designation,
                            Id = itemDBRecord.Id,
                            DOB = itemDBRecord.DOB,
                            Name = itemDBRecord.Name,
                            Skills = (Skills)(Enum.Parse(typeof(Skills),itemDBRecord.Skills))
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return employees;
        }

    }
}
