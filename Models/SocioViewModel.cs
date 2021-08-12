using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Model;

namespace ChallengeSuperLiga.Models
{
    public class SocioViewModel
    {
        public SocioModelDto SocioModelDto { get; }
        
        public int CantPersonasRegistradas { get; set; }

        public byte PromedioEdad { get; set; }

        public IEnumerable<SocioModelDto> CasadosUniversitarios { get; set; }

        public IEnumerable<string> NombresRiver { get; set; }

        public IEnumerable<SocioModelDto> SociosEdad { get; set; }

        public string File { get; set; }

    }
}