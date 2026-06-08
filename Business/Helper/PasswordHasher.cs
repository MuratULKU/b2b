using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    /// <summary>
    /// PBKDF2 + SHA512 + Salt ile şifre hashleme ve doğrulama.
    /// Dış paket gerektirmez — System.Security.Cryptography kullanır.
    /// </summary>
    public static class PasswordHasher
    {
        // --- Ayarlar ---
        private const int SaltSize = 32;    // 256 bit salt
        private const int HashSize = 64;    // 512 bit hash
        private const int Iterations = 150_000; // NIST 2024 önerisi (SHA512 için)
        private const char Separator = ':';

        // Versiyon — ileride iterasyon artırırsanız eski hash'leri tanımak için
        private const string Version = "v1";

        /// <summary>
        /// Düz şifreyi hashler.
        /// Dönen string veritabanına kaydedilir.
        /// Format: "v1:{iterations}:{base64salt}:{base64hash}"
        /// </summary>
        public static string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Şifre boş olamaz.", nameof(password));

            // 1. Rastgele salt üret
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // 2. PBKDF2 ile hash üret
            byte[] hash = Pbkdf2(password, salt, Iterations, HashSize);

            // 3. Tek string olarak birleştir
            return string.Join(Separator,
                Version,
                Iterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Girilen düz şifreyi, kayıtlı hash ile karşılaştırır.
        /// Timing-safe karşılaştırma kullanır.
        /// </summary>
        public static bool Verify(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Şifre boş olamaz.", nameof(password));

            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hash boş olamaz.", nameof(hashedPassword));

            string[] parts = hashedPassword.Split(Separator);

            if (parts.Length != 4)
                throw new FormatException("Hash formatı geçersiz.");

            string version = parts[0];
            int iterations = int.Parse(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] storedHash = Convert.FromBase64String(parts[3]);

            // Versiyon kontrolü (ileride v2 eklenirse buraya dallanma yapılır)
            if (version != Version)
                throw new NotSupportedException($"Desteklenmeyen hash versiyonu: {version}");

            // Aynı salt ve iterasyon ile tekrar hash üret
            byte[] computedHash = Pbkdf2(password, salt, iterations, storedHash.Length);

            // Timing-safe karşılaştırma (brute-force zamanlama saldırısını önler)
            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }

        /// <summary>
        /// Mevcut hash'in rehash edilmesi gerekip gerekmediğini kontrol eder.
        /// Iterasyon sayısı güncel değere eşit değilse true döner.
        /// Kullanım: kullanıcı başarılı login sonrası hash güncellenir.
        /// </summary>
        public static bool NeedsRehash(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) return true;

            string[] parts = hashedPassword.Split(Separator);
            if (parts.Length != 4) return true;

            return parts[0] != Version || int.Parse(parts[1]) != Iterations;
        }

        // --- Dahili PBKDF2 yardımcısı ---
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputLength)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512);

            return deriveBytes.GetBytes(outputLength);
        }
    }
}
