using Quirerinha.Views;

namespace Quirerinha
{
    public partial class App : Application
    {
        public static Models.SQLiteUsuario dbUsuario;
        public static Models.SQLiteCadastroElemento dbCadastroElemento;
        public App()
        {
            InitializeComponent();

            MainPage = new Login();
            // Tema claro app
            Application.Current.UserAppTheme = AppTheme.Light;
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
        public static Models.SQLiteCadastroElemento SQLiteDbCadastroElemento
        {
            get
            {
                if (dbCadastroElemento == null)
                {
                    dbCadastroElemento = new Models.SQLiteCadastroElemento(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CadastroElemento.db3"));
                }
                return dbCadastroElemento;
            }
        }
    }
}
