using App.Interfaces.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.FacadeLayer
{
    internal class DatabaseConnection : IDatabaseConnection
    {

        private object locker = new object(); // class level private field
        public string GetDbPath()
        {
            var sqliteFilename = AppSettings.SQLiteDbFile;
            #if __ANDROID__
                // Just use whatever directory SpecialFolder.Personal returns
                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
            #else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
            #endif
            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }

        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(GetDbPath());
            return connection;
        }

    }
}
