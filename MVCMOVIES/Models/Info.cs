using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMOVIES.Models
{
    public class Info
    {
        [ForeignKey("User")]
        public long Id { get; set; }

        [DisplayName("Monto")]
        [JsonProperty(PropertyName = "MontoInicial")]
        public long InitialAmount { get; set; }

        [DisplayName("Monto a la Fecha")]
        [JsonProperty(PropertyName = "MontoActual")]
        public long TotalAmount { get; set; }

        public virtual User User { get; set; }
    }
}
