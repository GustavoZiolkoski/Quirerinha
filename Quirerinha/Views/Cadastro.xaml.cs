using Quirerinha.Models;
using System.Globalization;

namespace Quirerinha.Views
{
    public partial class Cadastro : ContentPage
    {
        private CadastroElemento _elemento;
        public bool TipoPagina { get; set; }

        public Cadastro()
        {
            InitializeComponent();
            _elemento = new CadastroElemento();
            btnSalvar.Clicked += BtnSalvar_Clicked;
            TipoPagina = true;
        }

        public Cadastro(CadastroElemento emp)
        {
            InitializeComponent();
            btnSalvar.Clicked += BtnSalvar_Clicked;
            _elemento = emp;
            TipoPagina = false;

            dpDataCadastro.Date = DateTime.ParseExact(emp.Data, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            pckDespesas.SelectedItem = emp.Despesa;
            lbValor.Text = emp.Valor;
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            if (pckDespesas.SelectedItem == null || string.IsNullOrEmpty(lbValor.Text))
            {
                await DisplayAlert("Atenção", "Você não pode salvar seu cadastro, pois algum campo está vazio.", "OK");
                return;
            }

            double valorCadastro = double.Parse(lbValor.Text);
            Usuarios usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();

            if (usuario != null)
            {
                // Deduzir o valor do novo cadastro da remuneração
                usuario.Remuneracao -= valorCadastro;

                // Atualizar a remuneração do usuário no banco de dados
                await App.SQLiteDbUsuario.SaveItemAsync(usuario);
            }

            // Salvar o novo cadastro
            _elemento.Despesa = pckDespesas.SelectedItem.ToString();
            _elemento.Valor = lbValor.Text;
            _elemento.Data = dpDataCadastro.Date.ToString("dd-MM-yyyy");

            await App.SQLiteDbCadastroElemento.SaveItemAsync(_elemento);

            await DisplayAlert("Sucesso", "Dados salvos com sucesso.", "OK");
            await Navigation.PopAsync();
        }

    }
}
