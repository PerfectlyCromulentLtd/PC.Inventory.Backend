using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Sqlite
{
    /// <summary>
    /// Hack until EF7 supports offline concurrency token generation
    /// See https://github.com/aspnet/EntityFramework/issues/2195
    /// </summary>
    public class SqliteWriteLock
    {
    }
}
