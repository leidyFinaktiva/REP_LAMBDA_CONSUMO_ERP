using Integrador.ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Models
{
    public class TokenSettings
    {
        public string signature { get; set; } = "HMAC-SHA256";
        public string customerKey { get; set; } = "be5b034036a07ff3129b8843f50017cc5745049af8484430c793b1abc827eb75";
        public string secret { get; set; } = "960f8b86cfedbb1437cd23c2175299d8a5c8c3278a74a612dbb5456b7012b92c";
        public string accessToken { get; set; } = "04f81fc685c69202d23715c23c1ffe62c161cd8778148bbd4e810fb4d5efcec2";
        public string token { get; set; } = "cf7e5aaa9da6c41d7869be1f60c2f7cd10db819ce83367d77dab40a674351c38";
        public string realm { get; set; } = "7575517_SB1";

        public string clientURL { get; set; } = @"https://7575517-sb1.restlets.api.netsuite.com/app/site/hosting/restlet.nl?script=769&deploy=1";
        public string version { get; set; } = "1.0";
        public string disbursementURL { get; set; } 
        public string interestURL { get; set; }
    }
}
