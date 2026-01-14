namespace RMP_8
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadStudentData();
        }

        private void LoadStudentData()
        {            
            if (!string.IsNullOrEmpty(StudentData.PhotoPath) && File.Exists(StudentData.PhotoPath))
            {
                studentPhoto.Source = ImageSource.FromFile(StudentData.PhotoPath);
            }
            else
            {
                studentPhoto.Source = "man.jpg";
            }
            
            if (!string.IsNullOrWhiteSpace(StudentData.LastName) &&
                !string.IsNullOrWhiteSpace(StudentData.FirstName))
            {
                fullNameLabel.Text = $"{StudentData.LastName} {StudentData.FirstName} {StudentData.MiddleName}";
            }
            else
            {
                fullNameLabel.Text = "Не указано";
            }

            genderLabel.Text = string.IsNullOrEmpty(StudentData.Gender) ? "Не указан" : StudentData.Gender;
            birthDateLabel.Text = StudentData.BirthDate.ToString("dd.MM.yyyy");
            dormitoryLabel.Text = StudentData.NeedsDormitory ? "Нужно" : "Не нужно";
            monitorLabel.Text = StudentData.IsMonitor ? "Да" : "Нет";
            mathGradeLabel.Text = string.IsNullOrEmpty(StudentData.MathGrade) ? "Не указана" : StudentData.MathGrade;
            russianGradeLabel.Text = string.IsNullOrEmpty(StudentData.RussianGrade) ? "Не указана" : StudentData.RussianGrade;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new EditPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {                       
            await Navigation.PushAsync(new LoginPage());            
            Navigation.RemovePage(this);
        }
    }
}