using SocialMediaCore.Entidades.CustomEntities;

namespace Practica1.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T _Data)
        {
            Data = _Data;
        }

        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
