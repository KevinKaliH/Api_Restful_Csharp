using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practica1.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T _Data)
        {
            Data = _Data;
        }

        public T Data { get; set; }
    }
}
