using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace APO
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var o = new OpenFileDialog();
            o.ShowDialog();

            try
            {
                var imageWindow = new Image(new Bitmap(o.FileName));
                imageWindow.Show();
            }
            catch
            {
                MessageBox.Show("Nie wybrałeś obrazka.");
            }
        }

        
    }
}
