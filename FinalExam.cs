using System.Text;

namespace Examination_system
{
    class FinalExam : Exam
    {
        #region Constructor
        public FinalExam(TimeSpan duration, Subject subject) : base(duration, subject) { }
        #endregion

        #region Show Exam & Results
        public override void ShowExam(bool finished)
        {
            Console.WriteLine("\n========= Final Exam =========\n");
            Console.WriteLine($"----------{Subject.SubjectName}----------");
            Console.WriteLine($"Duration: {Duration.TotalMinutes} minutes\n");

            if (!finished)
            {
                StartTimer();
                bool examCompleted = false;

                while (!examCompleted && !IsTimeUp())
                {
                    Console.Clear();
                    Console.WriteLine($"\n{GetProgressInfo()}");
                    Console.WriteLine($"Time Remaining: {GetRemainingTime().ToString(@"hh\:mm\:ss")}");
                    
                    var currentQuestion = Questions[CurrentQuestionIndex];
                    Console.WriteLine(currentQuestion.Display());
                    
                    if (FlaggedQuestions[currentQuestion])
                        Console.WriteLine("[FLAGGED FOR REVIEW]");

                    if (UserAnswers.ContainsKey(currentQuestion))
                    {
                        int answer = UserAnswers[currentQuestion];
                        Console.WriteLine($"\nYour current answer: {answer}. {currentQuestion.AnswerList[answer].AnswerText}");
                    }

                    ShowExamOptions();
                    HandleUserInput(ref examCompleted, currentQuestion);

                    if (!examCompleted && !IsTimeUp())
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
            }

            ShowResults();
        }

        private void ShowExamOptions()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("A. Answer question");
            Console.WriteLine("F. Flag/Unflag question for review");
            Console.WriteLine("P. Previous question");
            Console.WriteLine("N. Next question");
            Console.WriteLine("R. Review all answers");
            Console.WriteLine("S. Submit exam");
            
            Console.Write("\nEnter your choice: ");
        }

        private void HandleUserInput(ref bool examCompleted, Question currentQuestion)
        {
            string choice = Console.ReadLine()?.ToUpper() ?? "";

            switch (choice)
            {
                case "A":
                    HandleAnswerInput(currentQuestion);
                    break;

                case "F":
                    FlagQuestion(currentQuestion);
                    Console.WriteLine(FlaggedQuestions[currentQuestion] ? 
                        "Question flagged for review." : 
                        "Question unflagged.");
                    break;

                case "P":
                    if (CurrentQuestionIndex > 0)
                        CurrentQuestionIndex--;
                    break;

                case "N":
                    if (CurrentQuestionIndex < NumberOfQuestions - 1)
                        CurrentQuestionIndex++;
                    break;

                case "R":
                    ReviewAnswers(Questions);
                    break;

                case "S":
                    if (ConfirmSubmission())
                        examCompleted = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void HandleAnswerInput(Question currentQuestion)
        {
            Console.Write("Enter your answer (enter the option number): ");
            if (int.TryParse(Console.ReadLine(), out int answer) && 
                answer >= 1 && answer <= currentQuestion.AnswerList.Count)
            {
                UserAnswers[currentQuestion] = answer;
                Console.WriteLine("Answer recorded.");
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a number between 1 and {currentQuestion.AnswerList.Count}.");
            }
        }
        #endregion

        #region Review & Submission
        private void ReviewAnswers(List<Question> questions)
        {
            Console.Clear();
            Console.WriteLine("\n===== Answer Review =====\n");
            
            for (int i = 0; i < questions.Count; i++)
            {
                var question = questions[i];
                Console.WriteLine($"\nQuestion {i + 1}/{questions.Count}");
                Console.WriteLine(question.Display());
                
                if (UserAnswers.ContainsKey(question))
                {
                    int answer = UserAnswers[question];
                    Console.WriteLine($"Your Answer: {answer}. {question.AnswerList[answer].AnswerText}");
                }
                else
                {
                    Console.WriteLine("Not answered yet");
                }

                if (FlaggedQuestions[question])
                    Console.WriteLine("[FLAGGED FOR REVIEW]");

                Console.WriteLine("------------------------");
            }

            Console.WriteLine("\nPress any key to return to the exam...");
            Console.ReadKey();
        }

        private bool ConfirmSubmission()
        {
            int unanswered = Questions.Count(q => !UserAnswers.ContainsKey(q));
            int flagged = FlaggedQuestions.Count(f => f.Value);

            Console.Clear();
            Console.WriteLine("\n===== Submission Confirmation =====");
            Console.WriteLine($"\nUnanswered questions: {unanswered}");
            Console.WriteLine($"Flagged questions: {flagged}");
            Console.Write("\nAre you sure you want to submit the exam? (Y/N): ");

            return Console.ReadLine()?.Trim().ToUpper() == "Y";
        }
        #endregion

        #region Results (Show, Generation, Save)
        private void ShowResults()
        {
            var (totalGrade, maxGrade, percentage) = CalculateGrade();
            string letterGrade = GetLetterGrade(percentage);

            string resultText = GenerateResultHeader(totalGrade, maxGrade, percentage, letterGrade) +
                              GenerateDetailedResults();

            Console.WriteLine(resultText);
            SaveResultsToFile(resultText);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private string GenerateResultHeader(int totalGrade, int maxGrade, double percentage, string letterGrade)
        {
            return $"\nFinal Exam Results - {Subject.SubjectName}\n" +
                   $"Duration: {Duration.TotalMinutes} minutes\n\n" +
                   $"Grade: {totalGrade}/{maxGrade} ({percentage:F1}%)\n" +
                   $"Letter Grade: {letterGrade}\n\n" +
                   GetExamStatistics() + "\n\n" +
                   "Detailed Question Review:\n" +
                   "=======================\n\n";
        }

        private string GenerateDetailedResults()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var question in Questions)
            {
                sb.AppendLine(question.Display());
                if (UserAnswers.ContainsKey(question))
                {
                    int userAnswer = UserAnswers[question];
                    sb.AppendLine($"Your Answer: {userAnswer}. {question.AnswerList[userAnswer].AnswerText}");
                    sb.AppendLine($"Correct Answer: {question.GetCorrectAnswer()}");
                    sb.AppendLine($"Result: {(question.IsCorrectAnswer(userAnswer) ? "Correct" : "Incorrect")}");
                }
                else
                {
                    sb.AppendLine("Your Answer: Not answered");
                    sb.AppendLine($"Correct Answer: {question.GetCorrectAnswer()}");
                    sb.AppendLine("Result: Incorrect");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void SaveResultsToFile(string resultText)
        {
            string resultsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Results");
            Directory.CreateDirectory(resultsDirectory);

            string fileName = $"FinalExam_{Subject.SubjectName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(resultsDirectory, fileName);
            File.WriteAllText(filePath, resultText);
            Console.WriteLine($"Results saved to: {filePath}");
        }
        #endregion
    }
}