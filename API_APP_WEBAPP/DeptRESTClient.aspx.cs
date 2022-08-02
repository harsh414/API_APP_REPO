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
    public partial class DeptRESTClient : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new HttpClient();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            txtdno.Text = 0.ToString();
            txtdname.Text = "";
            txtloc.Text = "";
            txtcap.Text = 0.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                Department department = new Department()
                {
                    DeptNo = Convert.ToInt32(txtdno.Text),
                    DeptName = txtdname.Text,
                    Location = txtloc.Text,
                    Capacity = Convert.ToInt32(txtcap.Text)
                };

                var jsonData = JsonConvert.SerializeObject(department);
             
                var stringDataToPost = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = client.PostAsync("https://localhost:44377/api/Department", stringDataToPost).Result;



                lblstatus.Text = response.Content.ReadAsStringAsync().Result;
                LoadData();
            }
            catch (Exception ex)
            {
                lblstatus.Text = $"Error Occurred {ex.Message}";
            }
        }

        private void LoadData()
        {
            var stringResponse = client.GetStringAsync("https://localhost:44377/api/Department").Result;
            List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(stringResponse);
            gvDept.DataSource = departments;
            gvDept.DataBind();
        }

        protected void gvDept_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // REad the DeptNo for selected row when the 'Delete' buton is clicked
                var deptno = Convert.ToInt32(gvDept.Rows[e.RowIndex].Cells[0].Text);
                var response = client.DeleteAsync("https://localhost:44377/api/Department/"+deptno).Result;

                LoadData();
            }
            catch (Exception ex)
            {
                lblstatus.Text = ex.Message;
            }
        }

        protected void gvDept_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // 1. Read Cell Value
                // a. Read the DeptNo
                Department dept = new Department();
                dept.DeptNo = Convert.ToInt32(gvDept.Rows[e.RowIndex].Cells[0].Text);
                
                dept.DeptName = (gvDept.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
                dept.Location = (gvDept.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
                dept.Capacity = Convert.ToInt32((gvDept.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text);

                var jsonData = JsonConvert.SerializeObject(dept);
                var stringDataToPost = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = client.PutAsync("https://localhost:44377/api/Department/"+ dept.DeptNo, stringDataToPost).Result;
               

                gvDept.EditIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                lblstatus.Text = ex.Message;
            }
        }

        protected void gvDept_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDept.EditIndex = e.NewEditIndex;


            // read the cells collection for a selected row
            var cells = gvDept.Rows[e.NewEditIndex].Cells;
            // show data in text boxes
            txtdno.Text = cells[0].Text;
            txtdname.Text = cells[1].Text;
            txtloc.Text = cells[2].Text;
            txtcap.Text = cells[3].Text;

            LoadData();
        }

        protected void gvDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cells = gvDept.SelectedRow.Cells;
            txtdno.Text = cells[0].Text;
            txtdname.Text = cells[1].Text;
            txtloc.Text = cells[2].Text;
            txtcap.Text = cells[3].Text;
        }
    }
}