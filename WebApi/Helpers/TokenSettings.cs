﻿namespace WebApi.Helpers
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}