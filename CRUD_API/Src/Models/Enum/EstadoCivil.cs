using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRUD_API.Src.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EstadoCivil
    {
        SOLTEIRO,
        NAMORANDO,
        CASADO
    }
}