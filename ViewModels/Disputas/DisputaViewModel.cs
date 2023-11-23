using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {
        private PersonagemService pService;

        public ObservableCollection<Personagem> PersonagemsEncontrados {  get; set; }

        public Personagem Atacante { get; set; }

        public Personagem Oponente { get; set; }

        public DisputaViewModel() 
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);

            Atacante = new Personagem();
            Oponente = new Personagem();

            PesquisarPersonagensCommand =
                new Command<string>(async (string pesquisa) => { await PesquisarPersonagens(pesquisa); });
        }

        public ICommand PesquisarPersonagensCommand { get; set; }

        public async Task PesquisarPersonagens(string textoPesquisaPersonagem)
        {

            try
            {
                PersonagemsEncontrados = await pService.GetPersonagensByNomeAsync(textoPesquisaPersonagem);
                OnPropertyChanged(nameof(PersonagemsEncontrados));
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }

        public string DescricaoPersonagemOponente
        { 
            get => Oponente.Nome;
        }

        private Personagem personagemSelecionado;
        
        public Personagem PersonagemSelecionado
        {
            set 
            {
                if(value != null)
                {
                    personagemSelecionado = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagemsEncontrados.Clear();
                }
            }
        }
        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombatente = await Application.Current.MainPage
                    .DisplayActionSheet("Atacante ou Oponente?", "Cancelar", "", "Atacante", "Oponente");

                if(tipoCombatente == "Atacante")
                {
                    Atacante = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemAtacante));
                }
                else if (tipoCombatente == "Oponente")
                {
                    Oponente = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));
                }
            }
            catch(Exception ex) 
            {
                await Application.Current.MainPage
                            .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
