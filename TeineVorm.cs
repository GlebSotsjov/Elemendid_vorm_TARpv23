using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vorm_TARpv23
{
    public partial class TeineVorm : Form
    {
        Label timeLabel;
        Label plusLeftLabel, plusRightLabel;
        Label minusLeftLabel, minusRightLabel;
        Label timesLeftLabel, timesRightLabel;
        Label dividedLeftLabel, dividedRightLabel;
        NumericUpDown sum, difference, product, quotient;
        Button startButton, resetButton, hintButton, submitButton;
        System.Windows.Forms.Timer quizTime;
        int timeLeft;

        public TeineVorm(int h, int w)
        {
            this.Height = h;
            this.Width = w;
            this.Text = "Math Quiz";

            timeLabel = new Label();
            timeLabel.Text = "Time Left";
            timeLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            timeLabel.Size = new Size(200, 50);
            timeLabel.Location = new Point(50, 20);
            Controls.Add(timeLabel);

            CreateMathQuestion(out plusLeftLabel, out plusRightLabel, out sum, "+", 50, 100);
            CreateMathQuestion(out minusLeftLabel, out minusRightLabel, out difference, "-", 50, 150);
            CreateMathQuestion(out timesLeftLabel, out timesRightLabel, out product, "×", 50, 200);
            CreateMathQuestion(out dividedLeftLabel, out dividedRightLabel, out quotient, "÷", 50, 250);

            // Start button
            startButton = new Button();
            startButton.Text = "Start Quiz";
            startButton.Size = new Size(100, 50);
            startButton.Location = new Point(200, 400);
            startButton.Click += new EventHandler(StartButton_Click);
            Controls.Add(startButton);

            // Reset button
            resetButton = new Button();
            resetButton.Text = "Reset Quiz";
            resetButton.Size = new Size(100, 50);
            resetButton.Location = new Point(320, 400);
            resetButton.Click += new EventHandler(ResetButton_Click);
            Controls.Add(resetButton);

            // Hint button
            hintButton = new Button();
            hintButton.Text = "Show Hints";
            hintButton.Size = new Size(100, 50);
            hintButton.Location = new Point(440, 400);
            hintButton.Click += new EventHandler(HintButton_Click);
            Controls.Add(hintButton);

            // Submit button for early answer submission
            submitButton = new Button();
            submitButton.Text = "Submit Answers";
            submitButton.Size = new Size(150, 50);
            submitButton.Location = new Point(560, 400);
            submitButton.Click += new EventHandler(SubmitButton_Click);
            Controls.Add(submitButton);

            // Timer
            quizTime = new System.Windows.Forms.Timer();
            quizTime.Interval = 1000; // 1 second
            quizTime.Tick += new EventHandler(Timer_Tick);
        }

        private void CreateMathQuestion(out Label leftLabel, out Label rightLabel, out NumericUpDown answerBox, string operation, int x, int y)
        {
            leftLabel = new Label();
            leftLabel.Text = "?";
            leftLabel.Font = new Font("Arial", 18);
            leftLabel.Location = new Point(x, y);
            leftLabel.Size = new Size(60, 50);
            Controls.Add(leftLabel);

            rightLabel = new Label();
            rightLabel.Text = "?";
            rightLabel.Font = new Font("Arial", 18);
            rightLabel.Location = new Point(x + 100, y);
            rightLabel.Size = new Size(60, 50);
            Controls.Add(rightLabel);

            Label operatorLabel = new Label();
            operatorLabel.Text = operation;
            operatorLabel.Font = new Font("Arial", 18);
            operatorLabel.Location = new Point(x + 60, y);
            operatorLabel.Size = new Size(40, 50);
            Controls.Add(operatorLabel);

            answerBox = new NumericUpDown();
            answerBox.Size = new Size(100, 50);
            answerBox.Location = new Point(x + 200, y);
            answerBox.Maximum = 1000; // Установите максимальное значение 1000
            Controls.Add(answerBox);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            plusLeftLabel.Text = random.Next(1, 100).ToString();
            plusRightLabel.Text = random.Next(1, 100).ToString();

            minusLeftLabel.Text = random.Next(1, 100).ToString();
            minusRightLabel.Text = random.Next(1, 100).ToString();

            timesLeftLabel.Text = random.Next(1, 100).ToString();
            timesRightLabel.Text = random.Next(1, 10).ToString();

            dividedLeftLabel.Text = random.Next(1, 100).ToString();
            dividedRightLabel.Text = random.Next(1, 10).ToString();

            timeLeft = 30; // 30 seconds for the quiz
            quizTime.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = "Time Left: " + timeLeft + " seconds";
            }
            else
            {
                quizTime.Stop();
                MessageBox.Show("Time's up!");
                CheckAnswers();
            }
        }

        // Function 1: Check answers
        private void CheckAnswers()
        {
            int correctAnswers = 0;

            if (sum.Value == (int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text)))
                correctAnswers++;

            if (difference.Value == (int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text)))
                correctAnswers++;

            if (product.Value == (int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text)))
                correctAnswers++;

            if (quotient.Value == (int.Parse(dividedLeftLabel.Text) / int.Parse(dividedRightLabel.Text)))
                correctAnswers++;

            MessageBox.Show($"You got {correctAnswers} correct out of 4!");
        }

        // Function 2: Reset quiz
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetQuiz();
        }

        private void ResetQuiz()
        {
            sum.Value = 0;
            difference.Value = 0;
            product.Value = 0;
            quotient.Value = 0;

            plusLeftLabel.Text = "?";
            plusRightLabel.Text = "?";
            minusLeftLabel.Text = "?";
            minusRightLabel.Text = "?";
            timesLeftLabel.Text = "?";
            timesRightLabel.Text = "?";
            dividedLeftLabel.Text = "?";
            dividedRightLabel.Text = "?";

            timeLabel.Text = "Time Left: 30 seconds";
            timeLeft = 30;
            quizTime.Stop(); // Остановить таймер, если викторина сбрасывается
        }

        // Function 3: Show hints
        private void HintButton_Click(object sender, EventArgs e)
        {
            ShowHints();
        }

        private void ShowHints()
        {
            MessageBox.Show($"Sum: {int.Parse(plusLeftLabel.Text) + int.Parse(plusRightLabel.Text)}\n" +
                            $"Difference: {int.Parse(minusLeftLabel.Text) - int.Parse(minusRightLabel.Text)}\n" +
                            $"Product: {int.Parse(timesLeftLabel.Text) * int.Parse(timesRightLabel.Text)}\n" +
                            $"Quotient: {int.Parse(dividedLeftLabel.Text) / int.Parse(dividedRightLabel.Text)}");
        }

        // Function 4: Early submission of answers
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            quizTime.Stop();  // Остановим таймер
            CheckAnswers();    // Выполним проверку ответов
        }
    }
}
