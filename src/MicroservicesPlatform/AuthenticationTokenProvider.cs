using IdentityModel.Client;
using MicroservicesPlatform.Configuration;
using System;
using System.Threading.Tasks;

namespace MicroservicesPlatform
{
    public interface IAuthTokenProvider
    {
        Task<TokenResponse> GetToken();
        Task<TokenResponse> GetToken(string scope);
    }

    public class AuthenticationTokenProvider : IAuthTokenProvider
    {
        [Obsolete]
        private readonly TokenClient _tokenClient;
        private readonly string _audience;

        [Obsolete]
        public AuthenticationTokenProvider(TokenClient tokenClient, string audience)
        {
            _tokenClient = tokenClient;
            _audience = audience;
        }

        [Obsolete]
        public Task<TokenResponse> GetToken()
        {
            return GetToken(null);
        }

        [Obsolete]
        public async Task<TokenResponse> GetToken(string scope)
        {
            var extra = new { audience = _audience };
            var token = await _tokenClient.RequestClientCredentialsAsync(scope, extra)
                                          .ConfigureAwait(false);
            return token;
        }
    }

    public class AuthenticationTokenProviderFactory : IAuthTokenProviderFactory
    {
        private static readonly IAuthTokenProviderFactory Singleton = new AuthenticationTokenProviderFactory();

        private AuthenticationTokenProviderFactory()
        {
        }

        [Obsolete]
        public AuthenticationTokenProvider Create(AuthService config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var tokenClient = TokenClientFactory.Instance.Create(config);
            var provider = new AuthenticationTokenProvider(tokenClient, config.Audience);
            return provider;
        }

        public static IAuthTokenProviderFactory Instance => Singleton;
    }

    public interface IAuthTokenProviderFactory
    {
        AuthenticationTokenProvider Create(AuthService config);
    }

    public class TokenClientFactory : ITokenClientFactory
    {
        private static readonly TokenClientFactory Singleton = new TokenClientFactory();

        private TokenClientFactory()
        {
        }

        [Obsolete]
        public TokenClient Create(AuthService config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            return new TokenClient(config.Address,
                                   config.ClientId,
                                   config.ClientSecret,
                                   style: AuthenticationStyle.PostValues);
        }

        public static ITokenClientFactory Instance => Singleton;
    }

    public interface ITokenClientFactory
    {
        [Obsolete]
        TokenClient Create(AuthService config);
    }
}
