using Microsoft.Maui.Controls;
using AppRpgEtec.ViewModels.Usuarios;
using Microsoft.Maui.Graphics.Text;

namespace AppRpgEtec.Views.Usuarios
{
    public partial class LoginView : ContentPage
    {
        UsuarioViewModel usuarioViewModel;
        private bool isColorChanged;

        public LoginView()
        {
            InitializeComponent();
            usuarioViewModel = new UsuarioViewModel();
            BindingContext = usuarioViewModel;
        }

        private void ChangeColorButton_Clicked(object sender, EventArgs e)
        {
            if (isColorChanged)
            {
                BackgroundColor = Color.FromRgb(255, 255, 255); // Cor padrão
                cadastro.BackgroundColor = Color.FromRgb(0, 0, 0); // Cor do BoxView (se estiver usando)
                btnAutenticar.BackgroundColor = Color.FromRgb(0, 0, 0);                            // Mude a cor dos elementos Entry e Button também
                informacao.BackgroundColor =Color.FromRgb(0, 0 , 0); 
                senha.BackgroundColor = Color.FromRgb(0, 0, 0);
                mudarCor.BackgroundColor = Color.FromRgb(0, 0, 0);
            }
            else
            {
                BackgroundColor = Color.FromRgb(0, 0 , 0); // LightBlue (RGB values)
                cadastro.BackgroundColor = Color.FromRgb(255, 255, 255); // Cor do BoxView (se estiver usando)
                btnAutenticar.BackgroundColor = Color.FromRgb(255, 255, 255);                                                    // Mude a cor dos elementos Entry e Button também
                informacao.BackgroundColor = Color.FromRgb(255, 255, 255);
                senha.BackgroundColor = Color.FromRgb(255, 255, 255);
                mudarCor.BackgroundColor = Color.FromRgb(255, 255, 255);
            }

            isColorChanged = !isColorChanged;
        }
    }
}
