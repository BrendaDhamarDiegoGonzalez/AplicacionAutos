using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Automoviles.Tablas
{
    public class T_Clientes
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public String Nombre { get; set; }
        [MaxLength(255)]
        public String Apellido { get; set; }
        [MaxLength(255)]
        public String Alias { get; set; }
        [MaxLength(255)]
        public String NumeroTelefonico { get; set; }
        [MaxLength(255)]
        public String Correo { get; set; }
    }
}
