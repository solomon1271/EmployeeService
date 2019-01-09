using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public IEnumerable<Employee> AllEmployees()
        {
            using(EmployeeDBEntities db = new EmployeeDBEntities())
            {
                return db.Employees.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage EmployeeWithId(int id)
        {
            using (EmployeeDBEntities db = new EmployeeDBEntities())
            {
                var emp = db.Employees.FirstOrDefault(x => x.ID == id);
                if(emp == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "the employee with id " + id + " does not exist");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmployee([FromBody]Employee emp)
        {
            try
            {
                using (EmployeeDBEntities db = new EmployeeDBEntities())
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + emp.ID.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Edit([FromUri]Employee emp, int id)
        {
            try
            {
                using(EmployeeDBEntities db = new EmployeeDBEntities())
                {
                    var e = db.Employees.FirstOrDefault(x => x.ID == id);
                    if(e == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The employee with the ID " + " does not found to be edited.");
                    }
                    else
                    {
                        e.FirstName = emp.FirstName;
                        e.LastName = emp.LastName;
                        e.Gender = emp.Gender;
                        e.Salary = emp.Salary;

                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, e);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Remove(int id)
        {
            try
            {
                using(EmployeeDBEntities db = new EmployeeDBEntities())
                {
                    var e = db.Employees.FirstOrDefault(x => x.ID == id);
                    if(e == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The employee with the ID " + " does not found to be edited.");
                    }
                    else
                    {
                        db.Employees.Remove(e);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "The employee with ID " + id + " is deleted.");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }            

        }
    }
}
