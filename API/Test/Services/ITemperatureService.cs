using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Entities;

namespace Test.Services
{
    public interface ITemperatureService
    {
        void SaveData(List<TemperatureEntity> temperatures);
        Task<List<TemperatureEntity>> LoadData();
        List<TemperatureEntity> SortData(List<TemperatureEntity> customers);
    }
}