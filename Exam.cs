namespace Examination_system
{
    abstract class Exam : ICloneable
    {
        #region Properties
        public TimeSpan Duration { get; }
        public Subject Subject { get; }
        protected List<Question> Questions { get; private set; } = new List<Question>();
        public int NumberOfQuestions => Questions.Count;
        protected Dictionary<Question, int> UserAnswers { get; private set; } = new Dictionary<Question, int>();
        protected Dictionary<Question, bool> FlaggedQuestions { get; private set; } = new Dictionary<Question, bool>();
        protected int CurrentQuestionIndex { get; set; } = 0;
        #endregion

        #region Timer Fields
        private System.Timers.Timer examTimer;
        private bool timeUp = false;
        private DateTime examStartTime;
        #endregion

        #region Constructor
        protected Exam(TimeSpan duration, Subject subject)
        {
            Duration = duration;
            Subject = subject;

            examTimer = new System.Timers.Timer(1000); // Update every second
            examTimer.Elapsed += (sender, e) =>
            {
                var remainingTime = Duration - (DateTime.Now - examStartTime);
                if (remainingTime <= TimeSpan.Zero)
                {
                    timeUp = true;
                    Console.WriteLine("\nTime's up! The exam has ended.");
                    examTimer.Stop();
                }
                else if (remainingTime.TotalMinutes == 15 || remainingTime.TotalMinutes == 5 || remainingTime.TotalMinutes == 1)
                {
                    Console.WriteLine($"\nWarning: {remainingTime.TotalMinutes} minute(s) remaining!");
                }
            };
            examTimer.AutoReset = true;
        }
        #endregion

        #region Abstract Methods
        public abstract void ShowExam(bool finished);
        #endregion

        #region Question Management
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
            FlaggedQuestions[question] = false;
        }

        protected List<Question> GetShuffledQuestions()
        {
            var shuffled = Questions.ToList();
            Random rand = new Random();
            int n = shuffled.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }
            return shuffled;
        }

        protected void FlagQuestion(Question question)
        {
            FlaggedQuestions[question] = !FlaggedQuestions[question];
        }

        protected string GetProgressInfo()
        {
            return $"Question {CurrentQuestionIndex + 1}/{NumberOfQuestions}";
        }
        #endregion

        #region Timer Management
        protected void StartTimer()
        {
            examStartTime = DateTime.Now;
            examTimer.Start();
        }

        protected bool IsTimeUp() => timeUp;

        protected TimeSpan GetRemainingTime() =>
            timeUp ? TimeSpan.Zero : Duration - (DateTime.Now - examStartTime);
        #endregion

        #region Grading & Statistics
        protected (int totalGrade, int maxGrade, double percentage) CalculateGrade()
        {
            int totalGrade = 0;
            int maxGrade = 0;

            foreach (var question in Questions)
            {
                if (UserAnswers.ContainsKey(question) && question.IsCorrectAnswer(UserAnswers[question]))
                    totalGrade += question.Mark;
                maxGrade += question.Mark;
            }

            double percentage = maxGrade > 0 ? (double)totalGrade / maxGrade * 100 : 0;
            return (totalGrade, maxGrade, percentage);
        }

        protected string GetLetterGrade(double percentage)
        {
            return percentage switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }

        protected string GetExamStatistics()
        {
            int correct = 0, incorrect = 0, unanswered = 0;

            foreach (var question in Questions)
            {
                if (!UserAnswers.ContainsKey(question) || UserAnswers[question] == -1)
                    unanswered++;
                else if (question.IsCorrectAnswer(UserAnswers[question]))
                    correct++;
                else
                    incorrect++;
            }

            return $"Statistics:\n" +
                   $"Correct Answers: {correct}\n" +
                   $"Incorrect Answers: {incorrect}\n" +
                   $"Unanswered Questions: {unanswered}";
        }
        #endregion

        #region Cloning
        public object Clone()
        {
            var clone = (Exam)MemberwiseClone();
            clone.Questions = new List<Question>(Questions.Select(q => (Question)q.Clone()));
            clone.UserAnswers = new Dictionary<Question, int>(UserAnswers);
            clone.FlaggedQuestions = new Dictionary<Question, bool>(FlaggedQuestions);
            clone.examTimer = new System.Timers.Timer(1000);
            return clone;
        }
        #endregion
    }
}
