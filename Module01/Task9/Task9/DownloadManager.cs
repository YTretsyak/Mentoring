using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task9
{
    public partial class DownloadManager : Form
    {
        private IProgress<int> _progress;// use for progress bar
        private CancellationTokenSource cancelSource;

        public DownloadManager()
        {
            InitializeComponent();
            var progressHandler = new Progress<int>(value =>
            {
                progressBar1.Value = value;
            });
            _progress = (IProgress<int>)progressHandler;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScroll = true;
            AddUrl();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _progress.Report(0);
            await DoAction(_progress);
        }

        private void AddUrl()
        {
            TextBox text1 = new TextBox();
            text1.Size = new Size { Width = 350, Height = 15 };

            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Controls.Add(text1);
        }

        private async Task DoAction(IProgress<int> progress)
        {
            var urlCollection = new List<string>();
            var content = new List<string>();
            cancelSource = new CancellationTokenSource();
            foreach(TextBox textbox in flowLayoutPanel1.Controls)
            {
                if (String.IsNullOrEmpty(textbox.Text) == false && textbox.Text.StartsWith("http"))
                {
                    urlCollection.Add(textbox.Text);
                }
            }

            var tempCount = 1;
            var client = new HttpClient();
            foreach (string t in urlCollection)
            {
                try
                {
                    var response = await client.GetAsync(new Uri(t), cancelSource.Token);
                    response.EnsureSuccessStatusCode();
                    string html = await response.Content.ReadAsStringAsync();
                    if (progress != null)
                    {
                        progress.Report(tempCount * 100 / urlCollection.Count);
                        Thread.Sleep(10);
                    }
                    tempCount++;
                    content.Add(html);
                }
                catch (OperationCanceledException ex)
                {
                    //MessageBox.Show("Canceled!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetType().FullName + @" " + ex.Message);
                }

                foreach (var item in content)
                {
                    //Test(item);
                    // TODO:here we can save our data as text file or add somethig else...
                }
            }
        }
        //Add Url button
        private void button3_Click_1(object sender, EventArgs e)
        {
            AddUrl();
        }
        //Remove Url button
        private void button4_Click_1(object sender, EventArgs e)
        {
            var lastElementIndex = flowLayoutPanel1.Controls.Count;
            if (lastElementIndex > 1)
            {
                flowLayoutPanel1.Controls.RemoveAt(lastElementIndex - 1);
            }
        }
        //Cansel Button
        private void button2_Click(object sender, EventArgs e)
        {
            cancelSource.Cancel();
            progressBar1.Value = 0;
        }

        private async void Test(string text)
        {
           await Task.Run(()=> MessageBox.Show(text));
        }
    }
}
