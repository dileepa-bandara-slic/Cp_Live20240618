using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_Authorized_ClientHome : System.Web.UI.Page
{
    EncryptDecrypt dc = new EncryptDecrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //populate policies for username from pol_det_for_web table
            ULCustomer customer = new ULCustomer();
            string message = customer.getSavedPolicies(Page.User.Identity.Name, GridMotPols, GridGenPols, GridPendPols);
            if (message != "success")
            {
                PolNumValidator.IsValid = false;
                PolNumValidator.ErrorMessage = message;
            }

            int polCount = GridMotPols.Rows.Count + GridGenPols.Rows.Count;
            if (polCount > 0)
            {
                if (polCount == 1)
                {
                    lblMesg.Text = polCount.ToString() + " policy added to your account.";
                }
                else
                {
                    lblMesg.Text = polCount.ToString() + " policies added to your account.";
                }
                litLabel.Text = "Enter another policy number";
            }
            else
            {
                lblMesg.Text = "No policies are added to your account yet.";
                litLabel.Text = "Enter your policy number";
            }

            if (polCount > 0)
            {
                lblCurrPols.Visible = true;
                lblMesg.Text = lblMesg.Text + "<br/>Please click on + sign on the grid to go to renew policy option";
            }
            else
            {
                lblCurrPols.Visible = false;
            }
            if (GridMotPols.Rows.Count > 0)
            {
                lblMotPols.Visible = true;
            }
            else
            {
                lblMotPols.Visible = false;
            }
            if (GridGenPols.Rows.Count > 0)
            {
                lblGenPols.Visible = true;
            }
            else
            {
                lblGenPols.Visible = false;
            }
            if (GridPendPols.Rows.Count > 0)
            {
                lblProps.Visible = true;
            }
            else
            {
                lblProps.Visible = false;
            }

        }
        load("M");
        load("G");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ULCustomer customer = new ULCustomer();
            string message = customer.getPolicies(Page.User.Identity.Name, txtPolNum.Text.Trim(), ddlPolType.SelectedValue.ToString(), GridMotPols, GridGenPols);
            if (message != "success")
            {
                PolNumValidator.IsValid = false;
                PolNumValidator.ErrorMessage = message;
            }
            else
            {
                txtPolNum.Text = "";
                ddlPolType.SelectedIndex = 0;
            }
            int polCount = GridMotPols.Rows.Count + GridGenPols.Rows.Count;
            if (polCount > 0)
            {
                if (polCount == 1)
                {
                    lblMesg.Text = polCount.ToString() + " policy added to your account.";
                }
                else
                {
                    lblMesg.Text = polCount.ToString() + " policies added to your account.";
                }
                litLabel.Text = "Enter another policy number";
            }

            if (polCount > 0)
            {
                lblCurrPols.Visible = true;
                lblMesg.Text = lblMesg.Text + "<br/>Please click on + sign on the grid to go to renew policy option";
            }
            else
            {
                lblCurrPols.Visible = false;
            }
            if (GridMotPols.Rows.Count > 0)
            {
                lblMotPols.Visible = true;
            }
            else
            {
                lblMotPols.Visible = false;
            }
            if (GridGenPols.Rows.Count > 0)
            {
                lblGenPols.Visible = true;
            }
            else
            {
                lblGenPols.Visible = false;
            }

            load("M");
            load("G");
        }
    }

    protected void GridMotPols_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //LinkButton deleteButton = (LinkButton)GridMotPols.Rows[e.RowIndex].Cells[8].Controls[0];
        ULCustomer customer = new ULCustomer();
        string message = customer.deleteSavedPolicy(Page.User.Identity.Name, GridMotPols, e.RowIndex, "M");
        if (message != "success")
        {
            PolNumValidator.IsValid = false;
            PolNumValidator.ErrorMessage = message;
        }
        int polCount = GridMotPols.Rows.Count + GridGenPols.Rows.Count;
        if (polCount > 0)
        {
            if (polCount == 1)
            {
                lblMesg.Text = polCount.ToString() + " policy added to your account.";
            }
            else
            {
                lblMesg.Text = polCount.ToString() + " policies added to your account.";
            }
            litLabel.Text = "Enter another policy number";
        }
        else
        {
            lblMesg.Text = "No policies are added to your account yet.";
            litLabel.Text = "Enter your policy number";

        }

        if (polCount > 0)
        {
            lblCurrPols.Visible = true;
        }
        else
        {
            lblCurrPols.Visible = false;
        }
        if (GridMotPols.Rows.Count > 0)
        {
            lblMotPols.Visible = true;
        }
        else
        {
            lblMotPols.Visible = false;
        }
        if (GridGenPols.Rows.Count > 0)
        {
            lblGenPols.Visible = true;
        }
        else
        {
            lblGenPols.Visible = false;
        }

        load("M");
    }

    protected void checkPolNum(object source, ServerValidateEventArgs args)
    {
        int status = -1;
        string message = "";

        InfoValidator validator = new InfoValidator();
        validator.validatePolNumber(txtPolNum.Text, out status, out message);

        if (status == 0)
        {
            args.IsValid = true;
            btnSubmit.Focus();
        }
        else
        {
            args.IsValid = false;
            PolNumValidator.ErrorMessage = message;
            txtPolNum.Focus();
        }
    }

    void load(string flag)
    {
        if (GridPendPols.Rows.Count > 0)
        {
            GridPendPols.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            GridPendPols.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            GridPendPols.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            GridPendPols.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone,tablet");
            GridPendPols.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone,tablet");

            GridPendPols.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (GridMotPols.Rows.Count > 0)
        {
            GridMotPols.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            GridMotPols.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            GridMotPols.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone");
            GridMotPols.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone");
            GridMotPols.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            GridMotPols.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");
            GridMotPols.HeaderRow.Cells[8].Attributes.Add("data-hide", "phone,tablet");
            GridMotPols.HeaderRow.Cells[9].Attributes.Add("data-hide", "phone,tablet");
            GridMotPols.HeaderRow.Cells[10].Attributes.Add("data-hide", "phone,tablet");
            GridMotPols.HeaderRow.Cells[11].Attributes.Add("data-hide", "phone,tablet");

            GridMotPols.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (GridGenPols.Rows.Count > 0)
        {
            GridGenPols.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            GridGenPols.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            GridGenPols.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone");
            GridGenPols.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone");
            GridGenPols.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            GridGenPols.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");
            GridGenPols.HeaderRow.Cells[8].Attributes.Add("data-hide", "phone,tablet");
            GridGenPols.HeaderRow.Cells[9].Attributes.Add("data-hide", "phone,tablet");
            GridGenPols.HeaderRow.Cells[10].Attributes.Add("data-hide", "phone,tablet");

            GridGenPols.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (flag == "M")
        {
            foreach (GridViewRow rw in GridMotPols.Rows)
            {
                if (rw.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        Label lblPolNo = (Label)rw.FindControl("lblPolNum");
                        string pol_id = lblPolNo.Text;


                        LinkButton lnk_btn = (LinkButton)rw.Cells[9].Controls[0];

                        if (lnk_btn != null)
                        {
                            lnk_btn.Attributes.Add("onclick", "return Confirm_to_delete('" + pol_id + "');");
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        else if (flag == "G")
        {
            foreach (GridViewRow rw in GridGenPols.Rows)
            {
                if (rw.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        Label lblPolNo = (Label)rw.FindControl("lblPolNum2");
                        string pol_id = lblPolNo.Text;


                        LinkButton lnk_btn = (LinkButton)rw.Cells[8].Controls[0];

                        if (lnk_btn != null)
                        {
                            lnk_btn.Attributes.Add("onclick", "return Confirm_to_delete('" + pol_id + "');");
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    protected void GridMotPols_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        load("M");
    }

    protected void GridGenPols_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        load("G");
    }

    protected void GridGenPols_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //LinkButton deleteButton = (LinkButton)GridMotPols.Rows[e.RowIndex].Cells[8].Controls[0];
        ULCustomer customer = new ULCustomer();
        string message = customer.deleteSavedPolicy(Page.User.Identity.Name, GridGenPols, e.RowIndex, "G");
        if (message != "success")
        {
            PolNumValidator.IsValid = false;
            PolNumValidator.ErrorMessage = message;
        }
        int polCount = GridGenPols.Rows.Count + GridMotPols.Rows.Count;
        if (polCount > 0)
        {
            if (polCount == 1)
            {
                lblMesg.Text = polCount.ToString() + " policy added to your account.";
            }
            else
            {
                lblMesg.Text = polCount.ToString() + " policies added to your account.";
            }
            litLabel.Text = "Enter another policy number";
        }
        else
        {
            lblMesg.Text = "No policies are added to your account yet.";
            litLabel.Text = "Enter your policy number";

        }

        if (polCount > 0)
        {
            lblCurrPols.Visible = true;
        }
        else
        {
            lblCurrPols.Visible = false;
        }
        if (GridMotPols.Rows.Count > 0)
        {
            lblMotPols.Visible = true;
        }
        else
        {
            lblMotPols.Visible = false;
        }
        if (GridGenPols.Rows.Count > 0)
        {
            lblGenPols.Visible = true;
        }
        else
        {
            lblGenPols.Visible = false;
        }
        load("G");
    }

    public string url_to_ecrypt(object id, object page)
    {
        return page.ToString().Trim() + "?" + enc("refN0=" + id.ToString());
    }

    protected string enc(string i)
    {

        string f = dc.Encrypt(i);
        return f;

    }

    protected void GridPendPols_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EncryptDecrypt en = new EncryptDecrypt();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow row = ((DataRowView)e.Row.DataItem).Row;
            String id = row.Field<String>("REF_NO");
            String sumAssured = row.Field<String>("SUM_ASSURD");

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("refN0", id.Trim());

            if (id.Contains("GTI"))
            {
                dc.Add("P0lNo", id.Trim());
            }
            if (id.Contains("MP"))
            {
                dc.Add("SA", sumAssured.Trim());
            }
            string link2 = en.url_encrypt("Quotation_Reprint.aspx", dc);

            HyperLink link = e.Row.Cells[0].Controls[0] as HyperLink;
            
            link.NavigateUrl = link2;// url_to_ecrypt(id.Trim(), "Quotation_Reprint.aspx");
            
        }
    }
}