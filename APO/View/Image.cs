using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using APO.Utility;


namespace APO
{
   
    public partial class Image : Form
    {
        public Bitmap image { get; set; }
        public Chart graph { get; set; }
        public Bitmap defaulImage { get; set; }
        public bool rgb { get; set; }
        public bool gray { get; set; }

        public Image(Bitmap image)
        {
            InitializeComponent();
            imageBox.Image = image;
            imageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            imageBox.SizeMode = PictureBoxSizeMode.StretchImage;

            this.image = image;
            defaulImage = image;
            rgb = false;
            gray = false;


        }
        public Image(Bitmap image, Bitmap defaultImage)
        {
            InitializeComponent();
            imageBox.Image = image;
            imageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            imageBox.SizeMode = PictureBoxSizeMode.StretchImage;

            this.image = image;
            defaulImage = image;
            rgb = false;
            gray = false;


        }
        
        private void obrazZapisMenuItem_Click(object sender, EventArgs e)
        {
            {
                var ima = new SaveFileDialog();
                ima.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg";
                ima.FileName = "Image";
                ima.ShowDialog();

                try { this.imageBox.Image.Save(ima.FileName); }
                catch { MessageBox.Show("Coś poszło nie tak :(."); }
            }

        }

        private void histogramZapisMenuItem_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            save.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg";
            save.FileName = "Histogram";
            save.ShowDialog();

            try { this.graphBox.SaveImage(save.FileName, ChartImageFormat.Jpeg); }
            catch { MessageBox.Show("Coś poszło nie tak :(."); }
        }

        private void klonowanieImageMenuItem_Click(object sender, EventArgs e)
        {
            var Image = new Image((Bitmap)imageBox.Image);
            Image.Size = this.Size;
            Image.Show();
        }
        private void greyHistogramItem_Click(object sender, EventArgs e)
        {
            graphBox.Series.Clear();
            graphBox.Visible = true;
            Dictionary<Color, int> map = Histogram.HistogramMap((Bitmap)imageBox.Image);
            int[] GrayLut = Histogram.HistogramLUT(map);
            int sum = 0;
            graphBox.Series.Add("Gray");
            graphBox.Series["Gray"].Color = Color.Gray;
            for (int i = 0; i < GrayLut.Length; i++)
            {
                this.graphBox.Series["Gray"].Points.AddXY(i, GrayLut[i]);
                sum += GrayLut[i];
            }

            //graphBox.Text = sum.ToString();
            //histogramToolStripMenuItem1.Enabled = true;
            //greyHistogramItem.Enabled = false;
            //RGBHistogramMenuStrip.Enabled = false;
            //StretchButton.Enabled = true;
            this.graph = graphBox;
            gray = true;
        }




        private void rgbHistogramItem_Click(object sender, EventArgs e)
        {
            graphBox.Series.Clear();
            graphBox.Visible = true;
            Dictionary<Color, int> map = Histogram.HistogramMap((Bitmap)imageBox.Image);
            int[] RedLut = Histogram.HistogramLUT(map, "red");
            int[] GreenLut = Histogram.HistogramLUT(map, "green");
            int[] BlueLut = Histogram.HistogramLUT(map, "blue");
            int sum = 0;

            graphBox.Series.Add("Red");
            graphBox.Series.Add("Blue");
            graphBox.Series.Add("Green");
            graphBox.Series["Red"].Color = Color.Red;
            graphBox.Series["Blue"].Color = Color.Blue;
            graphBox.Series["Green"].Color = Color.Green;

            for (int i = 0; i < RedLut.Length; i++)
            {
                this.graphBox.Series["Red"].Points.AddXY(i, RedLut[i]);
                this.graphBox.Series["Green"].Points.AddXY(i, GreenLut[i]);
                this.graphBox.Series["Blue"].Points.AddXY(i, BlueLut[i]);
                sum += RedLut[i];
            }

            //PixelsTextBox.Text = sum.ToString();
            //histogramToolStripMenuItem1.Enabled = true;
            //RGBHistogramMenuStrip.Enabled = false;
            //greyHistogramItem.Enabled = false;
            //StretchButton.Enabled = true;
            this.graph = graphBox;
            rgb = true;
        }

        
    }

}
