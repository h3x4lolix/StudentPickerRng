using StudentPicker.Models;

namespace StudentPicker
{
    public partial class MainPage : ContentPage
    {
        private string studentClass;
        private Students students;
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public MainPage()
        {
            InitializeComponent();
            students = new Students();
        }
        private void OnAddStudentClicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text;
            string surname = surnameEntry.Text;
            int number = int.Parse(numberEntry.Text);
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
            {
                students.AddStudent(name, surname, number);
                nameEntry.Text = string.Empty;
                surnameEntry.Text = string.Empty;
                numberEntry.Text = (students.GetStudents().Count() + 1).ToString();
                DisplayAlert("sukces", "dodano ucznia do listy", "OK");
            }
            else
            {
                DisplayAlert("błąd", "brak imienia lub nazwiska", "OK");
            }
        }
        public void OnShowStudentsClicked(object sender, EventArgs e)
        {
            List<string> studentsList = new List<string>();
            foreach (var student in students.GetStudents())    
            {
                studentsList.Add($"{student.Id} : {student.Name} {student.Surname}");    
            }
            string studentsString = string.Join("\n", studentsList);
            DisplayAlert("Lista uczniów", studentsString, "OK");
        }
        private void OnPickRandomStudentClicked(object sender, EventArgs e)
        {
            if(students.GetStudents().Count != 0)
            {
                Student randomStudent = students.RandomStudent(); 
                if (randomStudent != null)
                {
                    DisplayAlert("wynik", $"uczeń: {randomStudent.Id}. {randomStudent.Name} {randomStudent.Surname}", "OK");
                }
                else
                {
                    DisplayAlert("błąd", "Brak osób do wylosowania", "OK");
                }
            }
            else
            {
                DisplayAlert("błąd", "Brak osób do wylosowania", "OK");
            }


        }
        private async void OnDeleteStudentClicked(object sender, EventArgs e)
        {
            if (students.GetStudents().Count() != 0)
            {
                var studentsList = students.GetStudents().Select(s => $"{s.Id} : {s.Name} {s.Surname}").ToList();
                var result = await DisplayActionSheet("usuń ucznia", "Anuluj", null, studentsList.ToArray());

                if (!result.Equals("Anuluj"))
                {
                    var parts = result.Split(' ');
                    Console.WriteLine(parts[0]);

                    int number = int.Parse(parts[0]);
                    string surname = parts[2];
                    students.DeleteStudent(number);
                    await DisplayAlert("Sukces!", "Uczeń usunięty", "OK");                   
                }
            }
            else
            {
                await DisplayAlert("Błąd!", "Lista nie zawiera żadnych uczniów", "OK");
            }
        }
        private void OnSaveFileClicked(object sender, EventArgs e)
        {
            students.SaveFile(filePath, studentClass);
            DisplayAlert("Sukces!", "Lista studentów została zapisana", "OK");
        }
        private void onPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            studentClass = (string)picker.ItemsSource[picker.SelectedIndex];
        }
        private void OnLoadFileClicked(object sender, EventArgs e)
        {
            students.LoadFile(filePath, studentClass);
            numberEntry.Text = (students.GetStudents().Count() + 1).ToString();
            DisplayAlert("Sukces!", "Lista studentów została załadowana", "OK");
        }

    }
}
