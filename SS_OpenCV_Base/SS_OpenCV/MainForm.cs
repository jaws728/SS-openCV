using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace SS_OpenCV
{ 
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Change visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Call automated image processing check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        /// <summary>
        /// Call image convertion to gray scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.GreenCnel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BlueChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void brightContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            //get values from user
            InputBox form = new InputBox("Brightness: (0 to 255)");
            form.ShowDialog();
            int bright = Convert.ToInt32(form.ValueTextBox.Text);
            if (bright < 0)
                bright = 0;
            if (bright > 255)
                bright = 255;

            form = new InputBox("Contrast: (0 to 3)");
            form.ShowDialog();
            double contrast = Convert.ToDouble(form.ValueTextBox.Text.ToString());
            if (contrast < 0)
                contrast = 0;
            if (contrast > 3)
                contrast = 3;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BrightContrast(img, bright, contrast);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Translation at x: (Dx)");
            form.ShowDialog();
            int dx = Convert.ToInt32(form.ValueTextBox.Text);

            form = new InputBox("Translation at y: (Dy)");
            form.ShowDialog();
            int dy = Convert.ToInt32(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Rotation angle:");
            form.ShowDialog();
            float angle = Convert.ToSingle(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Rotation(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Scale:");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }


        //create mouse variables
        int mouseX, mouseY;
        bool mouseFlag = false;

        private void scaleXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Scale:");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            //get mouse coordinates using mouseclick event
            mouseFlag = true;
            while (mouseFlag) //wait for mouseclick event
                Application.DoEvents();

            //form = new InputBox("Center X: ");
            //form.ShowDialog();
            //int centerX = Convert.ToInt32(form.ValueTextBox.Text);
            //
            //form = new InputBox("Center Y: ");
            //form.ShowDialog();
            //int centerY = Convert.ToInt32(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Scale_point_xy(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseFlag)
            {
                mouseX = e.X; //get mouse coordinates
                mouseY = e.Y;

                mouseFlag = false; //unlock while cycle
            }
        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            float[,] matrix = new float[,] { { 0.0f, 0.0f, 0.0f }, { 0.0f, 0.0f, 0.0f }, { 0.0f, 0.0f, 0.0f } };
            float weight = 0;
            float offset = 0;

            FilterForm form = new FilterForm("Filter:");
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                switch (form.comboBox1.SelectedIndex)
                {
                    case 0:
                        matrix = new float[,] { {-1.0f, -1.0f, -1.0f}, {-1.0f, 9.0f, -1.0f}, {-1.0f, -1.0f, -1.0f} };
                        weight = 1;
                        offset = 0;
                        break;
                    case 1:
                        matrix = new float[,] { { 1.0f, 2.0f, 1.0f }, { 2.0f, 4.0f, 2.0f }, { 1.0f, 2.0f, 1.0f } };
                        weight = 16;
                        offset = 0;
                        break;
                    case 2:
                        matrix = new float[,] { { 1.0f, -2.0f, 1.0f }, { -2.0f, 4.0f, -2.0f }, { 1.0f, -2.0f, 1.0f } };
                        weight = 1;
                        offset = 0;
                        break;
                    case 3:
                        matrix = new float[,] { { 0.0f, 0.0f, 0.0f }, { -1.0f, 2.0f, -1.0f }, { 0.0f, 0.0f, 0.0f } };
                        weight = 16;
                        offset = 0;
                        break;
                }
            } else
            {
                return;
            }

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.NonUniform(img, imgUndo, matrix, weight, offset);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void diferentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Diferentiation(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Median(img, imgUndo);

            ImageViewer.Image = img.Bitmap;

            ImageViewer.Refresh(); // refresh image on the screen
            Cursor = Cursors.Default; // normal cursor 
        }

        private void histogramGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            int[] array = ImageClass.Histogram_Gray(img);
            //copy 1 dimension array to 2 dimension matrix
            int[,] matrix = new int[1, 256];
            for (int i = 0; i < array.Length; i++)
            {
                matrix[0, i] = array[i];
            }

            HistogramForm form = new HistogramForm(matrix);
            form.ShowDialog();

            Cursor = Cursors.Default; // normal cursor 
        }

        private void histogramRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            int[,] matrix = ImageClass.Histogram_RGB(img);

            HistogramForm form = new HistogramForm(matrix);
            form.ShowDialog();

            Cursor = Cursors.Default; // normal cursor 
        }

        private void histogramAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            int[,] matrix = ImageClass.Histogram_All(img);

            HistogramForm form = new HistogramForm(matrix);
            form.ShowDialog();

            Cursor = Cursors.Default; // normal cursor 
        }

        private void blackWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("THRESHOLD");
            form.ShowDialog();
            int threshold = Convert.ToInt32(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW(img, threshold);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void blackWhiteOTSUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }


        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Roberts(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void rotationBilinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Rotation angle:");
            form.ShowDialog();
            float angle = Convert.ToSingle(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Rotation_Bilinear(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void scaleBilinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Scale:");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void scaleXYBilinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            InputBox form = new InputBox("Scale:");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            //get mouse coordinates using mouseclick event
            mouseFlag = true;
            while (mouseFlag) //wait for mouseclick event
                Application.DoEvents();

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Scale_point_xy_Bilinear(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
        private void platesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            Rectangle[] r = new Rectangle[7];
            string[] str = new string[6];
            //int[] proj = new int[img.Width];
            //int[] proj = new int[img.Height];

            ImageClass.LP_Recognition(img, imgUndo, 2, "B", out r[0], out r[1], out r[2], out r[3], out r[4], out r[5], out r[6], out str[0], out str[1], out str[2], out str[3], out str[4], out str[5]);
            //ImageClass.LP_Recognition(img, imgUndo, 1, "B", out r[0], out r[1], out r[2], out r[3], out r[4], out r[5], out r[6], out str[0], out str[1], out str[2], out str[3], out str[4], out str[5], proj);

            string mess = str[0] + str[1] + str[2] + str[3] + str[4] + str[5];
            MessageBox.Show(mess);

            /*
            int[,] matrix = new int[1, proj.Length];
            for (int i = 0; i < proj.Length; i++)
            {
                matrix[0, i] = proj[i];
            }

            HistogramForm form = new HistogramForm(matrix);
            form.ShowDialog();
            */

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void mediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
    }




}