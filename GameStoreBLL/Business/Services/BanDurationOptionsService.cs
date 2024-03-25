using Business.Interfaces;
using Business.Models;

namespace Business.Services
{
    public class BanDurationOptionsService : IBanDurationOptionsService
    {
        public IEnumerable<BanDurationOptionsModel> GetAll()
        {
            var options = new List<BanDurationOptionsModel>()
            {
                new BanDurationOptionsModel
                {
                    Option = "1 hour"
                },
                new BanDurationOptionsModel
                {
                    Option = "1 day"
                },
                new BanDurationOptionsModel
                { 
                    Option = "1 week" 
                },
                new BanDurationOptionsModel
                {
                    Option = "1 month"
                },
                new BanDurationOptionsModel
                {
                    Option = "Permanently"
                }
            };
            return options.AsEnumerable();
        }
    }
}
