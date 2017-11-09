using System;
using System.Collections.Concurrent;
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
  using System.Runtime.CompilerServices;

  public partial class ShoppingCart : Form
    {
        private Form1 mainForm;
        private ShopCartData ShopCartData;
        public IProgress<int> progress;
        public Label label= new Label();

        private Thread demoThread = null;

        public ShoppingCart()
        {
            InitializeComponent();
        }

        public ShoppingCart(Form1 form, ShopCartData scd)
        {
          this.SetupForm(form, scd);
          this.demoThread = new Thread(new ThreadStart(this.ThreadProcSafe));
        }

      private void ThreadProcSafe()
      {
        this.SetNumber("0");
      }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // mainForm.BackColor = Color.BlueViolet;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShoppingCart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

      public async Task<int> Increment()
      {
        var number = 0;
        return await Task.Run(
          () =>
            {
              Thread.Sleep(1000);
              number = int.Parse(this.label.Text);
              return ++number;
            });
      }
      public async Task<int> Decrement()
      {
        var number = 0;
        return await Task.Run(
                 () =>
                   {
                     Thread.Sleep(1000);
                     number = int.Parse(this.label.Text);
                     return --number;
                   });
      }

    delegate void StringArgReturningVoidDelegate(string text);
    public void SetNumber(string text)
    {
      if (this.label.InvokeRequired)
      {
          StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(this.SetNumber);
          //var task = Task.Run(() => { this.label.Text = number.ToString(); });
          this.Invoke(d, new object[] { text });
      }
      else
      {
        this.label.Text = text.ToString();
      }

      }

      private void SetupForm(Form1 form, ShopCartData scd)
      {
        InitializeComponent();
        mainForm = form;
        ShopCartData = scd;
        flowLayoutPanel1.AutoScroll = true;
        flowLayoutPanel1.VerticalScroll.Enabled = true;
        this.label.Text = "0";
        flowLayoutPanel1.Controls.Add(this.label);
    }
  }
}
