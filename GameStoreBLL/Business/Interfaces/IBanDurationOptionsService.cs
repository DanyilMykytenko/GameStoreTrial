using Business.Models;

namespace Business.Interfaces
{
    public interface IBanDurationOptionsService
    {
        IEnumerable<BanDurationOptionsModel> GetAll();
    }
}
