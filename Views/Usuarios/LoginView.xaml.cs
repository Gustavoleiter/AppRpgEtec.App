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
                user.BackgroundColor = Color.FromRgb(0, 0, 0);
                user.TextColor = Color.FromRgb(255, 255, 255);
                cadastro.TextColor = Color.FromRgb(0, 0, 0);
                btnAutenticar.BackgroundColor = Color.FromRgb(0, 0, 0);// Mude a cor dos elementos Entry e Button também
                btnAutenticar.TextColor = Color.FromRgb(255, 255, 255);
                informacao.TextColor = Color.FromRgb(0, 0, 0);
                informacao.BackgroundColor = Color.FromRgb(255, 255, 255);
                senha.BackgroundColor = Color.FromRgb(0, 0, 0);
                mudarCor.BackgroundColor = Color.FromRgb(0, 0, 0);
                mudarCor.TextColor = Color.FromRgb(255, 255, 255);
            }
            else
            {
                BackgroundColor = Color.FromRgb(0, 0 , 0); // LightBlue (RGB values)
                user.TextColor = Color.FromRgb(0, 0, 0);
                user.BackgroundColor = Color.FromRgb(255, 255, 255);
                senha.BackgroundColor = Color.FromRgb(255, 255, 255);
                btnAutenticar.BackgroundColor = Color.FromRgb(255, 255, 255);                                                    // Mude a cor dos elementos Entry e Button também
                btnAutenticar.TextColor = Color.FromRgb(0, 0, 0);
                senha.BackgroundColor = Color.FromRgb(255, 255, 255);
                mudarCor.BackgroundColor = Color.FromRgb(255, 255, 255);
                mudarCor.TextColor = Color.FromRgb(0, 0, 0);
                informacao.TextColor = Color.FromRgb(255, 255, 255);
                informacao.BackgroundColor = Color.FromRgb(0, 0, 0);
                cadastro.TextColor = Color.FromRgb(255, 255, 255);


            }

            isColorChanged = !isColorChanged;
        }
    }
}
