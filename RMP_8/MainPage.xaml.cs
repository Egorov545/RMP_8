using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace RMP_8
{
    public partial class MainPage : ContentPage
    {
        private string _selectedPhotoPath;
        public MainPage()
        {
            InitializeComponent();            
        }
        private async void studentPhoto_Tapped(object sender, EventArgs e)
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Выберите фото студента",
                    FileTypes = FilePickerFileType.Images,
                };

                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    _selectedPhotoPath = result.FullPath;
                    studentPhoto.Source = ImageSource.FromFile(_selectedPhotoPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось выбрать фото: {ex.Message}", "OK");
            }
        }
        private void saveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastName.Text) ||
                string.IsNullOrWhiteSpace(firstName.Text))
            {
                DisplayAlert("Ошибка", "Заполните обязательные поля: Фамилия и Имя", "OK");
                return;
            }

            if (storageMethodPicker.SelectedIndex == 0)
            {                
                SaveUsingPreferences();
            }
            else
            {                
                SaveUsingFile();
            }

            string studentInfo = $"Студент: {lastName.Text} {firstName.Text} {middleName.Text}\n" +
                               $"Дата рождения: {birthDate.Date:dd.MM.yyyy}\n" +
                               $"Пол: {genderPicker.SelectedItem ?? "не указан"}\n" +
                               $"Общежитие: {(dormitorySwitch.IsToggled ? "Нужно" : "Не нужно")}\n" +
                               $"Староста: {(monitorSwitch.IsToggled ? "Да" : "Нет")}\n" +
                               $"Оценки - Математика: {mathGrade.SelectedItem ?? "не указана"}, " +
                               $"Русский: {russianGrade.SelectedItem ?? "не указана"}\n\n" +
                               $"Сохранено через: {(storageMethodPicker.SelectedIndex == 0 ? "Preferences" : "Файл student_data.txt")}";

            DisplayAlert("Данные сохранены", studentInfo, "OK");
        }

        private void SaveUsingPreferences()
        {
            Preferences.Default.Set("lastName", lastName.Text);
            Preferences.Default.Set("firstName", firstName.Text);
            Preferences.Default.Set("middleName", middleName.Text);
            Preferences.Default.Set("birthDate", birthDate.Date);            
            Preferences.Default.Set("genderIndex", genderPicker.SelectedIndex);            
            Preferences.Default.Set("dormitory", dormitorySwitch.IsToggled);
            Preferences.Default.Set("monitor", monitorSwitch.IsToggled);            
            Preferences.Default.Set("mathGradeIndex", mathGrade.SelectedIndex);
            Preferences.Default.Set("russianGradeIndex", russianGrade.SelectedIndex);
            Preferences.Default.Set("photoPath", _selectedPhotoPath ?? "");
        }
       
        private void SaveUsingFile()
        {
            try
            {
                string filePath = "/storage/emulated/0/Documents/student_data.txt";                               
                StreamWriter outFile = new StreamWriter(filePath);

                outFile.WriteLine(lastName.Text);
                outFile.WriteLine(firstName.Text);
                outFile.WriteLine(middleName.Text);
                outFile.WriteLine(genderPicker.SelectedItem ?? "не указан");
                outFile.WriteLine(birthDate.Date.ToString("dd.MM.yyyy"));
                outFile.WriteLine(dormitorySwitch.IsToggled ? "Нужно" : "Не нужно");
                outFile.WriteLine(monitorSwitch.IsToggled ? "Да" : "Нет");
                outFile.WriteLine(mathGrade.SelectedItem ?? "не указана");
                outFile.WriteLine(russianGrade.SelectedItem ?? "не указана");
                outFile.Close();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Не удалось сохранить файл: {ex.Message}", "OK");
            }
        }

        private void LoadUsingPreferences()
        {
            lastName.Text = Preferences.Default.Get("lastName", "");
            firstName.Text = Preferences.Default.Get("firstName", "");
            middleName.Text = Preferences.Default.Get("middleName", "");
            genderPicker.SelectedIndex = Preferences.Default.Get("gender", -1);

            DateTime savedDate = Preferences.Default.Get("birthDate", DateTime.Now);
            birthDate.Date = savedDate;

            dormitorySwitch.IsToggled = Preferences.Default.Get("dormitory", false);
            monitorSwitch.IsToggled = Preferences.Default.Get("monitor", false);
            mathGrade.SelectedIndex = Preferences.Default.Get("mathGrade", -1);
            russianGrade.SelectedIndex = Preferences.Default.Get("russianGrade", -1);

            _selectedPhotoPath = Preferences.Default.Get("photoPath", "");
            if (!string.IsNullOrEmpty(_selectedPhotoPath) && File.Exists(_selectedPhotoPath))
                studentPhoto.Source = ImageSource.FromFile(_selectedPhotoPath);
        }
        
        private void LoadUsingFile()
        {
            string filePath = "/storage/emulated/0/Documents/student_data.txt";
            if (File.Exists(filePath))
            {
                try
                {
                    StreamReader inFile = new StreamReader(filePath);
                    lastName.Text = inFile.ReadLine();
                    firstName.Text = inFile.ReadLine();
                    middleName.Text = inFile.ReadLine();

                    if (int.TryParse(inFile.ReadLine(), out int genderIndex))
                        genderPicker.SelectedIndex = genderIndex;

                    if (DateTime.TryParse(inFile.ReadLine(), out DateTime birthDateValue))
                        birthDate.Date = birthDateValue;

                    if (bool.TryParse(inFile.ReadLine(), out bool dormitoryValue))
                        dormitorySwitch.IsToggled = dormitoryValue;

                    if (bool.TryParse(inFile.ReadLine(), out bool monitorValue))
                        monitorSwitch.IsToggled = monitorValue;

                    if (int.TryParse(inFile.ReadLine(), out int mathIndex))
                        mathGrade.SelectedIndex = mathIndex;

                    if (int.TryParse(inFile.ReadLine(), out int russianIndex))
                        russianGrade.SelectedIndex = russianIndex;

                    inFile.Close();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Ошибка", $"Не удалось загрузить из файла: {ex.Message}", "OK");
                }
            }
        }

        private void clearButton_Clicked(object sender, EventArgs e)
        {
            lastName.Text = string.Empty;
            firstName.Text = string.Empty;
            middleName.Text = string.Empty;
            genderPicker.SelectedIndex = -1;
            birthDate.Date = DateTime.Today;
            dormitorySwitch.IsToggled = false;
            monitorSwitch.IsToggled = false;
            mathGrade.SelectedIndex = -1;
            russianGrade.SelectedIndex = -1;
        }

        private void loadButton_Clicked(object sender, EventArgs e)
        {
            if (storageMethodPicker.SelectedIndex == 0)
            {                
                LoadUsingPreferences();
            }
            else
            {                
                LoadUsingFile();
            }
        }
    }
}