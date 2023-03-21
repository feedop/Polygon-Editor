using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_CAD
{
    public partial class Form2 : Form
    {
        public float Result { get; private set; }
        public Form2(float length)
        {
            InitializeComponent();
            Result = length;
            textBox1.Text = Result.ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                Result = float.Parse(textBox1.Text);
                if (Result <= 0) DisplayError();
                else
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                DisplayError();
            }
        }

        private void DisplayError()
        {
            string message = "Incorrect value";
            string caption = "Error";

            // Display error
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
