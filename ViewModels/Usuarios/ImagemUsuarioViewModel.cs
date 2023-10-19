﻿using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        }

        private UsuarioService uService;

        public ICommand AbrirGaleriaCommand { get; }
        public ICommand SalvarImagemCommand { get; }
        public ICommand FotografarCommand { get; }


        public ImagemUsuarioViewModel()
        {
            string token= Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuarioService(token);
        }

        private ImageSource fonteImagem;

        public ImageSource FonteImagem
        {
            get { return fonteImagem;}
            set
            { fonteImagem = value;
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
                await CrossMedia.Current.Initialize();

                if (CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Sem Câmera", "A câmera não está disponível.", "Ok");
                    await Task.FromResult(false);
                }

                string fileName = String.Format("{0:ddMMyyy_HHmmss}", DateTime.Now) + ".jpg";

                var file = await CrossMedia.Current.TakePhotoAsync
                    (new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Fotos",
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        Name = fileName
                    });

                if (file != null) 
                {
                    await Task.FromResult(false);

                    MemoryStream ms = null;
                    using (ms = new MemoryStream()) 
                    {
                     var stream = file.GetStream();
                     stream.CopyTo(ms); 
                    }
                    FonteImagem = ImageSource.FromStream(() => file.GetStream());
                    Foto = ms.ToArray();
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
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Galeria não suportada",
                        "Não existe permissão para acessar a galeria.", "Ok");
                    return;

                }
                var file = await

            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage
                           .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        
    }
}