using Microsoft.AspNetCore.Identity;

namespace Services.Helpers
{
    public class GeneralHelper
    {
        public static string GenerateRandomNo()
        {
            int min = 10000;
            int max = 99990;
            Random _rdm = new Random();
            return _rdm.Next(min, max).ToString();
        }
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[]
            {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",   
            "abcdefghijkmnopqrstuvwxyz",   
            "0123456789",                   
            "!@$?_-"                       
        };
            Random rand = new Random(Environment.TickCount);
            char[] chars = new char[opts.RequiredLength];
            int[] charCounts = new int[randomChars.Length];

            // Ensure each category is represented at least once
            for (int i = 0; i < randomChars.Length; i++)
            {
                chars[i] = randomChars[i][rand.Next(randomChars[i].Length)];
                charCounts[i]++;
            }

            // Fill the remaining slots with random characters from any category
            for (int i = randomChars.Length; i < chars.Length; i++)
            {
                string rcs = randomChars[rand.Next(randomChars.Length)];
                chars[i] = rcs[rand.Next(rcs.Length)];
                charCounts[Array.IndexOf(randomChars, rcs)]++;
            }

            // Shuffle the characters
            return new string(chars.OrderBy(x => rand.Next()).ToArray());
        }
        public static string EncryptString(string? strEncrypted)
        {
            if (strEncrypted == null) return null;
            byte[] b = System.Text.Encoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encrString == null) return "";
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }
    }
}
