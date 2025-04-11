using System.Text;
using System.Linq;

namespace Examination_system
{
    #region Abstract Base Class: Question
    abstract class Question : ICloneable, IComparable<Question>
    {
        #region Properties
        public string Header { get; }
        public string Body { get; }
        public ushort Mark { get; }
        public Dictionary<int, Answer> AnswerList { get; protected set; }
        protected int CorrectAnswerId { get; set; }
        #endregion

        #region Constructor
        public Question(string header, string body, ushort mark, Answer[] answerList, int correctAnswerId)
        {
            if (string.IsNullOrWhiteSpace(header) || string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Header and Body cannot be empty");
            if (mark == 0)
                throw new ArgumentException("Mark must be greater than 0");
            if (answerList == null || answerList.Length < 2)
                throw new ArgumentException("AnswerList must have at least 2 answers");
            if (correctAnswerId < 1 || correctAnswerId > answerList.Length)
                throw new ArgumentException("CorrectAnswerId is out of range");

            Header = header;
            Body = body;
            Mark = mark;
            AnswerList = answerList.ToDictionary(a => a.AnswerID);
            CorrectAnswerId = correctAnswerId;
        }
        #endregion

        #region Abstract Methods
        public abstract string Display();
        #endregion

        #region Utility Methods
        public string GetCorrectAnswer() => $"{CorrectAnswerId}. {AnswerList[CorrectAnswerId].AnswerText}";
        public bool IsCorrectAnswer(int answerId) => answerId == CorrectAnswerId;
        public object Clone() => MemberwiseClone();
        public int CompareTo(Question? other) => Mark.CompareTo(other?.Mark);
        public override string ToString() => $"Header: {Header}, Body: {Body}, Mark: {Mark}";
        #endregion
    }
    #endregion

    #region MCQ Class
    class MCQ : Question
    {
        #region Constructor
        public MCQ(string header, string body, ushort mark, Answer[] options, int correctAnswerId)
            : base(header, body, mark, options, correctAnswerId) { }
        #endregion

        #region Override Methods
        public override string Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Question Header: {Header}");
            sb.AppendLine($"Question: {Body}");
            sb.AppendLine("Choose one of the following:");

            // Display answers in order of their IDs
            foreach (var answer in AnswerList.OrderBy(a => a.Key))
            {
                sb.AppendLine($"{answer.Key}. {answer.Value.AnswerText}");
            }

            sb.AppendLine($"[{Mark} Marks]");
            return sb.ToString();
        }
        #endregion
    }
    #endregion

    #region TrueFalse Class
    class TrueFalse : Question
    {
        #region Constructor
        public TrueFalse(string header, string body, ushort mark, bool correct)
            : base(header, body, mark, new Answer[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            }, correct ? 1 : 2)
        { }
        #endregion

        #region Override Methods
        public override string Display() => $"True/False: {Header}\n{Body}\n1. True\n2. False";
        #endregion
    }
    #endregion
}
