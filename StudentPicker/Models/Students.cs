using StudentPicker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPicker.Models
{

    internal class Students
    {
        private List<Student> students;

        public Students()
        {
            students = new List<Student>();
        }
        public void AddStudent(string name, string surname, int number)
        {
            var student = new Student(name, surname, number);

            students.Add(student);
        }
        public Student RandomStudent()
        {
            Random random = new Random();
            int index = random.Next(0, students.Count);
            var pickedStudent = students[index];

            return pickedStudent;
        }
        public void SaveFile(string filePath, string studentClass)
        {
            File.WriteAllLines(Path.Combine(filePath, studentClass + ".txt"), students.Select(s => $"{s.Name},{s.Surname},{s.Id}"));
        }
        public void LoadFile(string filePath, string studentClass)
        {
            if (File.Exists(Path.Combine(filePath, studentClass + ".txt")))
            {
                students.Clear();
                foreach (var line in File.ReadAllLines(Path.Combine(filePath, studentClass + ".txt")))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        AddStudent(parts[0], parts[1], int.Parse(parts[2]));
                    }
                }
            }
        }
        public void DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(student => student.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
        }
        public List<Student> GetStudents()
        {
            return students;
        }
    }
}
