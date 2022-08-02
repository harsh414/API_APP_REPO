using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.Entities;
using Application.DataAccess;

namespace API_APP.Controllers
{
    public class EmployeeController : ApiController
    {
        IDataAccess<Employee, int> empDbServ;

        public EmployeeController(IDataAccess<Employee, int> empDbServ)
        {
            this.empDbServ = empDbServ;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var employees = empDbServ.Get();
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var employees = empDbServ.Get(id);
                if (employees == null)
                {
                    return NotFound();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post(Employee emp)
        {
            try
            {
                // Check for Model Validation
                if (ModelState.IsValid)
                {
                    var Response = empDbServ.Create(emp);
                    return Ok(Response);
                }
                else
                {
                    // Send the Error Response to the Client
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Ocurred while posting the request {ex.Message}");
            }
        }

        public IHttpActionResult Put(int id, Employee emp)
        {
            try
            {
                // Check for Model Validation
                if (ModelState.IsValid)
                {
                    var Response = empDbServ.Update(id, emp);
                    return Ok(Response);
                }
                else
                {
                    // Send the Error Response to the Client
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Ocurred while posting the request {ex.Message}");
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var employee = empDbServ.Delete(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
