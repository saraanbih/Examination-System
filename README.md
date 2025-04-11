# 📝 Examination System

A C# Console-Based Examination System that simulates both **Final Exams** and **Practical Exams** with interactive features such as:
- Timed exams ⏱️
- Multiple question types 🧠
- User navigation and flagging 🔖
- Result grading and statistics 📊

---

## 🚀 Features

- ✅ **True/False and MCQ Questions**
- 🧩 **Flag questions** for review
- 🔄 Navigate between questions (Previous/Next)
- ⏰ **Automatic countdown timer** with time warnings
- 🧮 **Real-time progress tracking**
- 📈 **Detailed results summary** (score, percentage, letter grade)
- 🧪 **Supports multiple exam types** (`FinalExam`, `PracticalExam`)
- 🧬 Deep cloning for safe object duplication

---

## 📸 Demo Screens

> 💡 Add screenshots or screen recordings here showing:
> - Instructions
> - Exam navigation
> - Flagging questions
> - Final results

---

## 🧠 Technologies Used

- **Language:** C#
- **Platform:** .NET Console Application
- **OOP Concepts:** Inheritance, Polymorphism, Interfaces, Deep Copying
- **Design Pattern:** Cloning (Prototype)

---

## 🏗️ Project Structure

```bash
Examination_System/
│
├── Program.cs               # Entry point: simulates final and practical exams
├── Subject.cs               # Represents a subject with an associated exam
├── Exam.cs                  # Abstract base class for exams (timer logic, grading, etc.)
├── FinalExam.cs             # Concrete class for final exam logic
├── PracticalExam.cs         # Concrete class for practical exam logic
├── Question.cs              # Abstract base class for questions
├── TrueFalse.cs             # True/False question implementation
├── MCQ.cs                   # Multiple choice question implementation
├── Answer.cs                # Represents an answer choice for MCQs
└── ... (other supporting files)
```

---

## 🧪 How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/saraanbih/Examination_System.git
   cd Examination_System
   ```

2. Open the solution in **Visual Studio** or run via **.NET CLI**:

   ```bash
   dotnet build
   dotnet run
   ```

---

## 🗺️ Navigation Controls

| Key | Action                      |
|-----|-----------------------------|
| A   | Answer the current question |
| F   | Flag/Unflag for review      |
| N   | Next question               |
| P   | Previous question           |
| R   | Review answers              |
| S   | Submit the exam             |

---

## 📚 Future Improvements

- 🔒 User login and authentication
- 📋 Exam history and report exporting (PDF/CSV)
- 🌐 GUI version (WinForms/WPF or ASP.NET)
- 🧠 AI-generated feedback on wrong answers

---

## 🤝 Contributing

Contributions are welcome! Feel free to fork the repo and submit a pull request. If you find a bug or want a feature added, open an issue

---

## ✨ Author

Developed by **[Sara Nabih]**

> 📧 [Email](nabihsara8@gmail.com)
