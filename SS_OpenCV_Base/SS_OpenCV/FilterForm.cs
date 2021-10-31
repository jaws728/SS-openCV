using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class FilterForm : Form
    {
        public FilterForm(string _title)
        {
            InitializeComponent();
            this.Text = _title;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SELECTION OF THE FILTER TO CHOOSE
            switch (comboBox1.SelectedIndex)
            {
                case 0: //realce de contornos
                    textBox1.Text = "-1";
                    textBox2.Text = "-1";
                    textBox3.Text = "-1";
                    textBox4.Text = "-1";
                    textBox5.Text = "9";
                    textBox6.Text = "-1";
                    textBox7.Text = "-1";
                    textBox8.Text = "-1";
                    textBox9.Text = "-1";
                    weight.Text = "1";
                    offset.Text = "0";
                    break;
                case 1: //gaussiano
                    textBox1.Text = "1";
                    textBox2.Text = "2";
                    textBox3.Text = "1";
                    textBox4.Text = "2";
                    textBox5.Text = "4";
                    textBox6.Text = "2";
                    textBox7.Text = "1";
                    textBox8.Text = "2";
                    textBox9.Text = "1";
                    weight.Text = "16";
                    offset.Text = "0";
                    break;
                case 2: //laplacian hard
                    textBox1.Text = "1";
                    textBox2.Text = "-2";
                    textBox3.Text = "1";
                    textBox4.Text = "-2";
                    textBox5.Text = "4";
                    textBox6.Text = "-2";
                    textBox7.Text = "1";
                    textBox8.Text = "-2";
                    textBox9.Text = "1";
                    weight.Text = "1";
                    offset.Text = "0";
                    break;
                case 3: //linhas verticais
                    textBox1.Text = "0";
                    textBox2.Text = "0";
                    textBox3.Text = "0";
                    textBox4.Text = "-1";
                    textBox5.Text = "2";
                    textBox6.Text = "-1";
                    textBox7.Text = "0";
                    textBox8.Text = "0";
                    textBox9.Text = "0";
                    weight.Text = "1";
                    offset.Text = "128";
                    break;
            }
            
        }

        private void ok_Click(object sender, EventArgs e)
        {

        }
    }
}
