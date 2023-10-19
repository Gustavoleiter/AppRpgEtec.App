using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class ImagemUsuarioView : ContentView
{
	ImagemUsuarioViewModel viewModel;

	public ImagemUsuarioView()
	{
		InitializeComponent();

		viewModel = new ImagemUsuarioViewModel();
		Title = "Imagem do Usuário";
		BindingContext = viewModel;
	}
}