using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Automoviles.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection(); //SQLite es un motor de base de datos 
                                               //utilizado en plataformas moviles, estas 
                                              //son propiedades para obtener conexión a la base

    }
}
