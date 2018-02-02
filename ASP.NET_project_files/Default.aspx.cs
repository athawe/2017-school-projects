using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Aaron_eCommerce2017
{
    public partial class Default : System.Web.UI.Page
    {
        //Create variables for storing cart data,etc.
        const int MAXPRODUCTS = 15;
        public static string[] modelNum;
        public static string[] pics;
        public static string[] descrip;
        public static string[] qty;
        public static string[] price;
        public static string[] qtySold = new string[MAXPRODUCTS];
        public static int[] cartInfo = new int[MAXPRODUCTS];
        public static int numItems = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                for (int i = 0; i < MAXPRODUCTS; i++)
                    qtySold[i] = "1";
            }
        }

        //Functions to change iFrame to different pages.
        protected void btnPromo_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "PromoPage.aspx");
            
        }

        protected void btnWeather_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "https://www.theweathernetwork.com/ca/weather/ontario/london");
        }

        protected void btnCustomers_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Customers.aspx");
        }

        protected void btnProducts_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Products.aspx");
        }

        protected void btnCatalog_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Catalog.aspx");
        }

        protected void btnCart_Click(object sender, EventArgs e)
        {
            MyFrame.Attributes.Add("src", "Cart.aspx");
        }
    }
}