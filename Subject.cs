namespace Examination_system
{
    class Subject : ICloneable
    {
        #region Properties
        public int SubjectID { get; }
        public string SubjectName { get; set; }
        public Exam? Exam { get; private set; }
        #endregion

        #region Constructor
        public Subject(int subjectID, string subjectName)
        {
            SubjectID = subjectID;
            SubjectName = subjectName;
        }
        #endregion

        #region Creation & Information
        public void CreateExam(Exam exam) => Exam = exam;

        public void SubjectInfo()
        {
            Console.WriteLine($"Subject ID: {SubjectID}, Name: {SubjectName}");
            Exam?.ShowExam(false);
        }
        #endregion

        #region Cloning
        // Deep copy => Create a new Subject with its own Exam copy
        public object Clone() =>
            new Subject(SubjectID, SubjectName)
            {
                Exam = (Exam?)Exam?.Clone()
            };
        #endregion
    }
}
