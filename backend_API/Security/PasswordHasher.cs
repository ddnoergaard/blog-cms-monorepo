using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;

namespace backend_API.Security
{
    public class PasswordHasher
    {
        private const int TimeCost = 2;
        private const int MemoryCost = 19456;
        private const int Parallelism = 1;
        private const int SaltSize = 16;
        private const int HashLength = 32;

        public string Hash(string plainPassword)
        {
            Argon2Config config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing, //THIS USES THE ARGON2ID
                Version = Argon2Version.Nineteen,
                TimeCost = TimeCost,
                MemoryCost = MemoryCost,
                Lanes = Parallelism,
                Threads = Parallelism,
                Password = Encoding.UTF8.GetBytes(plainPassword),
                Salt = RandomNumberGenerator.GetBytes(SaltSize),
                HashLength = HashLength
            };

            return Argon2.Hash(config);
        }

        public bool Verify(string plainPassword, string HashedPaswword) => Argon2.Verify(HashedPaswword, plainPassword);


    }
}
