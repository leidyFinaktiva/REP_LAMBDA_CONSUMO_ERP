using AWSLambda1.Domain.Entities;
using AWSLambdaConsumoERP.Application.Resources;
using AWSLambdaConsumoERP.Domain.Entities;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using Newtonsoft.Json;
using OAuth;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AWSLambda1.Application
{
    public class ServiceERP
    {
        private readonly IAsyncRepository<TrazaConsumoERP> _trazaConsumoERP;
        public ServiceERP(IAsyncRepository<TrazaConsumoERP> trazaConsumoERP)
        {
            _trazaConsumoERP = trazaConsumoERP;
        }

        public async Task<RestResponse> ConsumirServicioERP(string url, string data, Method method, string nombreApi)
        {
            Console.WriteLine($"URL. {url} DATA {data} METHOD {method}");
            RestResponse restResponse = new RestResponse();
            try
            {
                List<ParametroGenerico> list = DatosCabecera(url);
                OAuth1Authenticator oAuth1Authenticator = OAuth1Authenticator.ForAccessToken(Constantes.CUSTOMER_KEY, Constantes.SECRET, Constantes.ACCESS_TOKEN, Constantes.TOKEN, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha256);
                oAuth1Authenticator.Realm = Constantes.REALM;
                oAuth1Authenticator.Version = Constantes.VERSION;
                oAuth1Authenticator.Type = OAuthType.AccessToken;
                RestClientOptions options = new RestClientOptions(url)
                {
                    ThrowOnAnyError = true,
                    Authenticator = oAuth1Authenticator
                };
                RestClient client = new RestClient(options);
                RestRequest restRequest = new RestRequest(url, method);
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("url", url);
                restRequest.AddHeader("Cookie", "NS_ROUTING_VERSION=LAGGING");
                restRequest.AddParameter("Authorization", list[0].Valor, ParameterType.HttpHeader);
                restRequest.Method = method;
                if (restRequest.Method != 0)
                {
                    restRequest.AddParameter("application/json", data, ParameterType.RequestBody);
                }

                restResponse = client.Execute(restRequest);
                Console.WriteLine($"RESPONSE CONTENT. {restResponse.Content} CODE {restResponse.StatusCode} ISSUCCESSFULL {restResponse.IsSuccessful}");
                TrazaConsumoERP traza = new TrazaConsumoERP();
                traza.Url = url;
                traza.FechaRequest = DateTime.Now;
                traza.NombreApi = nombreApi;
                traza.RequestBody = data;
                traza.RequestResponse = restResponse.Content;
                await _trazaConsumoERP.AddAsync(traza);
                return restResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"URL {url} RESPONSE CONTENT. {restResponse.Content} CODE {restResponse.StatusCode} ISSUCCESSFULL {restResponse.IsSuccessful} MESSAGE_ERROR {ex.Message} STACKTRACE {ex.StackTrace}");
                TrazaConsumoERP traza = new TrazaConsumoERP();
                traza.Url = url;
                traza.FechaRequest = DateTime.Now;
                traza.NombreApi = nombreApi;
                traza.RequestBody = data;
                traza.RequestResponse = restResponse.Content;
                await _trazaConsumoERP.AddAsync(traza);
                throw new Exception(string.Format("{0}{1} ERROR {2}", "ConsumirServicioERP ", url, ex.Message));
            }
        }

        private List<ParametroGenerico> DatosCabecera(string webAPIURL)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<ParametroGenerico> list = new List<ParametroGenerico>();
            string timestamp = OAuthTools.GetTimestamp();
            string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(timestamp));
            string text2 = "POST&";
            text2 = text2 + webAPIURL.ToLower() + "&";
            text2 = text2 + "oauth_consumer_key=" + Constantes.CUSTOMER_KEY + "&";
            text2 = text2 + "oauth_nonce=" + text + "&";
            text2 += "oauth_signature_method=HMAC-SHA256&";
            text2 = text2 + "oauth_timestamp=" + timestamp + "&";
            text2 = text2 + "oauth_token=" + Constantes.TOKEN + "&";
            text2 += "oauth_version=1.0";
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = aSCIIEncoding.GetBytes("oauth_token=" + Constantes.ACCESS_TOKEN + "&oauth_token_secret=" + Constantes.TOKEN);
            byte[] bytes2 = aSCIIEncoding.GetBytes(text2);
            using (HMACSHA256 hMACSHA = new HMACSHA256(bytes))
            {
                byte[] inArray = hMACSHA.ComputeHash(bytes2);
                string text3 = Convert.ToBase64String(inArray);
            }

            string str = CreateToken("oauth_token=" + Constantes.ACCESS_TOKEN + "&oauth_token_secret=" + Constantes.TOKEN, Constantes.SECRET);
            string s2 = HttpUtility.UrlEncode(str);
            StringBuilder stringBuilder2 = stringBuilder;
            StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(148, 8, stringBuilder2);
            handler.AppendLiteral("OAuth realm = ");
            handler.AppendFormatted(Constantes.REALM);
            handler.AppendLiteral(", oauth_consumer_key = ");
            handler.AppendFormatted(SimpleQuote(Constantes.CUSTOMER_KEY));
            handler.AppendLiteral(", ");
            handler.AppendLiteral("oauth_token = ");
            handler.AppendFormatted(SimpleQuote(Constantes.ACCESS_TOKEN));
            handler.AppendLiteral(", oauth_signature_method = ");
            handler.AppendFormatted(SimpleQuote(Constantes.SIGNATURE));
            handler.AppendLiteral(",");
            handler.AppendLiteral("oauth_signature = ");
            handler.AppendFormatted(SimpleQuote(s2));
            handler.AppendLiteral(",");
            handler.AppendLiteral("oauth_timestamp= ");
            handler.AppendFormatted(timestamp);
            handler.AppendLiteral(", oauth_nonce=");
            handler.AppendFormatted(SimpleQuote(text));
            handler.AppendLiteral(", oauth_version= ");
            handler.AppendFormatted(SimpleQuote(Constantes.VERSION));
            stringBuilder2.Append(ref handler);
            list.Add(new ParametroGenerico
            {
                Nombre = "Authorization",
                Valor = stringBuilder.ToString()
            });
            return list;
            static string SimpleQuote(string s)
            {
                return "\"" + s + "\"";
            }
        }

        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = aSCIIEncoding.GetBytes(secret);
            byte[] bytes2 = aSCIIEncoding.GetBytes(message);
            using HMACSHA256 hMACSHA = new HMACSHA256(bytes);
            byte[] inArray = hMACSHA.ComputeHash(bytes2);
            return Convert.ToBase64String(inArray);
        }
    }
}

