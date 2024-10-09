using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Elemendid_vorm_TARpv23
{
    public partial class EsimeneVorm : Form
    {

        Button btn;
        Button btn2;
        Button btn3;
        Button btn4;
        PictureBox pb1 = new PictureBox();
        ColorDialog cd1 = new ColorDialog();
        System.Windows.Forms.CheckBox chk1, chk2;

        public EsimeneVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Esimene vorm";

            btn = new Button();
            btn.Text = "Close";
            btn.Location = new Point(300,440);
            btn.Click += closeButton_Click;
            Controls.Add(btn);

            btn2 = new Button();
            btn2.Text = "Show picture";
            btn2.Location = new Point(375,440);
            btn2.Click += Click_ShowPictureButton;
            Controls.Add(btn2);

            btn3 = new Button();
            btn3.Text = "Clear Picture";
            btn3.Location = new Point(450,440);
            btn3.Click += clearButton_Click;

            Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Set the background color";
            btn4.Location = new Point(525,440);
            btn4.Click += backgroundButton_Click;

            Controls.Add(btn4);

            chk1 = new System.Windows.Forms.CheckBox();
            chk1.Checked = false;
            chk1.Text = "Strech";
            chk1.Size = new Size(75, 20);
            chk1.Location = new Point(20, 440);
            chk1.Click += checkBox1_CheckedChanged;

            Controls.Add(chk1);
        }

       

        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear the picture.
            pb1.Image = null;
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            // Show the color dialog box. If the user clicks OK, change the
            // PictureBox control's background to the color the user chose.
            
            cd1.ShowDialog(); this.BackColor = cd1.Color;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }

        private void Click_ShowPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = Image.FromFile(@"..\..\..\picture.jpg"); pb1.Location = new Point(0, 0);
            pb1.Size = new Size(800, 900); this.Controls.Add(pb1);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Stretch check box, 
            // change the PictureBox's
            // SizeMode property to "Stretch". If the user clears 
            // the check box, change it to "Normal".
            if (chk1.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
    