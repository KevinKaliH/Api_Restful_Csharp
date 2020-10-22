using SocialMediaCore.Entidades.QueryFilters;
using SocialMediaInfraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaInfraestructure.Services
{
    public class UriService : IUriService
    {
        private readonly string baseUrl;
        public UriService(string _baseUrl)
        {
            baseUrl = _baseUrl;
        }

        public Uri GetPostPagination(PostQueryFilters filters, string actionUrl)
        {
            string _baseUrl = $"{baseUrl}{actionUrl}";
            return new Uri(_baseUrl);
        }
    }
}
