using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Application.Entities;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace API_APP_WEBAPP
{
    public partial class EmpRESTClient : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new HttpClient();
            if (!IsPostBack)
            {
                var stringResponse = client.GetStringAsync("https://localhost:44377/api/Department").Result;
                List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(stringResponse);
                foreach (var dpt in departments)
                {
                    ListItem li = new ListItem(dpt.DeptName, dpt.DeptNo.ToString());
                    ddlDepart.Items.Add(li);
                }
                LoadData();
            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            txteno.Text = 0.ToString();
            txtename.Text = String.Empty;
            txtdesignation.Text = String.Empty;
            txtsalary.Text = 0.ToString();
            txtdno.Text = 0.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Employee emp = new Employee()
                {
                    EmpNo = Convert.ToInt32(txteno.Text),
                    EmpName = txtename.Text,
                    Designation = txtdesignation.Text,
                    Salary = Convert.ToInt32(txtsalary.Text),
                    DeptNo = Convert.ToInt32(txtdno.Text)
                };
                var jsonData = JsonConvert.SerializeObject(emp);

                var stringDataToPost = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = client.PostAsync("https://localhost:44377/api/Employee", stringDataToPost).Result;

                lblstatus.Text = response.Content.ReadAsStringAsync().Result;
                LoadData();
                

            }
            catch (Exception ex)
            {
                lblstatus.Text = ex.Message;
            }
        }

        protected void gvEmp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
          
        }

        private void LoadData()
        {
            var stringResponse = client.GetStringAsync("https://localhost:44377/api/Employee").Result;
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(stringResponse);
            gvEmp.DataSource = employees;
            gvEmp.DataBind();
        }

        protected void gvEmp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // REad the DeptNo for selected row when the 'Delete' buton is clicked
                var empno = Convert.ToInt32(gvEmp.Rows[e.RowIndex].Cells[0].Text);
                var response = client.DeleteAsync("https://localhost:44377/api/Employee/" + empno).Result;

                LoadData();
            }
            catch (Exception ex)
            {
                lblstatus.Text = ex.Message;
            }
        }

        protected void gvEmp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                Employee emp = new Employee();
                emp.EmpNo = Convert.ToInt32(gvEmp.Rows[e.RowIndex].Cells[0].Text);
                emp.EmpName = (gvEmp.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
                emp.Designation = (gvEmp.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
                emp.Salary = Convert.ToInt32((gvEmp.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text);
                emp.DeptNo = Convert.ToInt32((gvEmp.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox).Text);

                var jsonData = JsonConvert.SerializeObject(emp);
                var stringDataToPost = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = client.PutAsync("https://localhost:44377/api/Employee/" + emp.EmpNo, stringDataToPost).Result;


                gvEmp.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                lblstatus.Text = ex.Message;
            }
        }

        protected void gvEmp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmp.EditIndex = e.NewEditIndex;


            // read the cells collection for a selected row
            var cells = gvEmp.Rows[e.NewEditIndex].Cells;
            // show data in text boxes
            txteno.Text = cells[0].Text;
            txtename.Text = cells[1].Text;
            txtdesignation.Text = cells[2].Text;
            txtsalary.Text = cells[3].Text;
            txtdno.Text = cells[4].Text;
            LoadData();
        }

        protected void gvEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cells = gvEmp.SelectedRow.Cells;
            txteno.Text = cells[0].Text;
            txtename.Text = cells[1].Text;
            txtdesignation.Text = cells[2].Text;
            txtsalary.Text = cells[3].Text;
            txtdno.Text = cells[4].Text;
        }

        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdno.Text = ddlDepart.SelectedValue;
        }
    }
}