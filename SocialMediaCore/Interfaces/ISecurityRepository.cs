using SocialMediaCore.Entidades;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface ISecurityRepository: IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}