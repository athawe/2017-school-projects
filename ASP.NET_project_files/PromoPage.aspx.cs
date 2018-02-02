using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Aaron_eCommerce2017
{
    public partial class PromoPage : System.Web.UI.Page
    {
        //string connection string
        string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";
        //internal discount variable, set to 10% discount
        decimal discount = 0.9m;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve highest costing item
            percentOffHighest();

        }
        //Function to find most expensive item in Product database, and set up Promo Page
        protected void percentOffHighest()
        {
            // create the objects needed for CRUD
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            try
            {
                // create a new data set object called ds
                ds = new DataSet();
                // create a connection to the database called connectFill
                connectFill = new SqlConnection(dbConnect);

                // create SQL string to select customer record
                string sqlString = "SELECT * FROM Products WHERE Price = (SELECT MAX(Price) FROM Products)";
                
                // create new SQL command object based on SQL string and connection
                scmd = new SqlCommand(sqlString, connectFill);

                // add the parameter to SQL string and validate
                //scmd.Parameters.AddWithValue("@ProdID", txtProductNumber.Text);

                // create a new SQL data adapter to retrieve the data and
                // fill the data set
                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = scmd;
                sqlDataAdapter.Fill(ds, "Products");
            }
            catch (Exception ex)
            {
                //lblMessages.Text = ex.Message;
                //DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            if (ds.Tables["Products"].Rows.Count >= 1)
            {
                decimal originalPrice =  Convert.ToDecimal(ds.Tables["Products"].Rows[0]["Price"]);
                String name = ds.Tables["Products"].Rows[0]["Picture"].ToString().TrimEnd(' ');
                name = name.Remove(name.Length - 4);
                lblName.Text = name;
                lblDesc.Text = ds.Tables["Products"].Rows[0]["Description"].ToString();
                imgPictures.ImageUrl = "~/Images/" + ds.Tables["Products"].Rows[0]["Picture"].ToString();
                lblOldPrice.Text = "Old Price: " + ds.Tables["Products"].Rows[0]["Price"].ToString();

                decimal salePrice = originalPrice * discount;
                lblNewPrice.Text = "New Price: " + (salePrice).ToString("C");
                
            }
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }
        // **************************************************************
        // method releases all database resources that have been assigned
        private static void DisposeResources(ref SqlDataAdapter sqlDataAdapter,
            ref DataSet ds,
            ref SqlConnection connectFill,
            ref SqlConnection connectCmd,
            ref SqlCommand cmd,
            ref SqlCommand scmd)
        {
            if (sqlDataAdapter != null)
                sqlDataAdapter.Dispose();
            if (ds != null)
                ds.Dispose();
            if (connectFill != null)
                connectFill.Dispose();
            if (connectCmd != null)
                connectCmd.Dispose();
            if (cmd != null)
                cmd.Dispose();
            if (scmd != null)
                scmd.Dispose();
        }
    }
}