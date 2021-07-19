using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Test.Entities;

namespace Test.Services
{
    public class TemperatureService : ITemperatureService
    {
        string StrPath = "";
        public TemperatureService()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StrPath = Path.GetDirectoryName(asm.Location) + "\\temperature.json";
        }

        /// <summary>
        /// Load data from url
        /// </summary>
        /// <returns></returns>
        public async Task<List<TemperatureEntity>> LoadData()
        {
            List<TemperatureEntity> existingData = new List<TemperatureEntity>();
            if (File.Exists(StrPath))
            {
                var read = File.ReadAllText(StrPath);
                existingData = JsonConvert.DeserializeObject<List<TemperatureEntity>>(read);
            }

            using (HttpClient client = new HttpClient())
            {
                var url = "https://api.parin.io/sensor/4221";
                client.DefaultRequestHeaders.Add("x-api-key", "w3HkNadqoJ2KKtM1hgOn76RhGfwdi2dH1E2FW1sZ");
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var temperatures = SortData(JsonConvert.DeserializeObject<List<TemperatureEntity>>(result));
                temperatures?.ForEach(temp =>
                {
                    if (!existingData.Exists(d => d.TimeStamp == temp.TimeStamp))
                    {
                        existingData.Add(temp);
                    }
                });
                
                return existingData;
            }
        }

        /// <summary>
        /// Save data in file
        /// </summary>
        /// <param name="temperatures"></param>
        public void SaveData(List<TemperatureEntity> temperatures)
        {
            var json = JsonConvert.SerializeObject(temperatures);
            File.WriteAllText(StrPath, json);
        }

        /// <summary>
        /// Sort data on asc order
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        public List<TemperatureEntity> SortData(List<TemperatureEntity> temperatures)
        {
            return temperatures?.Where(x => x.Temperature > 84.3).OrderBy(x => x.TimeStamp).ToList();
        }
    }
}