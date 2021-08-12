using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IHomeService
    {
        int GetCantPersonasRegistradas();

        byte GetPromEdadRacing();

        IEnumerable<SocioModelDto> GetCasados();

        IEnumerable<string> GetNombresRiver();

        IEnumerable<SocioModelDto> GetSociosEdad();
    }
}
