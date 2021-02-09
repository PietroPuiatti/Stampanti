namespace Test.Extensions
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        public static string Coalesce(this string s, string sDefault)
        {
            if (string.IsNullOrEmpty(s))
                return sDefault ?? string.Empty;

            return s;
        }

        public static string EmptyIfNull(this string s)
        {
            return s.Coalesce(string.Empty);
        }

        public static string NullIfEmpty(this string s)
        {
            if (s == string.Empty)
                s = null;

            return s;
        }

        public static string Trimmed(this string s)
        {
            return s.EmptyIfNull().Trim();
        }

        public static string Trimmed(this string s, params char[] trimChars)
        {
            return s.EmptyIfNull().Trim(trimChars);
        }

        public static string Left(this string s, int maxLength)
        {
            return s.EmptyIfNull().Substring(0, Math.Min(maxLength, s.EmptyIfNull().Length));
        }

        public static string Right(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return s.Length <= maxLength ? s : s.Substring(s.Length - maxLength);
        }

        public static int NthIndexOf(this string input, char value, int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n", n, "n is less than zero.");

            var i = -1;

            do
            {
                i = input.IndexOf(value, i + 1);
                n--;
            } while (i != -1 && n > 0);

            return i;
        }

        public static int NthLastIndexOf(this string input, char value, int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n", n, "n is less than zero.");

            var i = input.Length;

            do
            {
                i = input.LastIndexOf(value, i - 1);
                n--;
            } while (i != -1 && n > 0);

            return i;
        }

        public static bool IsValidHttpAddress(this string url)
        {
            var result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsEmptyOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static T As<T>(this string s)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            return (T) converter.ConvertFromInvariantString(s);
        }

        public static T AsOrDefault<T>(this string s)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                return (T) converter.ConvertFromInvariantString(s);
            }
            catch
            {
                return default;
            }
        }

        public static string ReplaceUPlusSequences(this string s)
        {
            return Regex.Replace(s.EmptyIfNull(),
                                 @"U\+[0-9A-Fa-f]{4}",
                                 match => ((char) Convert.ToInt32(match.Value.Right(4), 16)).ToString(),
                                 RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Suitable for password hashing
        /// </summary>
        /// <param name="salt">A random salt will be generated if input is null</param>
        /// <returns>Lenght 64</returns>
        public static string ComputeHash(this string str, ref string salt)
        {
            //if you don't care about the hash being reverted
            //SHA1Managed is faster with a smaller output
            var hasher = new SHA256Managed();
            var keyLength = 4;

            var data = Encoding.UTF8.GetBytes(str);
            var key = new byte[keyLength];
            var dataReady = new byte[data.Length + keyLength];

            if (salt == null)
            {
                //random salt
                using (var random = new RNGCryptoServiceProvider())
                {
                    random.GetNonZeroBytes(key);
                }

                salt = Convert.ToBase64String(key);
            }

            //memory consuming operation
            Array.Copy(Encoding.UTF8.GetBytes(salt), key, keyLength);
            Array.Copy(data, dataReady, data.Length);
            Array.Copy(key, 0, dataReady, data.Length, keyLength);

            //hash
            return BitConverter.ToString(hasher.ComputeHash(dataReady)).Replace("-", string.Empty);
        }

        /// <summary>
        ///     Encrypt using Rijndael
        /// </summary>
        public static string Encrypt(this string str, string password)
        {
            var salt = Encoding.UTF8.GetBytes(password.Length.ToString());
            var text = Encoding.UTF8.GetBytes(str);
            byte[] cipher;

            var key = new PasswordDeriveBytes(password, salt);

            using (var encryptor = new RijndaelManaged().CreateEncryptor(key.GetBytes(32), key.GetBytes(16)))
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(text, 0, text.Length);
                cryptoStream.FlushFinalBlock();
                cipher = memoryStream.ToArray();
            }

            return Convert.ToBase64String(cipher);
        }

        /// <summary>
        ///     Decrypt Rijndael encrypted string
        /// </summary>
        public static string Decrypt(this string str, string password)
        {
            var salt = Encoding.UTF8.GetBytes(password.Length.ToString());
            var text = Convert.FromBase64String(str);
            var key = new PasswordDeriveBytes(password, salt);
            int outLen;

            using (var decryptor = new RijndaelManaged().CreateDecryptor(key.GetBytes(32), key.GetBytes(16)))
            using (var memoryStream = new MemoryStream(text))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                text = new byte[text.Length];
                outLen = cryptoStream.Read(text, 0, text.Length);
            }

            return Encoding.UTF8.GetString(text, 0, outLen);
        }
    }
}