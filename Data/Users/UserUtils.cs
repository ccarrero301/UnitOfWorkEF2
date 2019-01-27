namespace Data.Users
{
    using System.Text;
    using System.Security.Cryptography;

    public static class UserUtils
    {
        public static string EncryptPassword(string password)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));

            foreach (var theByte in crypto)
                hash.Append(theByte.ToString("x2"));

            return hash.ToString();
        }
    }
}