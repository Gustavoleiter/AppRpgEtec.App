using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Personagens;

public partial class CadastroPersonagemView : ContentPage
{
	private CadastroPersonagemViewModel cadViewMoedel;
	public CadastroPersonagemView()
	{
		InitializeComponent();

		cadViewMoedel = new CadastroPersonagemViewModel();
		BindingContext = cadViewMoedel;
		Title = "Novo Personagem";
	}
}