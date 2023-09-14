using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
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
            var client = new HttpClient();
            var addressToStandardize = "мск сухонска 11/-89"; // Замените на ваш сырой адрес

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
                    var deserializedObject = JsonConvert.DeserializeObject<AddressResponseModel>(responseContent);
                    var adresses = JsonConvert.DeserializeObject<List<Root>>(deserializedObject.StandardizedAddress);

                    var originalData = adresses; // Ваши исходные данные
                    var transposedData = new List<TransposedData>();

                    foreach (var row in originalData)
                    {
                        foreach (var property in row.GetType().GetProperties())
                        {
                            transposedData.Add(new TransposedData
                            {
                                ColumnName = property.Name, // Имя вашего столбца
                                Value = property.GetValue(row)?.ToString() // Значение ячейки
                            });
                        }
                    }
                    responseGrid.ItemsSource = transposedData;
                }
                else
                {
                    throw new Exception("Ошибка: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка: " + ex.Message);
            }
        }
    }
}
