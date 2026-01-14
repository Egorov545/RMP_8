namespace RMP_8
{
    public partial class LoginPage : ContentPage
    {
        private string enteredPassword = ""; 
        private string CurrentPassword = "123";

        public LoginPage()
        {
            InitializeComponent();            
        }

        private void OnDigitClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string digit)
            {                                
                enteredPassword += digit;
                UpdatePasswordDisplay();                
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (enteredPassword.Length > 0)
            {
                enteredPassword = enteredPassword[..^1];
                UpdatePasswordDisplay();
            }
        }

        private async void UpdatePasswordDisplay()
        {
            passwordDisplay.Text = new string('●', enteredPassword.Length);            

            if (enteredPassword.Length == 3)
            {
                if (enteredPassword == CurrentPassword)
                {
                    var mainPage = new MainPage();
                    await Navigation.PushAsync(mainPage);
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Неверный пароль", "OK");
                    enteredPassword = "";
                    UpdatePasswordDisplay();
                }
            }
        }
    }
}