using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task10.Shop.Models;

namespace Task10.Shop
{
    public partial class Form1 : Form
    {
        private IProgress<int> progress;
        private ShopCartData cart;
        private ShoppingCart shoppingCart;
        static Mutex mutexObj = new Mutex();

        public Form1()
        {
            cart = new ShopCartData();
            InitializeComponent();
            progress = new Progress<int>(delegate(int obj) { label1.Text = cart.GetResultAsync().Result.ToString(); });
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Enabled = true;
            CreateItems(50);
            label2.TextAlign = ContentAlignment.MiddleCenter;
            var initialPriceText = "0";
            label1.Text = initialPriceText;
            // init second form
            shoppingCart = new ShoppingCart(this, cart);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shoppingCart.Show();
        }

        private void CreateItems(int size)
        {
            var rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                var price = rnd.Next(1,100);
                var item = new Item {Text = "Item: " + i + " Price: " + price + "$", ItemPrice = price, Width = 150};
                item.CheckedChanged += this.item_CheckedChanged;
              this.flowLayoutPanel1.Controls.Add(item);
            }
        }

        private async void item_CheckedChanged(object sender, EventArgs e)
        {
            Item checkBox = (Item)sender;
            bool added = checkBox.Checked;
          await Task.Run(
            () =>
              {
                var cartPanel = this.shoppingCart.Controls.Find("flowLayoutPanel1", true);
                var el = cartPanel[0] as FlowLayoutPanel;
                mutexObj.WaitOne();
                var t1 = 0;
                if (checkBox.Checked)
                {
                  cart.Items.Add(checkBox); // add to cart model
                                            //this.shoppingCart.label.Text = t1.Result.ToString();
                                            //this.shoppingCart.SetNumber(t1.Result.ToString());
                  t1 = this.shoppingCart.Increment().Result;
                }
                else
                {
                  var removedItem = this.cart.Items.TryTake(out checkBox); // remove from cart 
                  t1 = this.shoppingCart.Decrement().Result;
                }
                this.shoppingCart.SetNumber(t1.ToString());
                mutexObj.ReleaseMutex();
              }).ContinueWith(
            (result) =>
              {
                var price = 0;
                if (added)
                {
                  price = checkBox.ItemPrice;
                }
                else
                {
                  price = -checkBox.ItemPrice;
                }
                progress.Report(price);
                Thread.Sleep(1000);
                MessageBox.Show("Test");
              },
            TaskContinuationOptions.AttachedToParent);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
