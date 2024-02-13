using Crud_Sqlite.Service;
using Crud_Sqlite.View;
using System.IO;
using Xamarin.Forms;

namespace Crud_Sqlite
{
    public partial class App : Application
    {
        internal static ClienteService ClienteService { get; private set; }

        public App()
        {
            InitializeComponent();
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "deberUno.db3");
            ClienteService = new ClienteService(dbPath);
            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
