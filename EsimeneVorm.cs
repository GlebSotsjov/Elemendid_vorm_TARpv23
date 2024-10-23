using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vorm_TARpv23
{
    public partial class EsimeneVorm : Form
    {
        Button btnClose;
        Button btnShowPicture;
        Button btnClearPicture;
        Button btnSetBackground;
        Button btnRotate;
        Button btnBlackAndWhite;
        PictureBox pb1 = new PictureBox();
        ColorDialog cd1 = new ColorDialog();
        System.Windows.Forms.CheckBox chk1;
        OpenFileDialog ofd = new OpenFileDialog();
        Button btnChangePicture;
        Label lblTime;
        System.Windows.Forms.Timer timeTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        List<string> imageFiles = new List<string> { @"..\..\..\patrick.png", @"..\..\..\squidward.png", @"..\..\..\spongebob.png" };
        int currentImageIndex = 0;

        public EsimeneVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Esimene vorm";

            // Создаем PictureBox
            pb1.SizeMode = PictureBoxSizeMode.Normal;
            pb1.Location = new Point(20, 20);
            pb1.Size = new Size(400, 300); // Размер по умолчанию
            this.Controls.Add(pb1);

            // Создаем кнопки
            btnClose = new Button();
            btnClose.Text = "Sulge";
            btnClose.Click += closeButton_Click;
            Controls.Add(btnClose);

            btnShowPicture = new Button();
            btnShowPicture.Text = "Näita pilti";
            btnShowPicture.Click += Click_ShowPictureButton;
            Controls.Add(btnShowPicture);

            btnClearPicture = new Button();
            btnClearPicture.Text = "Puhasta pilt";
            btnClearPicture.Click += clearButton_Click;
            Controls.Add(btnClearPicture);

            btnSetBackground = new Button();
            btnSetBackground.Text = "Määra taustavärv";
            btnSetBackground.Click += backgroundButton_Click;
            Controls.Add(btnSetBackground);

            btnRotate = new Button();
            btnRotate.Text = "Pööra 90°";
            btnRotate.Click += btnRotate_Click;
            Controls.Add(btnRotate);

            btnBlackAndWhite = new Button();
            btnBlackAndWhite.Text = "Must/Valge";
            btnBlackAndWhite.Click += btnBlackAndWhite_Click;
            Controls.Add(btnBlackAndWhite);

            // CheckBox
            chk1 = new System.Windows.Forms.CheckBox();
            chk1.Checked = false;
            chk1.Text = "Venita";
            chk1.Click += checkBox1_CheckedChanged;
            Controls.Add(chk1);

            // Добавляем функцию изменения изображения
            AddChangePictureButton();
            AddTimeDisplayFunctionality();

            // Располагаем кнопки под изображением
            UpdateButtonPositions();
        }

        private void AddChangePictureButton()
        {
            btnChangePicture = new Button();
            btnChangePicture.Text = "Vaheta pilti";
            btnChangePicture.Click += btnChangePicture_Click;
            Controls.Add(btnChangePicture);
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Pildifailid|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb1.Image = Image.FromFile(ofd.FileName);
                UpdateButtonPositions(); // Обновляем позиции кнопок после загрузки изображения
            }
        }

        // Функция для обновления расположения кнопок под изображением
        private void UpdateButtonPositions()
        {
            int btnY = pb1.Bottom + 10; // Положение кнопок под изображением

            // Располагаем кнопки
            btnShowPicture.Location = new Point(pb1.Left, btnY);
            btnClearPicture.Location = new Point(btnShowPicture.Right + 10, btnY);
            btnSetBackground.Location = new Point(btnClearPicture.Right + 10, btnY);
            btnRotate.Location = new Point(btnSetBackground.Right + 10, btnY);
            btnBlackAndWhite.Location = new Point(btnRotate.Right + 10, btnY);
            btnChangePicture.Location = new Point(btnBlackAndWhite.Right + 10, btnY);
            btnClose.Location = new Point(btnChangePicture.Right + 10, btnY);

            // CheckBox
            chk1.Location = new Point(pb1.Left, btnY + 40);
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (pb1.Image != null)
            {
                pb1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pb1.Refresh();
                UpdateButtonPositions(); // Обновляем позиции кнопок после поворота
            }
        }

        private void btnBlackAndWhite_Click(object sender, EventArgs e)
        {
            if (pb1.Image != null)
            {
                Bitmap bmp = new Bitmap(pb1.Image);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        int grayScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11));
                        Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        bmp.SetPixel(x, y, grayColor);
                    }
                }
                pb1.Image = bmp;
                UpdateButtonPositions(); // Обновляем позиции кнопок после изменения изображения
            }
        }

        private void AddTimeDisplayFunctionality()
        {
            lblTime = new Label();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblTime.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTime.Location = new Point(20, 20);
            lblTime.AutoSize = true;
            Controls.Add(lblTime);

            timeTimer.Interval = 1000; // 1 секунда
            timeTimer.Tick += timeTimer_Tick;
            timeTimer.Start();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pb1.Image = null;
            UpdateButtonPositions(); // Обновляем позиции кнопок после очистки изображения
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (cd1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = cd1.Color;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Click_ShowPictureButton(object? sender, EventArgs e)
        {
            pb1.Image = Image.FromFile(@"..\..\..\picture.jpg");
            UpdateButtonPositions(); // Обновляем позиции кнопок после загрузки изображения
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pb1.SizeMode = PictureBoxSizeMode.Normal;
            UpdateButtonPositions(); // Обновляем позиции кнопок после изменения режима
        }
    }
}
