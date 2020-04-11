using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Automoviles.Tablas
{
   public class T_Accesorios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public String Nombre{ get; set; }
        [MaxLength(255)]
        public String Categoria { get; set; }
        [MaxLength(255)]
        public String Piezas { get; set; }
        [MaxLength(255)]
        public String Color { get; set; }
        [MaxLength(255)]
        public String Proveedor { get; set; }
    }
}
