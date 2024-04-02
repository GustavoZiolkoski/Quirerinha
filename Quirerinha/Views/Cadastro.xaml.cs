using Quirerinha.Models;
using System.Globalization;

namespace Quirerinha.Views
{
    public partial class Cadastro : ContentPage
    {
        private CadastroElemento _elemento;
        public bool TipoPagina { get; set; }
        public double valorAntigo { get; set; }

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
            lbValor.Text = emp.Valor.ToString();
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

            if (TipoPagina == true) 
            { 
                if (usuario != null)
                {
                    usuario.Remuneracao -= valorCadastro;

                    await App.SQLiteDbUsuario.SaveItemAsync(usuario);
                }
            }
            if (TipoPagina == false) 
            {
                if (double.TryParse(Valor.GlobalValor, out double valorConvertido))
                {
                    valorAntigo = valorConvertido;
                    usuario.Remuneracao += valorAntigo - valorCadastro;
                    await App.SQLiteDbUsuario.SaveItemAsync(usuario);
                }
                else
                {
                    Console.WriteLine("Erro ao converter valor para double.");
                }

            }
            // Salvar o novo cadastro
            _elemento.Despesa = pckDespesas.SelectedItem.ToString();
            _elemento.Valor = double.Parse(lbValor.Text);
            _elemento.Data = dpDataCadastro.Date.ToString("dd-MM-yyyy");

            await App.SQLiteDbCadastroElemento.SaveItemAsync(_elemento);

            await DisplayAlert("Sucesso", "Dados salvos com sucesso.", "OK");
            await Navigation.PopAsync();
        }

    }
}
