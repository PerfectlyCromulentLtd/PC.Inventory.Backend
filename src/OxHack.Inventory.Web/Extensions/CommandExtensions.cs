using OxHack.Inventory.Web.Models.Commands;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Extensions
{
    public static class CommandExtensions
    {
        internal static int GetDecryptedConcurrencyId(this IConcurrencyAwareCommand @this, EncryptionService encryptionService)
        {
            int concurrencyId;
            var segments = @this.ConcurrencyId.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length != 2)
            {
                throw new InvalidOperationException("ConcurrencyId is in the wrong format");
            }

            var encryptedConcurrencyId = Convert.FromBase64String(segments[0]);
            var iv = Convert.FromBase64String(segments[1]);

            concurrencyId = Int32.Parse(encryptionService.DecryptAscii(encryptedConcurrencyId, iv));
            return concurrencyId;
        }
    }
}
