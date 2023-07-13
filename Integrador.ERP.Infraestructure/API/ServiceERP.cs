using Integrador.ERP.Application.Contracts.Infraestructure;
using Integrador.ERP.Application.Contracts.Persistence;
using Integrador.ERP.Application.Models;
using Integrador.ERP.Domain.Entities;
using Integrador.ERP.Infraestructure.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OAuth;
//using OAuth;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Integrador.ERP.Infraestructure.API
{
    public class ServiceERP<T> : ApiService<T>, IServiceERP<T> where T : class
    {
        public TokenSettings _tokenSettings { get; set; }
        private readonly string _postMethod;
        private readonly string _getMethod;
        private readonly string _putMethod;
        private readonly IAsyncRepository<ResponseApi> _repository;

        public ServiceERP(IOptions<TokenSettings> tokenSettings, IConfiguration configuration, IAsyncRepository<ResponseApi> repository) {
            _tokenSettings = tokenSettings.Value;
            _postMethod = configuration.GetSection("postMethod").Value;
            _getMethod = configuration.GetSection("getMethod").Value;
            _putMethod = configuration.GetSection("putMethod").Value;
            _repository = repository;
        }

        public async Task AddResponseApi(ResponseVm response, string data, int FK_endpoint)
        {
            ResponseApi responseApiEntity = new ResponseApi();
            responseApiEntity.Code = response.code;
            responseApiEntity.RequestJson = data;
            responseApiEntity.FK_Endpoint = FK_endpoint;
            responseApiEntity.Message = (response.errors != null) ? response.errors.ToString() : string.Empty;
            responseApiEntity.RequestDate = DateTime.Now;
            responseApiEntity.IdResponse = response.id;
            await _repository.AddAsync(responseApiEntity);
        }

        public T ConsumirServicioERP(RequestIntegrador _request) 
        {
            RestResponse responseApi = new RestResponse();
            try
            {
                var response = DatosCabecera(_request.Url);
                var oAuth1 = OAuth1Authenticator.ForAccessToken(
                                consumerKey: _tokenSettings.customerKey,
                                consumerSecret: _tokenSettings.secret,
                                token: _tokenSettings.accessToken,
                                tokenSecret: _tokenSettings.token,
                                RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha256);
                oAuth1.Realm = _tokenSettings.realm; // if Realm has otherwise ignore
                oAuth1.Version = _tokenSettings.version;
                oAuth1.Type = OAuthType.AccessToken;

                var options = new RestClientOptions(_request.Url)
                {
                    ThrowOnAnyError = true,
                    Authenticator = oAuth1
                };
                var client = new RestClient(options);

                var request = new RestRequest(_request.Url, _request.MethodRest);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("url", _request.Url);
                request.AddHeader("Cookie", "NS_ROUTING_VERSION=LAGGING");
                request.AddParameter("Authorization", response[0].Valor, ParameterType.HttpHeader);
                request.Method = _request.MethodRest;
                if(request.Method != Method.Get)
                {
                    request.AddParameter("application/json", _request.Data, ParameterType.RequestBody);
                }
                
                responseApi = client.Execute(request);
                ResponseTrazaApi responseTraza = new ResponseTrazaApi();
                responseTraza.code = "200";
                responseTraza.detail = responseApi.Content;
                responseTraza.errors = responseApi.Content;
                responseTraza.objectData = _request.Data;
                responseTraza.endpoint = _request.Endpoint;
                responseTraza.nombreapiconsumida = _request.NombreApiConsumida;
                responseTraza.data = new List<data>();
                var resultTraza = JsonConvert.SerializeObject(responseTraza);
                var responseServiceTraza = ConsumingTrazaERP("https://3yqe0dc4df.execute-api.us-east-1.amazonaws.com/PROD/api/Trazabilidad/AddTrazabilidadERP/endpoint", Method.Post, resultTraza);
                if (_request.MethodRest == Method.Get)
                {
                    ResponseERP responseERP = new ResponseERP();

                    var unescaped = JsonConvert.DeserializeObject<string>(responseApi.Content);
                    var defOBJ = JsonConvert.DeserializeObject<ResponseERP2>(unescaped);
                    responseERP.code = defOBJ.code;
                    responseERP.data = new List<dataERP>();
                    if (defOBJ != null && defOBJ.data != null)
                    {
                        responseERP.data.AddRange(defOBJ.data);
                    }
                    object responseObject = responseERP as object;
                    return (T)responseObject;
                }
                return JsonConvert.DeserializeObject<T>(responseApi.Content.ToString());
            }
            catch (Exception ex)
            {
                ResponseTrazaApi responseTraza = new ResponseTrazaApi();
                responseTraza.code = "500";
                var mensaje = ex.Message;
                mensaje = string.IsNullOrEmpty(responseApi.Content) ? ex.StackTrace : responseApi.Content;
                responseTraza.detail = mensaje;
                responseTraza.objectData = _request.Data;
                responseTraza.endpoint = _request.Endpoint;
                responseTraza.errors = ex.Message;
                responseTraza.data = new List<data>();
                responseTraza.nombreapiconsumida = _request.NombreApiConsumida;
                var resultTraza = JsonConvert.SerializeObject(responseTraza);
                var responseServiceTraza = ConsumingTrazaERP("https://3yqe0dc4df.execute-api.us-east-1.amazonaws.com/PROD/api/Trazabilidad/AddTrazabilidadERP/endpoint", Method.Post, resultTraza);
                throw new Exception(String.Format("{0}{1} ERROR {2}", "ConsumirServicioERP ", _request.Url, ex.Message));
            }
        }

        private List<ParametroGenerico> DatosCabecera(string webAPIURL) {
            StringBuilder m_Cabecera = new StringBuilder();
            string SimpleQuote(string s) => '"' + s + '"';
            List<ParametroGenerico> parametroGenericos = new List<ParametroGenerico>();
            var timeStamp = OAuthTools.GetTimestamp();
            var nonce = Convert.ToBase64String(Encoding.UTF8.GetBytes(timeStamp));
            var signatureBaseString = "POST" + "&";
            signatureBaseString = signatureBaseString + webAPIURL.ToLower() + "&";
            signatureBaseString = signatureBaseString + "oauth_consumer_key=" + _tokenSettings.customerKey + "&";
            signatureBaseString = signatureBaseString + "oauth_nonce=" + nonce + "&";
            signatureBaseString = signatureBaseString + "oauth_signature_method=" + "HMAC-SHA256" + "&";
            signatureBaseString = signatureBaseString + "oauth_timestamp=" + timeStamp + "&";
            signatureBaseString = signatureBaseString + "oauth_token=" + _tokenSettings.token + "&";
            signatureBaseString = signatureBaseString + "oauth_version=" + "1.0";
            var signatureEncoding = new ASCIIEncoding();
            var keyBytes = signatureEncoding.GetBytes("oauth_token=" + _tokenSettings.accessToken + "&oauth_token_secret=" + _tokenSettings.token);
            var signatureBaseBytes = signatureEncoding.GetBytes(signatureBaseString);
            string signatureString;

            using (var hmacsha1 = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmacsha1.ComputeHash(signatureBaseBytes);
                signatureString = Convert.ToBase64String(hashBytes);
            }

            var signatureBase = CreateToken("oauth_token=" + _tokenSettings.accessToken + "&oauth_token_secret=" + _tokenSettings.token, _tokenSettings.secret);
            var signature = HttpUtility.UrlEncode(signatureBase);

            m_Cabecera.Append($"OAuth realm = {_tokenSettings.realm}, oauth_consumer_key = {SimpleQuote(_tokenSettings.customerKey)}, " +
            $"oauth_token = {SimpleQuote(_tokenSettings.accessToken)}, oauth_signature_method = {SimpleQuote(_tokenSettings.signature)}," +
            $"oauth_signature = {SimpleQuote(signature)}," +
            $"oauth_timestamp= {timeStamp}, oauth_nonce={SimpleQuote(nonce)}, oauth_version= {SimpleQuote(_tokenSettings.version)}");

            parametroGenericos.Add(new ParametroGenerico
            {
                Nombre = "Authorization",
                Valor = m_Cabecera.ToString()
            });
            return parametroGenericos;
        }

        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        private ResponseVm ConsumingTrazaERP(string url, Method method, string data)
        {
            var client = new RestClient();

            var request = new RestRequest(url, method);
            request.Method = method;
            if (method != Method.Get)
            {
                request.AddJsonBody(data);
            }
            var responseApi = client.Execute(request);
            var response = JsonConvert.DeserializeObject<ResponseVm>(responseApi.Content.ToString());
            return response;
        }
    }
}
