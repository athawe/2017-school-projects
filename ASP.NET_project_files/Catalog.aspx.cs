using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace Aaron_eCommerce2017
{
    public partial class Catalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Database connection string
            string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";

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
                string sqlString = "SELECT * FROM Products";

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
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            int numProducts = 0;
            if (ds.Tables["Products"].Rows.Count > 0)
            {
                //numProducts = how many Products are in Products Database
                numProducts = ds.Tables["Products"].Rows.Count;
                //set up arrays with correct size based on numProducts
                Default.modelNum = new string[numProducts];
                Default.pics = new string[numProducts];
                Default.descrip = new string[numProducts];
                Default.qty = new string[numProducts];
                Default.price = new string[numProducts];

                //Fill arrays with data
                for (int i = 0; i < numProducts; i++)
                {
                    Default.modelNum[i] = ((int)ds.Tables["Products"].Rows[i]["ProdID"]).ToString();
                    Default.descrip[i] = ds.Tables["Products"].Rows[i]["Description"].ToString();
                    Default.pics[i] = ds.Tables["Products"].Rows[i]["Picture"].ToString().Replace(" ", "");
                    Default.qty[i] = ((int)ds.Tables["Products"].Rows[i]["QtyOnHand"]).ToString();
                    Default.price[i] = ((decimal)ds.Tables["Products"].Rows[i]["Price"]).ToString();
                }
            }

            // release all database resources (memory)
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            //After reading data from database, output to table on page
            for (int i = 0; i < numProducts; i++)
            {
                // add new empty row object
                TableRow row = new TableRow();
                for (int j = 0; j < 6; j++)
                {
                    // add new empty cell object
                    TableCell cell = new TableCell();

                    if (j == 0)
                    {
                        Image pic = new Image();
                        pic.ImageUrl = "Images/" + Default.pics[i];
                        pic.Height = 165;
                        pic.Width = 120;
                        cell.Controls.Add(pic);
                    }
                    else if (j == 1)
                    {
                        cell.Text = Default.modelNum[i];
                    }
                    else if (j == 2)
                    {
                        cell.Text = Default.descrip[i];
                    }
                    else if (j == 3)
                    {
                        cell.Text = Default.qty[i];
                    }
                    else if (j == 4)
                    {
                        cell.Text = Default.price[i];
                    }
                    else
                    {
                        //Create buttons
                        Button btnAddToCart = new Button();
                        btnAddToCart.ID = i.ToString();
                        btnAddToCart.CssClass = "Buttons";
                        btnAddToCart.Click += new EventHandler(btnCatalog_Click);
                        btnAddToCart.Text = "Add To Cart";
                        cell.Controls.Add(btnAddToCart);
                    }
                    // now add all the cells for the current row
                    row.Cells.Add(cell);
                }
                // finally, add the current row to the table
                tblCatalog.Rows.Add(row);
            }
        }

        protected void btnCatalog_Click(object sender, EventArgs e)
        {
            // create a button object from the sender
            // and cast the sender object to a button
            Button b = (Button)sender;
            int row = int.Parse(b.ID);

            if (Default.numItems > 0)
            {
                bool matchRow = false;
                for (int i = 0; i < Default.numItems; i++)
                {
                    if (row == Default.cartInfo[i])
                    {
                        matchRow = true;
                        break;
                    }
                }
                if (!matchRow)
                {
                    Default.cartInfo[Default.numItems] = row;
                    Default.numItems++;
                }
            }
            else
            {
                Default.cartInfo[Default.numItems] = row;
                Default.numItems++;
            }
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