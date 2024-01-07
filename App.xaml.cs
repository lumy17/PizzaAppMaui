using PizzaAppMaui.Data;

namespace PizzaAppMaui
{
    public partial class App : Application
    {
        static PizzaAppMauiDatabase database;
        public static PizzaAppMauiDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "PizzaAppMaui.db3");
                    database = new PizzaAppMauiDatabase(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}