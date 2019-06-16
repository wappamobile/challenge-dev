using System;
using System.Collections.Generic;

namespace MicroservicesPlatform
{
    public interface ILogContextTokenProvider
    {
        ContextToken GetToken(string tokenType);
    }

    public class LogContextTokenProvider : ILogContextTokenProvider
    {
        private readonly IDictionary<string, object> _context;

        public LogContextTokenProvider(IDictionary<string, object> context)
        {
            _context = context;
        }

        public ContextToken GetToken(string tokenType)
        {
            if (tokenType == null) throw new ArgumentNullException(tokenType);
            return _context.ContainsKey(tokenType)
                       ? new ContextToken(tokenType, _context?[tokenType] as string)
                       : null;
        }
    }

    public class ContextToken
    {
        public ContextToken(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; }
        public string Value { get; }
    }

    public static class LogContextTypes
    {
        public static string CORRELATION_TOKEN = "x-correlation-token";
        public static string USER_SESSION_TOKEN = "x-user-session-token";
    }
}