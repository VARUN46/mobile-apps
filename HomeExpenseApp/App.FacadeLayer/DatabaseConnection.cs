using App.Interfaces.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.FacadeLayer
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public string GetDbPath()
        {
            var sqliteFilename = AppSettings.SQLiteDbFile;
            #if __IOS__
                // Just use whatever directory SpecialFolder.Personal returns
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
      
            #else
                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;

            #endif
            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }

        public SQLiteConnection GetConnection()
        {
            var path = GetDbPath();
            var connection = new SQLiteConnection(path);
            return connection;
        }

    }
}
