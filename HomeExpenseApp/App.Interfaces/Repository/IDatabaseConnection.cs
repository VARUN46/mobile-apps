using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces.Repository
{
    public interface IDatabaseConnection
    {
        SQLiteConnection GetConnection();
    }
}
