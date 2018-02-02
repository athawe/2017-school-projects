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
    public partial class Details : System.Web.UI.Page
    {
        public static Logger Log = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateDetailGrid();
                CalculateTotal();
            }
        }

        //CreateDetailGrid function
        private void CreateDetailGrid()
        {
            Table1.Rows.Clear();
            for (int i = 0; i < Default.numItems; i++)
            {
                // add new empty row object
                TableRow row = new TableRow();
                for (int j = 0; j < 4; j++)
                {
                    // add new empty cell object
                    TableCell cell = new TableCell();

                    if (j == 0)
                    {
                        cell.Text = Default.modelNum[Default.cartInfo[i]];
                    }
                    else if (j == 1)
                    {
                        cell.Text = Default.descrip[Default.cartInfo[i]];
                    }
                    else if (j == 2)
                    {
                        Label price = new Label();
                        price.Text = Default.price[Default.cartInfo[i]];
                        cell.Controls.Add(price);
                    }
                    else
                    {
                        TextBox text = new TextBox();
                        text.Text = Default.qtySold[Default.cartInfo[i]];
                        text.Style["font-family"] = "helvetica";
                        text.Style["color"] = "blue";
                        text.Style["background-color"] = "white";
                        text.Style["width"] = "20px";
                        text.Style["border"] = "solid 1px #002594";
                        text.Enabled = false;

                        cell.Controls.Add(text);
                    }
                    // now add all the cells for the current row
                    row.Cells.Add(cell);
                }
                // finally, add the current row to the table
                Table1.Rows.Add(row);
            }
        }

        //Calculate Total function
        private void CalculateTotal()
        {
            decimal total = 0;
            for (int i = 0; i < Default.numItems; i++)
            {
                TableRow row = Table1.Rows[i];
                decimal rowPrice = 0;

                for (int j = 0; j < 4; j++)
                {
                    TableCell cell = row.Cells[j];
                    //get price
                    if (j == 2)
                    {
                        Control ctrl = cell.Controls[0];
                        Label lbl = (Label)ctrl;
                        string price = lbl.Text;
                        rowPrice = decimal.Parse(price);
                    }
                    //get qty value
                    else if (j == 3)
                    {
                        Control ctrl = cell.Controls[0];
                        TextBox txt = (TextBox)ctrl;
                        string qty = txt.Text;
                        Default.qtySold[Default.cartInfo[i]] = qty;

                        decimal rowTotal = rowPrice * int.Parse(qty);
                        total += rowTotal;
                    }
                }
            }

            LblTotal.Text = total.ToString("$##,##0.#0");
        }
        //PayForOrder function
        protected void PayForOrder(object sender, EventArgs e)
        {
            if (Default.numItems == 0)
            {
                MessageBox.Show(this, "No items in the cart");
                return;
            }
            //connection string
            string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";

            // test data used to process a sale
            int customerID = Customers.cusID;
            string[] productArray = new string[Default.numItems];
            int[] qtyArray = new int[Default.numItems];
            decimal[] priceArray = new decimal[Default.numItems];

            for (int i = 0; i < Default.numItems; i++)
            {
                productArray[i] = Default.modelNum[Default.cartInfo[i]];
                qtyArray[i] = int.Parse(Default.qtySold[Default.cartInfo[i]]);
                priceArray[i] = decimal.Parse(Default.price[Default.cartInfo[i]]);
            }

            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            // begin the transaction here
            SqlTransaction dbTrans = connectCmd.BeginTransaction();
            Log.LogMessage("MessageStream", "PayForOrder Starting Sales Transaction");
            // create a date/time stamp
            DateTime dtStamp = DateTime.Now;

            // 1. Create the SalesMain record
            string sqlString = "INSERT INTO SalesMain (CusID, DateTimeStamp) VALUES (@CusID, @DateTimeStamp)";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);

                // start building the SQL transaction package
                cmd.Transaction = dbTrans;
                cmd.Parameters.AddWithValue("@CusID", customerID);
                cmd.Parameters.AddWithValue("@DateTimeStamp", dtStamp);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                Log.LogMessage("MessageStream", "PayForOrder 1.0 " + ex.Message);
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }

            // get the primary key identity just inserted
            // this is the new invoice number which we need below
            cmd = new SqlCommand("SELECT IDENT_CURRENT('SalesMain') FROM SalesMain", connectCmd);
            cmd.Transaction = dbTrans;
            int invoiceNum = Convert.ToInt32(cmd.ExecuteScalar());

            // 2. Add all the SalesDetail records (1 for each product)

            for (int i = 0; i < productArray.Length; i++)
            {
                sqlString = "INSERT INTO SalesDetail (InvID, ProdId, Qty, Price) VALUES (@InvID, @ProdId, @Qty, @Price)";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);

                    // continue building the SQL transaction package
                    cmd.Transaction = dbTrans;
                    cmd.Parameters.AddWithValue("@InvID", invoiceNum);
                    cmd.Parameters.AddWithValue("@ProdId", productArray[i]);
                    cmd.Parameters.AddWithValue("@Qty", qtyArray[i]);       
                    cmd.Parameters.AddWithValue("@Price", priceArray[i]);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 2.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }
            }

            // 3. Update the QtyOnHand for each product record

            int qtyOnHand = 0;
            for (int i = 0; i < productArray.Length; i++)
            {
                // get the current quantity on hand
                try
                {
                    ds = new DataSet();
                    connectFill = new SqlConnection(dbConnect);

                    sqlString = "SELECT QtyOnHand FROM Products WHERE ProdID = @ProdID";
                    scmd = new SqlCommand(sqlString, connectFill);
                    scmd.Parameters.AddWithValue("@ProdID", productArray[i]);

                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = scmd;
                    sqlDataAdapter.Fill(ds, "Products");
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 3.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                if (ds.Tables["Products"].Rows.Count == 1)
                {
                    qtyOnHand = (int)ds.Tables["Products"].Rows[0]["QtyOnHand"];
                }
                else
                {
                    dbTrans.Rollback();
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                sqlString = "UPDATE Products SET QtyOnHand = @QtyOnHand WHERE ProdID = @ProdID";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);

                    // finish building the SQL transaction package
                    cmd.Transaction = dbTrans;
                    cmd.Parameters.AddWithValue("@ProdID", productArray[i]);
                    cmd.Parameters.AddWithValue("@QtyOnHand", qtyOnHand - qtyArray[i]);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 4.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }
            }
            Console.WriteLine("All associated Products records updated");

            //commit the transaction and complete all table changes
            dbTrans.Commit();
            //Upate log
            Log.LogMessage("MessageStream", "PayForOrder Sales Transaction Completed for Customer " + customerID);
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }
        //Unused function, possible Mailing List capability if needed.
        protected void AddMe(object sender, EventArgs e)
        {
            // code that adds or removes
            if (ChkMailingList.Checked)
            {
                // add
                bool add = true;
            }
            else
            {
                // remove
                bool add = false;
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

        protected void ConfirmAction_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You clicked OK");
        }
    }
}