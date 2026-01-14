namespace RMP_8
{
    public partial class EditPage : ContentPage
    {
        private string _selectedPhotoPath;

        public EditPage()
        {
            InitializeComponent();
            LoadCurrentData();
        }

        private void LoadCurrentData()
        {            
            lastName.Text = StudentData.LastName;
            firstName.Text = StudentData.FirstName;
            middleName.Text = StudentData.MiddleName;
            
            if (!string.IsNullOrEmpty(StudentData.Gender))
            {
                if (StudentData.Gender == "Мужской")
                    genderPicker.SelectedIndex = 0;
                else if (StudentData.Gender == "Женский")
                    genderPicker.SelectedIndex = 1;
            }

            birthDate.Date = StudentData.BirthDate;
            dormitorySwitch.IsToggled = StudentData.NeedsDormitory;
            monitorSwitch.IsToggled = StudentData.IsMonitor;
            
            if (!string.IsNullOrEmpty(StudentData.MathGrade))
            {
                switch (StudentData.MathGrade)
                {
                    case "2": mathGrade.SelectedIndex = 0; break;
                    case "3": mathGrade.SelectedIndex = 1; break;
                    case "4": mathGrade.SelectedIndex = 2; break;
                    case "5": mathGrade.SelectedIndex = 3; break;
                }
            }

            if (!string.IsNullOrEmpty(StudentData.RussianGrade))
            {
                switch (StudentData.RussianGrade)
                {
                    case "2": russianGrade.SelectedIndex = 0; break;
                    case "3": russianGrade.SelectedIndex = 1; break;
                    case "4": russianGrade.SelectedIndex = 2; break;
                    case "5": russianGrade.SelectedIndex = 3; break;
                }
            }
            
            _selectedPhotoPath = StudentData.PhotoPath;
            if (!string.IsNullOrEmpty(_selectedPhotoPath) && File.Exists(_selectedPhotoPath))
                studentPhoto.Source = ImageSource.FromFile(_selectedPhotoPath);
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
            
            StudentData.LastName = lastName.Text;
            StudentData.FirstName = firstName.Text;
            StudentData.MiddleName = middleName.Text;
            StudentData.Gender = genderPicker.SelectedItem?.ToString() ?? "";
            StudentData.BirthDate = birthDate.Date;
            StudentData.NeedsDormitory = dormitorySwitch.IsToggled;
            StudentData.IsMonitor = monitorSwitch.IsToggled;
            StudentData.MathGrade = mathGrade.SelectedItem?.ToString() ?? "";
            StudentData.RussianGrade = russianGrade.SelectedItem?.ToString() ?? "";
            StudentData.PhotoPath = _selectedPhotoPath ?? "";
            
            if (storageMethodPicker.SelectedIndex == 0)
            {
                SaveUsingPreferences();
            }
            else
            {
                SaveUsingFile();
            }

            string studentInfo = $"Данные сохранены!\n\n" +
                               $"Студент: {StudentData.LastName} {StudentData.FirstName}\n" +
                               $"Сохранено через: {(storageMethodPicker.SelectedIndex == 0 ? "Preferences" : "Файл")}";

            DisplayAlert("Успешно", studentInfo, "OK");
            
            Navigation.PopAsync();
        }

        private void SaveUsingPreferences()
        {
            Preferences.Default.Set("lastName", StudentData.LastName);
            Preferences.Default.Set("firstName", StudentData.FirstName);
            Preferences.Default.Set("middleName", StudentData.MiddleName);
            Preferences.Default.Set("birthDate", StudentData.BirthDate);

            if (!string.IsNullOrEmpty(StudentData.Gender))
            {
                Preferences.Default.Set("gender", StudentData.Gender);
            }

            Preferences.Default.Set("dormitory", StudentData.NeedsDormitory);
            Preferences.Default.Set("monitor", StudentData.IsMonitor);

            if (!string.IsNullOrEmpty(StudentData.MathGrade))
            {
                Preferences.Default.Set("mathGrade", StudentData.MathGrade);
            }

            if (!string.IsNullOrEmpty(StudentData.RussianGrade))
            {
                Preferences.Default.Set("russianGrade", StudentData.RussianGrade);
            }

            Preferences.Default.Set("photoPath", StudentData.PhotoPath);
        }

        private void SaveUsingFile()
        {
            try
            {
                string filePath = "/storage/emulated/0/Documents/student_data.txt";
                StreamWriter outFile = new StreamWriter(filePath);

                outFile.WriteLine(StudentData.LastName);
                outFile.WriteLine(StudentData.FirstName);
                outFile.WriteLine(StudentData.MiddleName);
                outFile.WriteLine(StudentData.Gender);
                outFile.WriteLine(StudentData.BirthDate.ToString("dd.MM.yyyy"));
                outFile.WriteLine(StudentData.NeedsDormitory);
                outFile.WriteLine(StudentData.IsMonitor);
                outFile.WriteLine(StudentData.MathGrade);
                outFile.WriteLine(StudentData.RussianGrade);
                outFile.Close();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", $"Не удалось сохранить файл: {ex.Message}", "OK");
            }
        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {            
            Navigation.PopAsync();
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
            studentPhoto.Source = ImageSource.FromFile("man.jpg");
            _selectedPhotoPath = "";
        }
    }
}