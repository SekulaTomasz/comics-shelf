using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace comics_shelf_api.core.Dtos
{
    public class ApiResult<T> where T : class
    {
        public T Result { get; set; }
        public string Error { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
