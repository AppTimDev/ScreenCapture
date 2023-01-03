using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class MainForm : Form
    {
        private bool bMinimizeToIcon = true;
        public MainForm()
        {
            InitializeComponent();

            //not allow resize
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //remove the minimize and maximize buttons
            this.MaximizeBox = false;
            //this.MinimizeBox = false;
        }

        private void ScreenCapture()
        {
            Screen curScreen = Screen.AllScreens[0];
            //Screen curScreen = Screen.PrimaryScreen;
            //Create a Rectangle object, capture current screen
            Rectangle captureRect = curScreen.Bounds;

            //Create a new Bitmap object
            //Bitmap bmp = new Bitmap(captureRect.Width, captureRect.Height, PixelFormat.Format32bppArgb);
            Bitmap bmp = new Bitmap(captureRect.Width, captureRect.Height);
            //Create a new Graphics object from bmp
            Graphics g = Graphics.FromImage(bmp);

            //copy image from screen
            g.CopyFromScreen(captureRect.Left, captureRect.Top, 0, 0, captureRect.Size);

            //save the image in jpg to the desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            string filepath = Path.Combine(path, filename);
            bmp.Save(filepath, ImageFormat.Jpeg);
        }
        private void ScreenCapBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.bMinimizeToIcon = false;
                ShowInTaskbar = true;
                WindowState = FormWindowState.Minimized;
                Thread.Sleep(1000);

                ScreenCapture();

                Thread.Sleep(1000);
                this.bMinimizeToIcon = true;
                WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void capScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                ScreenCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && this.bMinimizeToIcon)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.PrintScreen)
                {
                    this.bMinimizeToIcon = false;
                    ShowInTaskbar = true;
                    WindowState = FormWindowState.Minimized;
                    Thread.Sleep(2000);

                    ScreenCapture();
                    this.bMinimizeToIcon = true;
                    WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
