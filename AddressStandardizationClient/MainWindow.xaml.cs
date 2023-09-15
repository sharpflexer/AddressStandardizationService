using AddressStandardizationClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

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
            string fullAddressUrl = "https://localhost:7227/api/address/full";
            await GetRequest<Address>(fullAddressUrl);
        }
        private async void getShortAddressButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string shortAddressUrl = "https://localhost:7227/api/address/short";
            await GetRequest<ShortAddress>(shortAddressUrl);
        }
        private async void getGeodataButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string geodataAddressUrl = "https://localhost:7227/api/address/geodata";
            await GetRequest<GeoData>(geodataAddressUrl);
        }


        private async Task GetRequest<T>(string url)
        {
            var client = new HttpClient();
            var requestJson = JsonConvert.SerializeObject(requestBox.Text);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(url, content);
                if (CheckResponseStatus(response))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var adresses = JsonConvert.DeserializeObject<List<T>>(responseContent);
                    TransposeGrid(adresses);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private bool CheckResponseStatus(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception("Ошибка: " + response.StatusCode);
            }
        }

        private void TransposeGrid<T>(List<T>? adresses)
        {
            var originalData = adresses;
            var transposedData = new List<TransposedData>();

            foreach (var row in originalData)
            {
                foreach (var property in row.GetType().GetProperties())
                {
                    transposedData.Add(new TransposedData
                    {
                        ColumnName = property.Name, // Имя столбца
                        Value = property.GetValue(row)?.ToString() // Значение ячейки
                    });
                }
            }
            responseGrid.ItemsSource = transposedData;
        }
    }
}
