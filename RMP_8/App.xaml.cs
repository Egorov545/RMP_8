namespace RMP_8
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}