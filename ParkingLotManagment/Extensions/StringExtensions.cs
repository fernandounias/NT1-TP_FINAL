using System.Security.Cryptography;
using System.Text;

namespace ParkingLotManagment.Extensions
{
    public static class StringExtensions
    {
        ///funcion para encriptar
        public static byte[] Encript(this string text)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.ASCII.GetBytes(text));
            }
        }

        public static void ValidatePassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("la contraseña es requerida");
            }

            if (password.Length < 8)
            {
                throw new Exception("la contraseña debe tener minimo de 8 caracteres");
            }
        }  

    }
}
