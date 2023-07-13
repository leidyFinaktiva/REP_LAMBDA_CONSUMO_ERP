using Integrador.ERP.Application.Contracts.Infraestructure;
using MediatR;
using Integrador.ERP.Application.Models;

namespace Integrador.ERP.Domain.Models
{
    public class RequestObject<T>: IRequest<ResponseVm> where T : class
    {
        public T Object { get; set; }
        public string ScriptId { get; set; }
        public string Method { get; set; }
        public string URL { get; set; }
        public string Data { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsGet { get; set; }
        public string IdDocumentType { get; set; }
        public string documentNumber { get; set; }

        public RequestObject(T data, string scriptId)
        {
            Object = data;
            ScriptId = scriptId;
        }
    }
}
