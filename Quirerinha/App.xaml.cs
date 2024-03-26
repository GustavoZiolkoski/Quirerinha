using Quirerinha.Views;

namespace Quirerinha
{
    public partial class App : Application
    {
        public static Models.SQLiteUsuario dbUsuario;
        public App()
        {
            InitializeComponent();

            MainPage = new Login();
        }

        public static Models.SQLiteUsuario SQLiteDbUsuario
        {
            get
            {
                if (dbUsuario == null)
                {
                    dbUsuario = new Models.SQLiteUsuario(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "usuario.db3"));
                }
                return dbUsuario;
            }
        }
    }
}
