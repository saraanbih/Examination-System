namespace Examination_system
{
    class Answer : ICloneable
    {
        #region Properties
        public int AnswerID { get; }
        public string AnswerText { get; }
        #endregion

        #region Constructor
        public Answer(int id, string text)
        {
            if (id < 1)
                throw new ArgumentException("Answer ID must be 1 or greater");

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Answer text cannot be empty");

            AnswerID = id;
            AnswerText = text;
        }
        #endregion

        #region Clone & Printing
        public object Clone() => MemberwiseClone();

        public override string ToString() => $"{AnswerID}. {AnswerText}";
        #endregion
    }
}
