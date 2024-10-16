using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elemendid_vorm_TARpv23
{
    public partial class KolmasVorm : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        string firstChoice;
        string secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        Label lblStatus;
        Label lblTimeLeft;
        System.Windows.Forms.Timer GameTimer;
        int totalTime = 60;
        int countDownTime;
        bool gameOver = false;

        Button btnRestart;
        Button btnCheckAnswers; // Кнопка для проверки ответов
        int matches = 0;
        Label lblMatched;

        public KolmasVorm(int width, int height) // Конструктор с параметрами
        {
            InitializeComponent(); // Инициализация компонентов
            this.ClientSize = new Size(width, height); // Устанавливаем размер окна
            GameTimer = new System.Windows.Forms.Timer(); // Инициализация таймера
            GameTimer.Interval = 1000; // Установка интервала в 1 секунду
            GameTimer.Tick += TimerEvent; // Подписка на событие таймера

            lblStatus = new Label
            {
                Location = new Point(20, 200),
                Size = new Size(200, 30)
            };
            lblTimeLeft = new Label
            {
                Location = new Point(20, 230),
                Size = new Size(200, 30)
            };
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblTimeLeft);

            // Добавляем кнопку Restart, кнопку для проверки ответов и счетчик совпадений
            AddRestartButton();
            AddCheckAnswersButton(); // Добавляем кнопку для проверки ответов
            AddMatchedLabel();

            LoadPictures(); // Загружаем картинки
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            lblTimeLeft.Text = "Time Left: " + countDownTime;
            if (countDownTime < 1)
            {
                GameOver("Time's Up, You Lose");
                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile(@"..\..\..\" + (string)x.Tag + ".png");
                    }
                }
            }
        }

        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 12; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 50;
                newPic.Width = 50;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 4)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos += 60;
                }
                if (rows == 4)
                {
                    leftPos = 20;
                    topPos += 60;
                    rows = 0;
                }
            }
            RestartGame();
        }

        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                // Не регистрировать клик, если игра окончена
                return;
            }
            if (firstChoice == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile(@"..\..\..\" + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Image == null)
                {
                    picB.Image = Image.FromFile(@"..\..\..\" + (string)picB.Tag + ".png");
                    secondChoice = (string)picB.Tag;
                }
            }
            else
            {
                CheckPictures(picA, picB);
            }
        }

        private void RestartGame()
        {
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            numbers = randomList;
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();
            }
            tries = 0;
            matches = 0; // Обнуляем количество совпадений
            lblStatus.Text = "Mismatched: " + tries + " times.";
            lblMatched.Text = "Matched: 0 pairs";
            lblTimeLeft.Text = "Time Left: " + totalTime;
            gameOver = false;
            countDownTime = totalTime;
            GameTimer.Start(); // Запуск таймера
        }

        private void AddRestartButton()
        {
            btnRestart = new Button();
            btnRestart.Text = "Restart";
            btnRestart.Size = new Size(100, 30);
            btnRestart.Location = new Point(300, 400); // Размести кнопку рядом с таймером
            btnRestart.Click += btnRestart_Click;
            this.Controls.Add(btnRestart);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame(); // Перезапуск игры
        }

        private void AddCheckAnswersButton()
        {
            btnCheckAnswers = new Button
            {
                Text = "Check Answers",
                Size = new Size(120, 30),
                Location = new Point(420, 400) // Размести кнопку рядом с кнопкой перезапуска
            };
            btnCheckAnswers.Click += CheckAnswersButton_Click;
            this.Controls.Add(btnCheckAnswers);
        }

        private void CheckAnswersButton_Click(object sender, EventArgs e)
        {
            // Проверяем, решены ли все элементы
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
            else
            {
                MessageBox.Show("There are still unmatched pairs!", "Check Answers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddMatchedLabel()
        {
            lblMatched = new Label
            {
                Text = "Matched: 0 pairs",
                Location = new Point(20, 260),
                Size = new Size(200, 30)
            };
            this.Controls.Add(lblMatched);
        }

        private void UpdateMatchedLabel()
        {
            matches++;
            lblMatched.Text = "Matched: " + matches + " pairs";
        }

        private void ShowResultsTable()
        {
            string message = $"Game Over! Results:\nMismatched: {tries} times\nMatched: {matches} pairs\nTime Left: {countDownTime} seconds";
            MessageBox.Show(message, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GameOver(string msg)
        {
            GameTimer.Stop(); // Остановка таймера
            gameOver = true; // Игра окончена

            ShowResultsTable(); // Показываем таблицу результатов
        }

        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null;
                UpdateMatchedLabel(); // Увеличиваем счетчик совпадений
            }
            else
            {
                tries++;
                lblStatus.Text = "Mismatched " + tries + " times.";
            }
            firstChoice = null;
            secondChoice = null;
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }
            // Проверяем, решены ли все элементы
            if (pictures.All(o => o.Tag == null))
            {
                GameOver("Great Work, You Win!!!!");
            }
        }
    }
}
