using Business.Models;

namespace Business.Interfaces
{
    public interface IJWTGeneratorService
    {
        string GenerateJWT(JWTBusinessModel model);
    }
}
