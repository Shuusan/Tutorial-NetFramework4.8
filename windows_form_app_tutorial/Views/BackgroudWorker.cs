using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_app_tutorial.Class;

namespace windows_form_app_tutorial.Views
{
    public partial class frmBackgrundWorker : Form
    {
        public frmBackgrundWorker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                textBox1.Text = "";
                textBox1.Focus();

                progressBar1.Visible = true;
                progressBar1.Value = 0;
                pictureBox1.Image = null;
                
                string imageUrl = "https://drive.google.com/u/1/uc?id=1ihdEkQGc-gQaBQAiYz4JoLENZZIshXh9&export=download";
                backgroundWorker1.RunWorkerAsync(imageUrl);

                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();

            progressBar1.Visible = true;
            progressBar1.Value = 0;
            pictureBox1.Image = null;

            string imageUrl = "https://images.pexels.com/photos/842711/pexels-photo-842711.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";
            backgroundWorker1.RunWorkerAsync(imageUrl);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string imageUrl = e.Argument as string;
            try
            {


                //using (WebClient webClient = new WebClient())
                //{
                //    webClient.DownloadProgressChanged += (s, progressArgs) =>
                //    {
                //        backgroundWorker1.ReportProgress(progressArgs.ProgressPercentage);
                //    };

                //    byte[] imageData = webClient.DownloadData(imageUrl);
                //    MemoryStream stream = new MemoryStream(imageData);
                //    e.Result = Image.FromStream(stream);
                //}

                var tcs = new TaskCompletionSource<Image>();

                Task.Run(async () =>
                {
                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.GetAsync(imageUrl, HttpCompletionOption.ResponseHeadersRead);
                            response.EnsureSuccessStatusCode();

                            long contentLength = response.Content.Headers.ContentLength.GetValueOrDefault(0);

                            using (var contentStream = await response.Content.ReadAsStreamAsync())
                            {
                                var progressHandler = new HttpProgress(backgroundWorker1, contentLength);

                                using (var memoryStream = new MemoryStream())
                                {
                                    var buffer = new byte[8192];
                                    long bytesRead = 0;
                                    int read;

                                    while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await memoryStream.WriteAsync(buffer, 0, read);
                                        bytesRead += read;
                                        progressHandler.Report(bytesRead);
                                    }

                                    memoryStream.Position = 0;
                                    tcs.SetResult(Image.FromStream(memoryStream));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                });

                e.Result = tcs.Task.Result;
            }
            catch (WebException ex)
            {
                e.Result = ex;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                if (e.Result is Image)
                {
                    pictureBox1.Image = e.Result as Image;
                    
                }
                else if (e.Result is WebException)
                {
                    MessageBox.Show($"Error downloading image: {((WebException)e.Result).Message}");
                }
            }
            else
            {
                MessageBox.Show("Error downloading image.");
            }
        }

        private async void btnRandomImage_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();

            progressBar1.Visible = true;
            progressBar1.Value = 0;
            pictureBox1.Image = null;

            string randomImageUrl = await DownloadImage();

            if (!string.IsNullOrEmpty(randomImageUrl))
            {
                backgroundWorker1.RunWorkerAsync(randomImageUrl);
            }
        }

        private async Task<string> DownloadImage()
        {
            // Generate a random URL for the image
            var imageUrl = "https://picsum.photos/1024";

            // Create the folder on the Desktop if it doesn't exist
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "randomImage");

            Directory.CreateDirectory(folderPath);

            // Generate a unique filename for the downloaded image
            string filename = $"randomImage_{Guid.NewGuid()}.jpg";
            string imagePath = Path.Combine(folderPath, filename);
            string finalImageUrl = "";


            // Download and save the image
            using (HttpClient httpClient = new HttpClient())
            {
                // Follow the redirect and get the actual URL
                // Set a user agent
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
                HttpResponseMessage response = await httpClient.GetAsync(imageUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                finalImageUrl = response.RequestMessage.RequestUri.ToString();

                // Download the image
                // byte[] imageBytes = await httpClient.GetByteArrayAsync(finalImageUrl);

                // Save the image to the local file
                // File.WriteAllBytes(imagePath, imageBytes);
            }

            // Console.WriteLine($"Image saved to: {imagePath}");

            return finalImageUrl;


        }
    }
}
