using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MVCMOVIES.Models
{
    public class Operations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Fecha de operacion")]
        [JsonProperty(PropertyName = "Fecha")]
        public string Date { get; set; }

        [DisplayName("Long/Short")]
        [JsonProperty(PropertyName = "Tipo")]
        public bool OperactionType { get; set; }


        [DisplayName("Monto ganado/perdido")]
        [JsonProperty(PropertyName = "Monto")]
        public long Amount { get; set; }

        [DisplayName("Ganacia/perdida")]
        [JsonProperty(PropertyName = "IsLost")]
        public bool IsLost { get; set; }



        [DisplayName("Porcentaje %")]
        [JsonProperty(PropertyName = "%")]
        public float Percentage { get; set; }

        [DisplayName("Observaciones")]
        [JsonProperty(PropertyName = "Observaciones")]
        public string Description { get; set; }

      
        public long UserId { get; set; }
        public  User User { get; set; }
    }
}
