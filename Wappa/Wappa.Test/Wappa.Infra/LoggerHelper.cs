using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Wappa.Test.Wappa.Infra
{
    public static class LoggerHelper
    {
        public static ILogger<T> GetLogger<T>()
        {
            var logger = Substitute.For<ILogger<T>>();

            logger.LogTrace(Arg.Any<string>(), Arg.Any<object[]>());
            logger.LogInformation(Arg.Any<string>(), Arg.Any<object[]>());

            return logger;
        }
    }
}