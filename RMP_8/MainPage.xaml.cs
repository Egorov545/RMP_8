using Microsoft.Maui.Controls;

namespace RMP_8
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void saveButton_Clicked(object sender, EventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(lastName.Text) ||
                string.IsNullOrWhiteSpace(firstName.Text))
            {
                DisplayAlert("Ошибка", "Заполните обязательные поля: Фамилия и Имя", "OK");
                return;
            }
            
            string studentInfo = $"Студент: {lastName.Text} {firstName.Text} {middleName.Text}\n" +
                               $"Дата рождения: {birthDate.Date:dd.MM.yyyy}\n" +
                               $"Пол: {genderPicker.SelectedItem ?? "не указан"}\n" +
                               $"Общежитие: {(dormitorySwitch.IsToggled ? "Нужно" : "Не нужно")}\n" +
                               $"Староста: {(monitorSwitch.IsToggled ? "Да" : "Нет")}\n" +
                               $"Оценки - Математика: {mathGrade.SelectedItem ?? "не указана"}, " +
                               $"Русский: {russianGrade.SelectedItem ?? "не указана"}";

            DisplayAlert("Данные сохранены", studentInfo, "OK");
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
    }
}