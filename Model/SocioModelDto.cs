using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class SocioModelDto
    {
        public int Cont { get; set; }

        public string Nombre { get; set; }

        public byte Edad { get; set; }

        public string Equipo { get; set; }

        public string EstadoCivil { get; set; }

        public string NivelDeEstudios { get; set; }

        public int CantSocios { get; set; }

        public byte PromedioEdad { get; set; }
        
        public byte MenorEdad { get; set; }

        public byte MayorEdad { get; set; }
    }
}
