using Integrador.ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Contracts.Infraestructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiService<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="webAPIURL"></param>
        /// <param name="controlador"></param>
        /// <param name="metodo"></param>
        /// <param name="verboHTTP"></param>
        /// <returns></returns>
        T ConsumirServicio(string data, string webAPIURL, string controlador, string metodo, string verboHTTP);
       
        T ConsumirServicioCabeceraToken(string data, string token, string webAPIURL, string controlador, string metodo, string verboHTTP);

        T ConsumirServicioCabecera(string data, List<ParametroGenerico> cabecera, string webAPIURL, string controlador, string metodo, string verboHTTP);

    }
}
