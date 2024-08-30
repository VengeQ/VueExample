namespace Security
{
    public class TokenOptions
    {
        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;

        public int ExpiresInMinutes { get; init; }

        public string Key { get; init; } = null!;
    }
}
