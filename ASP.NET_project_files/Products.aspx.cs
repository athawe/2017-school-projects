using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Aaron_eCommerce2017
{
    public partial class Products : System.Web.UI.Page
    {
        //string webSiteData = HttpContext.Current.Server.MapPath(".") + @"\Data\Products\";
        //connect to database
        string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";
        //on page load function. currently unused
        protected void Page_Load(object sender, EventArgs e)
        {
            if (lstImages.Items.Count == 0)
            {
                populateImages();
            }
        }
        //Search the database for images, populate List Box
        protected void populateImages()
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
                string sqlString = "SELECT Picture FROM Products;";

                // create new SQL command object based on SQL string and connection
                scmd = new SqlCommand(sqlString, connectFill);

                // create a new SQL data adapter to retrieve the data and
                // fill the data set
                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = scmd;
                sqlDataAdapter.Fill(ds, "Products");
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            if (ds.Tables["Products"].Rows.Count > 0)
            {
                //Now populate the List Box with the Pictures
                txtPict.Text = ds.Tables["Products"].Rows[0]["Picture"].ToString();
                foreach(DataRow row in ds.Tables["Products"].Rows)
                {
                    lstImages.Items.Add(row.ItemArray[0].ToString());
                }
                lblMessages.Text = "";

                string firstImg = lstImages.Items[0].ToString();
                imgPictures.ImageUrl = "~/Images/" + firstImg;
                txtPict.Text = firstImg;                
            }
            else
                lblMessages.Text = "No Products Found!";

            // release all database resources (memory)
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }
        //clear out data fields
        protected void btnNewProduct_Click(object sender, EventArgs e)
        {
            flushData();
        }
        //updata a data entry in database
        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (txtProductNumber.Text != "")
            {
                // create the objects needed for CRUD
                SqlDataAdapter sqlDataAdapter = null;
                DataSet ds = null;
                SqlConnection connectFill = null;
                SqlConnection connectCmd = null;
                SqlCommand cmd = null;
                SqlCommand scmd = null;

                // open a connection to the database
                connectCmd = new SqlConnection(dbConnect);
                connectCmd.Open();

                // now make a change to the customer last name
                string sqlString = "UPDATE Products SET ManCode=@ManCode, Description=@Description, Picture=@Picture, QtyOnHand=@QtyOnHand, Price=@Price WHERE ProdID=@ProdID";

                int count = 0;
                try
                {
                    // create a SQL command object
                    cmd = new SqlCommand(sqlString, connectCmd);

                    // map the parameters into the placeholders in the SQL string
                    cmd.Parameters.AddWithValue("@ManCode", txtManufactCode.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Picture", txtPict.Text);
                    cmd.Parameters.AddWithValue("@QtyOnHand", txtQuan.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@ProdID", txtProductNumber.Text);

                    // execute the query
                    count = cmd.ExecuteNonQuery();
                    lblMessages.Text = "";
                }
                catch (Exception ex)
                {
                    // if the query fails for any reason
                    // record the error message and release all resources
                    lblMessages.Text = ex.Message;
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                }

                // release all database resources (memory)
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

            }
        }
        //delete a reference from database
        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // create the objects needed for CRUD
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            // open a connection to the database
            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            // define SQL string to delete customer by customer ID
            string sqlString = "DELETE FROM Products WHERE ProdID=@ProdID";

            // create an int variable to receive number of records deleted
            int count = 0;
            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@ProdID", txtProductNumber.Text);
                count = cmd.ExecuteNonQuery();
                flushData();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }
            // release all database resources (memory)
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

        }
        //Search the database for an entry based on ID
        protected void btnFindProduct_Click(object sender, EventArgs e)
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
                    string sqlString = "SELECT * FROM Products WHERE ProdID = @ProdID";

                    // create new SQL command object based on SQL string and connection
                    scmd = new SqlCommand(sqlString, connectFill);

                    // add the parameter to SQL string and validate
                    scmd.Parameters.AddWithValue("@ProdID", txtProductNumber.Text);

                    // create a new SQL data adapter to retrieve the data and
                    // fill the data set
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = scmd;
                    sqlDataAdapter.Fill(ds, "Products");
                }
                catch (Exception ex)
                {
                    lblMessages.Text = ex.Message;
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                }

                if (ds.Tables["Products"].Rows.Count == 1)
                {
                    txtManufactCode.Text = ds.Tables["Products"].Rows[0]["ManCode"].ToString();
                    txtDesc.Text = ds.Tables["Products"].Rows[0]["Description"].ToString();
                    txtPict.Text = ds.Tables["Products"].Rows[0]["Picture"].ToString();
                    txtQuan.Text = ds.Tables["Products"].Rows[0]["QtyOnHand"].ToString();
                    txtPrice.Text = ds.Tables["Products"].Rows[0]["Price"].ToString();
                    btnDeleteProduct.Enabled = true;
                    btnUpdateProduct.Enabled = true;
                    lblMessages.Text = "";
                }
                else
                    lblMessages.Text = "Product Number not found!";

                // release all database resources (memory)
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);           
        }
        //Add a new entry in the product database
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            // create the objects needed for CRUD
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            // open a connection to the database
            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            // create our SQL string with VALUE parameters
            string sqlString = "INSERT INTO Product (ManCode, Description, Picture, QtyOnHand, Price) VALUES (@ManCode, @Description, @Picture, @QtyOnHand, @Price)";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@ManCode", txtManufactCode.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@Picture", txtPict.Text);
                cmd.Parameters.AddWithValue("@QtyOnHand", txtQuan.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.ExecuteNonQuery();
                lblMessages.Text = "";
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            // get the primary key identity just inserted
            // this will be the customer number picked by the database engine
            string identRequest = "SELECT IDENT_CURRENT('Products') FROM Products";
            cmd = new SqlCommand(identRequest, connectCmd);
            int identValue = Convert.ToInt32(cmd.ExecuteScalar());

            // display the new customer number
            txtProductNumber.Text = identValue.ToString();

            // release all database resources (memory)
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }

        protected void lstImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPict.Text = lstImages.SelectedValue;
            imgPictures.ImageUrl = "~/Images/" + lstImages.SelectedValue;

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
                string sqlString = "SELECT * FROM Products WHERE Picture = @Picture";

                // create new SQL command object based on SQL string and connection
                scmd = new SqlCommand(sqlString, connectFill);

                // add the parameter to SQL string and validate
                scmd.Parameters.AddWithValue("@Picture", txtPict.Text);

                // create a new SQL data adapter to retrieve the data and
                // fill the data set
                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = scmd;
                sqlDataAdapter.Fill(ds, "Products");
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            if (ds.Tables["Products"].Rows.Count == 1)
            {
                txtProductNumber.Text = ds.Tables["Products"].Rows[0]["ProdID"].ToString();
                txtManufactCode.Text = ds.Tables["Products"].Rows[0]["ManCode"].ToString();
                txtDesc.Text = ds.Tables["Products"].Rows[0]["Description"].ToString();
                //txtPict.Text = ds.Tables["Products"].Rows[0]["Picture"].ToString();
                txtQuan.Text = ds.Tables["Products"].Rows[0]["QtyOnHand"].ToString();
                txtPrice.Text = ds.Tables["Products"].Rows[0]["Price"].ToString();
                btnDeleteProduct.Enabled = true;
                btnUpdateProduct.Enabled = true;
                lblMessages.Text = "";
            }
            else
                lblMessages.Text = "Product Number not found!";

            // release all database resources (memory)
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }
        //clear out entry fields
        private void flushData()
        {
            txtProductNumber.Text = "";
            txtManufactCode.Text = "";
            txtDesc.Text = "";
            txtPict.Text = "";
            txtQuan.Text = "";
            txtPrice.Text = "";
            //disable certain buttons
            btnDeleteProduct.Enabled = false;
            btnUpdateProduct.Enabled = false;
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