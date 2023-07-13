using Integrador.ERP.Application.Contracts.Infraestructure;
using Integrador.ERP.Domain.Entities;
using Integrador.ERP.Infraestructure.Client;
using Integrador.ERP.Application.Contracts.Infraestructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Infraestructure.API
{
    public class ApiService<T> : IApiService<T> where T : class
    {
        #region "Implementacion Interface"
        public T ConsumirServicio(string data, string webAPIURL, string controlador, string metodo, string verboHTTP)
        {
            try
            {
                using (WebClient context = WebClientProxy.GetWebClient())
                {
                    string url = String.Format("{0}{1}{2}", webAPIURL, controlador, metodo);
                    var m_Response = JsonConvert.DeserializeObject<T>(context.UploadString(url, verboHTTP, data));
                    return m_Response;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("{0}{1} ERROR {2}", "ConsumirServicio ", metodo, ex.Message));
            }
        }

        public T ConsumirServicioCabecera(string data, List<ParametroGenerico> cabecera, string webAPIURL, string controlador, string metodo, string verboHTTP)
        {
            try
            {
                using (WebClient context = this.GetWebClient(cabecera))
                {
                    string url = String.Format("{0}{1}{2}", webAPIURL, controlador, metodo);
                    return JsonConvert.DeserializeObject<T>(context.UploadString(url, verboHTTP, data));
                }

            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("{0}{1} ERROR {2}", "ConsumirServicio ", metodo, ex.Message));
            }
        }

        public T ConsumirServicioCabeceraToken(string data, string token, string webAPIURL, string controlador, string metodo, string verboHTTP)
        {
           try
            {
                List<ParametroGenerico> m_Cabecera = null;
                if (!string.IsNullOrEmpty(token))
                {
                    m_Cabecera = new List<ParametroGenerico> { new ParametroGenerico { Nombre = "Authorization", Valor = token } };
                }
                return ConsumirServicioCabecera(data, m_Cabecera, webAPIURL, controlador, metodo, verboHTTP);
            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("{0}{1} ERROR {2}", "ConsumirServicio ", metodo, ex.Message));
            }
        }

        #endregion
        #region "Metodos Privados"

        private WebClient GetWebClient(List<ParametroGenerico> cabeceras)
        {
            string m_MediaTypeJson = "Application/JSON; charset=utf-8";
            WebClient m_WebClient = new WebClient();
            m_WebClient.Headers.Add("Content-Type", m_MediaTypeJson);
            m_WebClient.Headers.Add("Accept-Type", m_MediaTypeJson);
            if (cabeceras != null && cabeceras.Count > 0)
            {
                foreach (ParametroGenerico m_Cabecera in cabeceras)
                {
                    m_WebClient.Headers.Add(m_Cabecera.Nombre, m_Cabecera.Valor);
                }
            }
            m_WebClient.Encoding = System.Text.Encoding.UTF8;
            return m_WebClient;
        }

        private HttpClient GetHttpClient(List<ParametroGenerico> cabeceras)
        {
            string m_MediaTypeJson = "Application/JSON; charset=utf-8";
            HttpClient m_WebClient = new HttpClient();
            m_WebClient.DefaultRequestHeaders.Add("Content-Type", m_MediaTypeJson);
            m_WebClient.DefaultRequestHeaders.Add("Accept-Type", m_MediaTypeJson);

            if (cabeceras != null && cabeceras.Count > 0)
            {
                foreach (ParametroGenerico m_Cabecera in cabeceras)
                {
                    m_WebClient.DefaultRequestHeaders.Add(m_Cabecera.Nombre, m_Cabecera.Valor);
                }
            }
           // m_WebClient.Encoding = System.Text.Encoding.UTF8;
            return m_WebClient;
        }
        #endregion
    }
}
