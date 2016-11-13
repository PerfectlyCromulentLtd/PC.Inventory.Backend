using OxHack.Inventory.Web.Models.Commands;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Extensions
{
    public static class ConcurrencyIdExtensions
    {
        internal static int GetDecryptedConcurrencyId(this IConcurrencyAwareCommand @this, EncryptionService encryptionService)
        {
            return AsDecryptedConcurrencyId(@this.ConcurrencyId, encryptionService);
        }

        internal static int AsDecryptedConcurrencyId(this string concurrencyIdString, EncryptionService encryptionService)
        {
            var segments = concurrencyIdString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length != 2)
            {
                throw new InvalidOperationException("ConcurrencyId is in the wrong format");
            }

            var encryptedConcurrencyId = Convert.FromBase64String(segments[0]);
            var iv = Convert.FromBase64String(segments[1]);

            int concurrencyId = Int32.Parse(encryptionService.DecryptAscii(encryptedConcurrencyId, iv));
            return concurrencyId;
        }
    }
}
