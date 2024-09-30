using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //out:parametreyi verdiğimizde değişen nesne aynı zamanda byte[] arrayimize aktarılacak.
        //bir methodun birden fazla dönüş tipi yapmasını istediğimizde kullanırız genellikle.
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512()) { 
            
                passwordSalt = hmac.Key;
                //compute hash fonksiyonuna key olarak saltı verdi o yüzden saltlanmış şifre hashlandi.
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        }
    }

