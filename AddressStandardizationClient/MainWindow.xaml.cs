using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AddressStandardizationClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void sendButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var addressToStandardize = "123 Main St, City, Country"; // Замените на ваш сырой адрес

            var requestModel = new AddressRequestModel
            {
                RawAddress = addressToStandardize
            };

            var requestJson = JsonConvert.SerializeObject(requestModel);

            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // Замените URL на URL вашего веб-сервиса
            var apiUrl = "https://localhost:7227/api/address/standardize";

            try
            {
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var standardizedAddress = JsonConvert.DeserializeObject<AddressResponseModel>(responseContent);

                    MessageBox.Show("Стандартизированный адрес: " + standardizedAddress.StandardizedAddress);
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
    }
}
