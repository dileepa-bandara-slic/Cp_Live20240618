using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Life_Authorized_ClientHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //populate policies for username from pol_det_for_web table
            LifeCustomer customer = new LifeCustomer();
            string message = customer.getSavedPolicies(Page.User.Identity.Name, GridLifePols);
            if (message != "success")
            {
                PolNumValidator.IsValid = false;
                PolNumValidator.ErrorMessage = message;
            }

            int polCount = GridLifePols.Rows.Count;
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

            if (GridLifePols.Rows.Count > 0)
            {
                lblLifePols.Visible = true;
            }
            else
            {
                lblLifePols.Visible = false;
            }

        }
        load();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            LifeCustomer customer = new LifeCustomer();
            string message = customer.getPolicies(Page.User.Identity.Name, txtPolNum.Text.Trim(), GridLifePols);
            if (message != "success")
            {
                PolNumValidator.IsValid = false;
                PolNumValidator.ErrorMessage = message;
            }
            else
            {
                txtPolNum.Text = "";
            }
            int polCount = GridLifePols.Rows.Count;
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
            }
            else
            {
                lblCurrPols.Visible = false;
            }

            if (GridLifePols.Rows.Count > 0)
            {
                lblLifePols.Visible = true;
            }
            else
            {
                lblLifePols.Visible = false;
            }

            load();
            load();
        }
    }

    private void load()
    {
        if (GridLifePols.Rows.Count > 0)
        {
            GridLifePols.HeaderRow.Cells[0].Attributes.Add("data-class", "expand");
            GridLifePols.HeaderRow.Cells[2].Attributes.Add("data-hide", "phone");
            GridLifePols.HeaderRow.Cells[3].Attributes.Add("data-hide", "phone");
            GridLifePols.HeaderRow.Cells[4].Attributes.Add("data-hide", "phone");
            GridLifePols.HeaderRow.Cells[5].Attributes.Add("data-hide", "phone,tablet");
            GridLifePols.HeaderRow.Cells[6].Attributes.Add("data-hide", "phone,tablet");
            GridLifePols.HeaderRow.Cells[7].Attributes.Add("data-hide", "phone,tablet");
            GridLifePols.HeaderRow.Cells[8].Attributes.Add("data-hide", "phone,tablet");

            GridLifePols.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        foreach (GridViewRow rw in GridLifePols.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblPolNo = (Label)rw.FindControl("lblPolNum");
                    string pol_id = lblPolNo.Text;


                    LinkButton lnk_btn = (LinkButton)rw.Cells[6].Controls[0];

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

    protected void GridLifePols_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        load();
    }

    protected void GridLifePols_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //LinkButton deleteButton = (LinkButton)GridMotPols.Rows[e.RowIndex].Cells[8].Controls[0];
        LifeCustomer customer = new LifeCustomer();
        string message = customer.deleteSavedPolicy(Page.User.Identity.Name, GridLifePols, e.RowIndex);
        if (message != "success")
        {
            PolNumValidator.IsValid = false;
            PolNumValidator.ErrorMessage = message;
        }
        int polCount = GridLifePols.Rows.Count;
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
        if (GridLifePols.Rows.Count > 0)
        {
            lblLifePols.Visible = true;
        }
        else
        {
            lblLifePols.Visible = false;
        }
        load();
    }
}