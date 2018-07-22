using System;
using Akavache.Sqlite3;

// Note: This class file is *required* for iOS to work correctly, and is 
// also a good idea for Android if you enable "Link All Assemblies".
namespace StatisticsRomania
{
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }


    public class PreserveAttribute : Attribute
    {
    }
}