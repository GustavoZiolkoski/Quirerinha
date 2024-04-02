using Quirerinha.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Quirerinha.Views;

public partial class Gestao : ContentPage
{
    public ObservableCollection<CadastroElemento> Aux { get; set; }
    public ICommand OnItemSwipedCommand { get; set; }
    public Gestao()
	{
		InitializeComponent();

        OnItemSwipedCommand = new Command(OnItemSwiped);

        LerElementosAsync();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        LerElementosAsync();
    }

    private async void LerElementosAsync()
    {
        Aux = new ObservableCollection<CadastroElemento>(await App.SQLiteDbCadastroElemento.GetItemsAsyncOrder());

        if (Aux != null)
        {
            myCollectionView.ItemsSource = Aux;
        }

        // definindo o ctx de dados da página para si mesma
        BindingContext = this;
    }

    private void OnItemSwiped(object parameter)
    {
        var item = parameter as CadastroElemento;
        if (item != null)
        {
            // Executar ação desejada para o deslize para a direita
            /*ExibirDetalhes(item);*/
        }
    }

    private async void Detalhar_Invoked(object sender, EventArgs e)
    {
        var item = sender as SwipeItem;
        var emp = item.CommandParameter as CadastroElemento;
        await DisplayAlert("Detalhes do Cadastro",
            $" ● Despesa: {emp.Despesa}\n\n" +
            $" ● Valor: R$ {emp.Valor} reais\n\n" +
            $" ● Data: {emp.Data}\n",
            "OK");
    }
    private async void Editar_Invoked(object sender, EventArgs e)
    {
        var item = sender as SwipeItem;
        var emp = item.CommandParameter as CadastroElemento;

        IdCadastroElemento.ValorIdCadastroElemento = emp.ID;
        Despesa.GlobalDespesa = emp.Despesa;
        Valor.GlobalValor = emp.Valor.ToString();
        Data.GlobalData = emp.Data;

        await Navigation.PushAsync(new Cadastro(emp));
    }



    private async void Remover_Invoked(object sender, EventArgs e)
    {
        var item = sender as SwipeItem;
        var emp = item.CommandParameter as CadastroElemento;
        var result = await DisplayAlert("EXCLUIR", $"VOCÊ TEM CERTEZA QUE DESEJA EXCLUIR O CADASTRO DE:\n\n" +
            $"● Despesa: {emp.Despesa}\n\n" +
            $" ● Valor: R$ {emp.Valor} reais ?",
            "SIM", "NÃO");

        if (result)
        {
            double valorExcluido = emp.Valor;

            Usuarios usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();
            usuario.Remuneracao += valorExcluido;

            await App.SQLiteDbUsuario.SaveItemAsync(usuario);

            await App.SQLiteDbCadastroElemento.DeleteItemAsync(emp);
            myCollectionView.ItemsSource = await App.SQLiteDbCadastroElemento.GetItemsAsyncOrder();
        }
    }
}