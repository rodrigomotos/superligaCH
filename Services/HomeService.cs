using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace Services
{
    public class HomeService : IHomeService
    {
        private string _stringCSV;

        public HomeService(string stringCSV)
        {
            _stringCSV = stringCSV;
        }

        #region Public Methods

        /// <summary>
        /// Retorna cantidad de personas (hinchas) registradas en total, guardados en el archivo .CSV
        /// </summary>
        public int GetCantPersonasRegistradas()
        {
            return _GetCantPersonasRegistradas();
        }

        /// <summary>
        /// Promedio de edad de los hinchas de Racing a partir del listado de Socios.
        /// </summary>
        public byte GetPromEdadRacing()
        {
            return _GetPromEdadRacing();
        }

        /// <summary>
        ///Listado con las 100 primeras personas casadas, con estudios Universitarios, ordenadas de menor a mayor según su edad.
        /// </summary>
        public IEnumerable<SocioModelDto> GetCasados()
        {
            return _GetCasados();
        }

        /// <summary>
        /// Listado con los 5 nombres más comunes entre los hinchas de River.
        /// </summary>
        public IEnumerable<string> GetNombresRiver()
        {
            return _GetNombresRiver();
        }

        /// <summary>
        /// Listado, ordenado de mayor a menor según la cantidad de socios, que enumera, junto con cada equipo, el promedio de edad
        /// de sus socios, la menor edad y la mayor edad registrada de cada uno.
        /// </summary>
        public IEnumerable<SocioModelDto> GetSociosEdad()
        {
            return _GetSociosEdad();
        }

        #endregion

        #region Internal Methods
        /// <summary>
        /// se realiza el split de la cadena del archivo CSV y se devuelve un listado para realizar las consultas dentro del servicio
        /// </summary>
        internal List<SocioModel> Socios()
        {
            List<SocioModel> socios = new List<SocioModel>();
            foreach (string row in _stringCSV.Split('\r', '\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    socios.Add(new SocioModel
                    {
                        Nombre = row.Split(';')[0],
                        Edad = byte.Parse(row.Split(';')[1]),
                        Equipo = row.Split(';')[2],
                        EstadoCivil = row.Split(';')[3],
                        NivelDeEstudios = row.Split(';')[4]
                    });
                }
            }

            return socios;
        }
        #endregion

        #region private Methods

        /// <summary>
        /// Función que retorna el promedio de algún equipo como parámetro, a partir de List Socios. _GetPromEdad(string equipo)
        /// </summary>
        private byte _GetPromEdad(string equipo)
        {
            //consulto todas las edades de los hinchas de algún equipo
            var listado = from hinchas in this.Socios() where hinchas.Equipo == equipo select hinchas.Edad;
            //Total de hinchas de Racing
            int cantidadTotal = listado.ToArray().Length;
            int totalEdades = 0;
            int promedio = 0;
            //Promedio
            foreach (var edades in listado)
            {
                totalEdades += edades;
            }
            promedio = totalEdades / cantidadTotal;
            return (byte)promedio;
        }

        private byte _GetMenorEdad(string equipo)
        {
            byte edad = 0;

            //consulto la menor edad del listado según el equipo
            var listado = (from e in Socios() where e.Equipo == equipo orderby e.Edad ascending select e.Edad).Take(1);

            edad = listado.First();

            return edad;
        }

        private byte _GetMayorEdad(string equipo)
        {
            byte edad = 0;

            //consulto la menor edad del listado según el equipo
            var listado = (from e in Socios() where e.Equipo == equipo orderby e.Edad descending select e.Edad).Take(1);

            edad = listado.First();

            return edad;
        }

        private int _GetCantPersonasRegistradas()
        {
            return this.Socios().ToArray().Length;
        }

        private byte _GetPromEdadRacing()
        {
            return _GetPromEdad("Racing");
        }

        private List<SocioModelDto> _GetCasados()
        {
            List<SocioModelDto> socioModels = new List<SocioModelDto>();

            var casados = (from c in this.Socios()
                           where c.EstadoCivil == "Casado"
                           where c.NivelDeEstudios == "Universitario"
                           orderby c.Edad ascending
                           select (c.Nombre, c.Edad, c.Equipo)).Take(100);

            int cont = 1;

            foreach (var listado in casados)
            {
                socioModels.Add(new SocioModelDto {Cont = cont, Nombre = listado.Nombre, Edad = listado.Edad, Equipo = listado.Equipo });
                cont += 1;
            }

            return socioModels;
        }

        private List<string> _GetNombresRiver()
        {
            var nombres = from n in Socios()
                          where n.Equipo == "River"
                          group n by n.Nombre
                      into CountNombres
                          select new { Nombre = CountNombres.Key, Count = CountNombres.Count() };

            var listado = (from n in nombres orderby n.Count descending select n.Nombre).Take(5);

            return listado.ToList();
        }

        private List<SocioModelDto> _GetSociosEdad()
        {
            List<SocioModelDto> socioModels = new List<SocioModelDto>();

            //Cantidad de socios
            var cantSocios = from n in Socios() group n by n.Equipo into CantSocios select new { Equipo = CantSocios.Key, Count = CantSocios.Count() };

            int cont = 1;

            foreach (var listado in cantSocios.OrderByDescending(l => l.Count))//orden descendiente para la cantidad de socios en foreach
            {
                socioModels.Add(
                    new SocioModelDto
                    {
                        Cont = cont,
                        Equipo = listado.Equipo,
                        CantSocios = listado.Count,
                        PromedioEdad = this._GetPromEdad(listado.Equipo),
                        MenorEdad = this._GetMenorEdad(listado.Equipo),
                        MayorEdad = this._GetMayorEdad(listado.Equipo),
                    }
                    );
                cont += 1;
            }

            return socioModels;
        }

        #endregion

       

    }
}
