using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aaron_eCommerce2017
{
    public partial class Cart : System.Web.UI.Page
    {
        //on page load call functions to generate chart
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateCartGrid();
            CalculateTotal();
        }
        //Remove item from Cart function
        protected void RemoveFromCart(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int row = int.Parse(b.ID);

            Default.qtySold[Default.cartInfo[row]] = "1";
            // delete the cart item from the Main.cartInfo array
            // ... then rebuild the cart grid
            for (int i = row; i < Default.numItems; i++)
                Default.cartInfo[i] = Default.cartInfo[i + 1];

            Default.numItems--;
            CreateCartGrid();
            CalculateTotal();
        }
        //Create Grid function
        private void CreateCartGrid()
        {
            //Clear out old data
            Table1.Rows.Clear();
            //Loop for every item
            for (int i = 0; i < Default.numItems; i++)
            {
                // add new empty row object
                TableRow row = new TableRow();
                for (int j = 0; j < 7; j++)
                {
                    // add new empty cell object
                    TableCell cell = new TableCell();
                    //add info to cell based on what info should be there
                    if (j == 0)
                    {
                        Image pic = new Image();
                        pic.ImageUrl = "Images/" + Default.pics[Default.cartInfo[i]];
                        pic.Height = 165;
                        pic.Width = 120;
                        cell.Controls.Add(pic);
                    }
                    else if (j == 1)
                    {
                        cell.Text = Default.modelNum[Default.cartInfo[i]];
                    }
                    else if (j == 2)
                    {
                        cell.Text = Default.descrip[Default.cartInfo[i]];
                    }
                    else if (j == 3)
                    {
                        cell.Text = Default.qty[Default.cartInfo[i]];
                    }
                    else if (j == 4)
                    {
                        Label price = new Label();
                        price.Text = Default.price[Default.cartInfo[i]];
                        cell.Controls.Add(price);
                    }
                    else if (j == 5)
                    {
                        Button btnAddToCart = new Button();
                        btnAddToCart.ID = i.ToString();

                        btnAddToCart.Click += new EventHandler(RemoveFromCart);
                        btnAddToCart.Text = "Remove from Cart";
                        cell.Controls.Add(btnAddToCart);
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
                        cell.Controls.Add(text);
                    }
                    // now add all the cells for the current row
                    row.Cells.Add(cell);
                }
                // finally, add the current row to the table
                Table1.Rows.Add(row);
            }
        }
        //Recalculate Total if needed
        protected void RecalculateTotal(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        //Caluculate Total price function
        private void CalculateTotal()
        {
            decimal total = 0;
            //Loop for each item
            for (int i = 0; i < Default.numItems; i++)
            {
                TableRow row = Table1.Rows[i];
                decimal rowPrice = 0;
                //Loop for each cell on the row
                for (int j = 0; j < 7; j++)
                {
                    TableCell cell = row.Cells[j];
                    //if looking at price cell, parse price from data
                    if (j == 4)
                    {
                        Control ctrl = cell.Controls[0];
                        Label lbl = (Label)ctrl;
                        string price = lbl.Text;
                        rowPrice = decimal.Parse(price);
                    }
                    //if looking at quantity cell, parse qty from data
                    else if (j == 6)
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
            //format and output total cost
            LblTotal.Text = total.ToString("$##,##0.#0");
        }

        //Function to load checkout page
        protected void LoadCheckoutPage(object sender, EventArgs e)
        {
            Server.Transfer("Checkout.aspx");
        }
    }

}