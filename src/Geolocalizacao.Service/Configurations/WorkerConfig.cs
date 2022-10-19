using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Service.Configurations
{
    public class WorkerConfig
    {
        public bool Ativo { get; set; }
        public int IntervaloEmSegundos { get; set; }
        public int IntervaloEmMinutos { get; set; }
        public int IntervaloEmHoras { get; set; }
    }
}
