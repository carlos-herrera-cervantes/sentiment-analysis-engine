using System;
using System.Security.Cryptography;

namespace SentimentAnalysisEngine.Repository.Repositories
{
    public class ApiKey : IApiKey
    {
        /// <summary>
        /// Generates an API Key like base 64 string
        /// </summary>
        /// <returns>String</returns>
        public string GenerateApiKey()
        {
            var key = new byte[32];
            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(key);

            return Convert.ToBase64String(key).Replace(@"/", "=");
        }
    }
}