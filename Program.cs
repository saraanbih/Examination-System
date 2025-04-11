namespace Examination_system
{
    class Program
    {
        
        static void Main(string[] args)
        {
            #region Subjects
            Subject math = new Subject(1, "Mathematics");
            Subject programming = new Subject(2, "Programming");
            #endregion

            #region Final Exam
            Console.WriteLine("=== Testing Final Exam ===");
            TestFinalExam(math);
            #endregion

            #region Practical Exam
            Console.WriteLine("\nPress any key to continue to Practical Exam...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("=== Testing Practical Exam ===");
            TestPracticalExam(programming);
            #endregion
        }

        #region Instructions
        static void ShowExamIntructions()
        {
            Console.WriteLine("\nFinal Exam Instructions:");
            Console.WriteLine("- Use 'A' to answer a question");
            Console.WriteLine("- Use 'F' to flag/unflag a question for review");
            Console.WriteLine("- Use 'P' and 'N' to navigate between questions");
            Console.WriteLine("- Use 'R' to review all your answers");
            Console.WriteLine("- Use 'S' to submit the exam");
            Console.WriteLine("\nPress any key to start the exam...");
        }
        #endregion

        #region Test Final Exam
        static void TestFinalExam(Subject subject)
        {
            FinalExam finalExam = new FinalExam(TimeSpan.FromMinutes(4), subject);

            finalExam.AddQuestion(new TrueFalse("Basic Arithmetic", "Is 2 + 2 = 4?", 1, true));
            finalExam.AddQuestion(new MCQ("Addition", "What is 3 + 5?", 3,
                new Answer[]
                {
                    new Answer(1, "6"),
                    new Answer(2, "7"),
                    new Answer(3, "8"),
                    new Answer(4, "9")
                }, 3));
            finalExam.AddQuestion(new MCQ("Multiplication", "What is 4 × 3?", 3,
                new Answer[]
                {
                    new Answer(1, "10"),
                    new Answer(2, "11"),
                    new Answer(3, "12"),
                    new Answer(4, "13")
                }, 3));
            finalExam.AddQuestion(new TrueFalse("Number Theory", "Is 15 divisible by 3?", 1, true));
            finalExam.AddQuestion(new MCQ("Algebra", "Solve for x: 2x + 5 = 13", 3,
                new Answer[]
                {
                    new Answer(1, "3"),
                    new Answer(2, "4"),
                    new Answer(3, "5"),
                    new Answer(4, "6")
                }, 2));

            subject.CreateExam(finalExam);

            ShowExamIntructions();
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Starting the Final Exam...");
            subject.Exam.ShowExam(false);
        }
        #endregion

        #region Test Practical Exam
        static void TestPracticalExam(Subject subject)
        {
            PracticalExam practicalExam = new PracticalExam(TimeSpan.FromMinutes(2), subject);

            practicalExam.AddQuestion(new MCQ("Variables", "Which of these is not a valid variable name in C#?", 2,
                new Answer[]
                {
                    new Answer(1, "_value"),
                    new Answer(2, "firstName"),
                    new Answer(3, "123variable"),
                    new Answer(4, "camelCase")
                }, 3));
            practicalExam.AddQuestion(new TrueFalse("OOP Concepts", "In C#, a class can inherit from multiple classes.", 1, false));
            practicalExam.AddQuestion(new MCQ("Arrays", "What is the output of: int[] arr = new int[3]; Console.WriteLine(arr[0]);", 3,
                new Answer[]
                {
                    new Answer(1, "0"),
                    new Answer(2, "null"),
                    new Answer(3, "undefined"),
                    new Answer(4, "Runtime Error")
                }, 1));
            practicalExam.AddQuestion(new MCQ("Methods", "Which method declaration is correct?", 4,
                new Answer[]
                {
                    new Answer(1, "void MyMethod[string param]"),
                    new Answer(2, "public void MyMethod(string param)"),
                    new Answer(3, "public: void MyMethod(string param)"),
                    new Answer(4, "void public MyMethod(string param)")
                }, 2));
            practicalExam.AddQuestion(new TrueFalse("Exception Handling", "try-catch blocks can be nested.", 1, true));

            subject.CreateExam(practicalExam);

            ShowExamIntructions();
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Starting the Practical Exam...");
            subject.Exam.ShowExam(false);
        }
        #endregion
    }
}
