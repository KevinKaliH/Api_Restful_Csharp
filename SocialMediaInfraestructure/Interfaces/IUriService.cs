using SocialMediaCore.Entidades.QueryFilters;
using System;

namespace SocialMediaInfraestructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPagination(PostQueryFilters filters, string actionUrl);
    }
}