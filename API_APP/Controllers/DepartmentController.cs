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
    public class DepartmentController : ApiController
    {
        IDataAccess<Department, int> deptDbServ;
        public DepartmentController(IDataAccess<Department, int> Serv)
        {
            deptDbServ = Serv;
        }


        public IHttpActionResult Get()
        {
            try
            {
                var departments = deptDbServ.Get();
                return Ok(departments);
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
                var department = deptDbServ.Get(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post(Department dept)
        {
            try
            {
                // Check for Model Validation
                if (ModelState.IsValid)
                {
                    var Response = deptDbServ.Create(dept);
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

        public IHttpActionResult Put(int id, Department dept)
        {
            try
            {
                // Check for Model Validation
                if (ModelState.IsValid)
                {
                    var Response = deptDbServ.Update(id, dept);
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
                var department = deptDbServ.Delete(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}