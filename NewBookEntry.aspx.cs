using BLL;
using QuickSell;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACCCounter
{
    public partial class NewBookEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    txtBookId.Enabled = true;

                    loadBookInformationToGrid();
                    btnUpdate.Enabled = false;
                    LoadBookType();
                    LoadClass();
                    LoadSupplierCompany();
                }
            }
            catch (Exception)
            {
                 
            }
        }

        private void LoadBookType()
        {
            LoadComboData loadClass = new LoadComboData();
            loadClass.BookType(ddlBookType);
        }

        private void LoadClass()
        {
            ddlClass.DataSource = Li_ClassesManager.GetAllLi_Classess();
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassID";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("--select--", ""));
        }

        private void LoadSupplierCompany()
        {
            li_CompanyManager company=new li_CompanyManager();
            ddlCompany.DataSource =  company.GetAllCompany().Tables[0];
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("--select", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {


                li_BookInformation _bookInformation = new li_BookInformation();

                _bookInformation.BookName = txtBookName.Text;

                _bookInformation.ClassID = Int32.Parse(ddlClass.SelectedValue.ToString());
                _bookInformation.CreatedBy = int.Parse(Session["UserID"].ToString());
                _bookInformation.CreatedDate = DateTime.Now;
                _bookInformation.ModifiedBy = int.Parse(Session["UserID"].ToString());
                _bookInformation.ModifiedDate = DateTime.Now;
                _bookInformation.BookType = int.Parse(ddlBookType.SelectedValue.ToString());

                object bookRow = getTotalBookRow();

                string nextID = bookRow.ToString();

                string BookNewID = txtBookId.Text;           

                _bookInformation.BookID = BookNewID;
                _bookInformation.CompanyId = int.Parse(ddlCompany.SelectedValue);
                _bookInformation.CompName = ddlCompany.SelectedValue.ToString();

                li_BookInformationManager.Insert_BookInformation(_bookInformation);

                loadBookInformationToGrid();
                ClearAll();

                string message = "Saved successfully.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += Request.Url.AbsoluteUri;
                script += "'; }";
                //  ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "SuccessMessage", script, true);

            }
            catch (Exception)
            {
                
            }
        }


        public static int getTotalBookRow()
        {
            DataAccessObject dsa = new DataAccessObject();
            using (SqlConnection connection = new SqlConnection(dsa.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SMPM_GetNext_Book", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@lastin", SqlDbType.Int).Direction = ParameterDirection.Output;

                connection.Open();

                int result = cmd.ExecuteNonQuery();

                return (int)cmd.Parameters["@lastin"].Value;
            }
        }

        private void loadBookInformationToGrid()
        {
            try
            {
                gvwBookEntry.DataSource = li_BookInformationManager.GetAll_BookInformations(string.Empty).Tables[0];
                gvwBookEntry.DataBind();
            }
            catch (Exception)
            {
                
            }
        }

        private void ClearAll()
        {
            // cmbPublisherComName.SelectedIndex = -1;
            ddlClass.SelectedIndex = -1;
            ddlBookType.SelectedIndex = -1;
            txtBookName.Text = "";
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvwBookEntry.DataSource = li_BookInformationManager.GetAll_BookInformationsByBookName(txtSearch.Text).Tables[0];
                gvwBookEntry.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvwBookEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvwBookEntry.PageIndex = e.NewPageIndex;
                gvwBookEntry.DataSource = li_BookInformationManager.GetAll_BookInformations(string.Empty).Tables[0];
                gvwBookEntry.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                li_BookInformation _bookInformation = new li_BookInformation();

                _bookInformation.BookID = txtBookId.Text;
                _bookInformation.BookName = txtBookName.Text;
                _bookInformation.ClassID = Int32.Parse(ddlClass.SelectedValue.ToString());
                _bookInformation.ModifiedBy = int.Parse(Session["UserID"].ToString());
                _bookInformation.ModifiedDate = DateTime.Now;
                _bookInformation.BookType = int.Parse(ddlBookType.SelectedValue.ToString());
                _bookInformation.CompanyId = int.Parse(ddlCompany.SelectedValue);

                li_BookInformationManager.Update_BookInformation(_bookInformation);

                loadBookInformationToGrid();
                ClearAll();

                string message = "Update successfully.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "');";
                script += "window.location = '";
                script += Request.Url.AbsoluteUri;
                script += "'; }";
                //  ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "SuccessMessage", script, true);
            }
            catch (Exception ex)
            {
               
            }
            txtBookId.Enabled = true;
        }

        protected void gvwBookEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBookId.Enabled = false;
                GridViewRow row = gvwBookEntry.SelectedRow;
                string BookId = row.Cells[1].Text;

               DataTable dtgvw= li_BookInformationManager.GetAll_BookInformations(BookId).Tables[0];

               txtBookId.Text = dtgvw.Rows[0]["BookID"].ToString();
               txtBookName.Text = dtgvw.Rows[0]["BookName"].ToString();
               ddlClass.SelectedValue = dtgvw.Rows[0]["ClassID"].ToString();
               ddlBookType.SelectedValue = dtgvw.Rows[0]["BookTypeID"].ToString();
               ddlCompany.SelectedValue = dtgvw.Rows[0]["CompanyId"].ToString();

               btnUpdate.Enabled = true;
               btnSave.Enabled = false;
            }
            catch (Exception)
            {

            }
        }

        protected void gvwBookEntry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(sender as GridView, "Select$" + e.Row.RowIndex.ToString()));
        }
    }
}