using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Automoviles.Tablas
{
    public class T_Autos
    {
        //Se crea la tabla T_Autos que contendra: la marca del automóvil
        //el color del automóvil, el año del automóvil y el id que es autoincrementable
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public String Marca { get; set; }
        [MaxLength(255)]
        public String Color { get; set; }
        [MaxLength(255)]
        public String Anio { get; set; }
    }
}
