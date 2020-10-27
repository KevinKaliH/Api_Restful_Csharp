using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork unitOfWork;

        public SecurityService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);
        }

        public async Task RegisterUser(Security security)
        {
            await unitOfWork.SecurityRepository.Add(security);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
