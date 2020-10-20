using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}