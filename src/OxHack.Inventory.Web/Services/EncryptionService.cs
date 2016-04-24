using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Services
{
    public class EncryptionService
    {
        private readonly byte[] key;
        private const int keyLength = 16;

        public EncryptionService()
        {
            this.key = this.GetRandomBytes();
        }

        private byte[] GetRandomBytes()
        {
            Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(Path.GetRandomFileName(), EncryptionService.keyLength, 32);
            return keyGenerator.GetBytes(EncryptionService.keyLength);
        }

        internal byte[] EncryptAscii(string payload, out byte[] iv)
        {
            iv = this.GetRandomBytes();

            using (var provider = new AesCryptoServiceProvider())
            {
                using (var encryptor = provider.CreateEncryptor(this.key, iv))
                {
                    var buffer = Encoding.ASCII.GetBytes(payload.PadRight(EncryptionService.keyLength));
                    var result = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);

                    return result;
                }
            }
        }

        internal string DecryptAscii(byte[] payload, byte[] iv)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                using (var decryptor = provider.CreateDecryptor(this.key, iv))
                {
                    var buffer = decryptor.TransformFinalBlock(payload, 0, payload.Length);

                    var result = Encoding.ASCII.GetString(buffer);
                    return result;
                }
            }
        }
    }
}
