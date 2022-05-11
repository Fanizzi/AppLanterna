using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Plugin.Battery;
using Xamarin.Essentials;

namespace AppLanterna
{
    public partial class MainPage : ContentPage
    {
        bool lanterna_ligada = false;
        public MainPage()
        {
            InitializeComponent();

            btnOnOff.Source = ImageSource.FromResource("AppLanterna.Image.botao-desligado.png");

            Carrega_Informacoes_Bateria();
        }

        private async void Carrega_Informacoes_Bateria()
        {
            try
            {
                if (CrossBattery.IsSupported)
                {
                    CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                    CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;
                }
                else
                {
                    lbl_bateria_fraca.Text = "As Informações sobre a bateria não estão disponíveis.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro: \n ", ex.Message, "OK");
            }
        }

        private async void Mudanca_Status_Bateria(object sender, Plugin.Battery.Abstractions.BatteryChangedEventArgs e)
        {
            try
            {
                lbl_porcentagem_restante.Text = e.RemainingChargePercent.ToString() + "%";

                if (e.IsLow)
                {
                    lbl_bateria_fraca.Text = "Atenção! A Bateria está fraca!";
                }
                else
                {
                    lbl_bateria_fraca.Text = "";
                }
            }
            catch
        }

        private void btnOnOff_Clicked(object sender, EventArgs e)
        {

        }
    }
}
