using Microsoft.AspNetCore.Http;

namespace UrlShortener.Services
{
    public class UrlShorteningService
    {
        public const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new();
        public string GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];
            for(var i = 0; i < NumberOfCharsInShortLink; i++)
            {
                int randomIndex = _random.Next(Alphabet.Length - 1);
                codeChars[i] = Alphabet[randomIndex];
            }
            var code= new string(codeChars);
            return new string(codeChars);
        }
    }
}
