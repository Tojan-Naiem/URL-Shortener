using Microsoft.AspNetCore.Http;

namespace UrlShortener.Services
{
    public class UrlShorteningService
    {
        private const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    }
}
