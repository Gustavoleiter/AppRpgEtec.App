using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using Plugin.Media;
using System.Windows.Input;
using System.Threading.Tasks;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class ImagemUsuarioViewModel : BaseViewModel

    {

        public ImagemUsuarioViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuarioService(token);

            AbrirGaleriaCommand = new Command(AbrirGaleria);
            SalvarImagemCommand = new Command(SalvarImagem);
            FotografarCommand = new Command(Fotografar);

            CarregarUsuario();
        }

        private UsuarioService uService;

        public ICommand AbrirGaleriaCommand { get; }
        public ICommand SalvarImagemCommand { get; }
        public ICommand FotografarCommand { get; }



        private ImageSource fonteImagem;

        public ImageSource FonteImagem
        {
            get { return fonteImagem; }
            set
            {
                fonteImagem = value;
                OnPropertyChanged();

            }
        }

        private byte[] foto;

        public byte[] Foto
        {
            get => foto;
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }


        public async void Fotografar()
        {
            try
            {
                if(MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null) 
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            using (MemoryStream ms = new MemoryStream()) 
                            {
                            Foto = ms.ToArray();
                                FonteImagem = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async void SalvarImagem()
        {
            try
            {
                Usuario u = new Usuario();
                u.Foto = foto;
                u.Id = Preferences.Get("UsuarioId", 0);

                if (await uService.PutFotoUsuarioAsync(u) != 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else { throw new Exception("Erro ao tentar atualizar imagem"); }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                       .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async void AbrirGaleria()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                    if (photo != null)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                Foto = ms.ToArray();
                                FonteImagem = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async void CarregarUsuario()
        {
            try
            {
                int usuarioId = Preferences.Get("UsuarioId", 0);
                Usuario u = await uService.GetUsuarioAsync(usuarioId);

                Foto = u.Foto;
            }
            catch ( Exception ex)
            {
                await Application.Current.MainPage
                           .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

    }
}
