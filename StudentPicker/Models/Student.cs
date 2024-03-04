using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPicker.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }

        public Student(string name, string surname, int number)
        {
            Name = name;
            Surname = surname;
            Id = number;
        }
    }
}
