using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaConsumoERP.Domain.Entities
{
    [Table("trazaconsumoerp")]
    public class TrazaConsumoERP
    {
        [Column("trazaconsumoerpid")]
        [Key]
        public long TrazaConsumoERPId { get; set; }

        [Column("requestbody")]
        public string RequestBody { get; set; }

        [Column("requestresponse")]
        public string RequestResponse { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("fecharequest")]
        public DateTime FechaRequest { get; set; }

        [Column("nombreapi")]
        public string NombreApi { get; set; }
    }
}
