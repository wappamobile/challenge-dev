
using AutoMapper;
using DotNetCore.Objects;

namespace Wappa.Middleware.Application
{
    public class WappaMiddlewareAplicationBase
    {
        public readonly IMapper _objectMapper;

        public WappaMiddlewareAplicationBase()
        {

        }
        public WappaMiddlewareAplicationBase(IMapper objectMapper)
        {
            _objectMapper = objectMapper;
        }

        public ErrorDataResult<T> ErrorDataResult<T>(string message)
        {
            return new ErrorDataResult<T>(message);
        }

        public ErrorResult ErrorResult(string message)
        {
            return new ErrorResult(message);
        }

        public SuccessDataResult<T> SuccessDataResult<T>(T data)
        {
            return new SuccessDataResult<T>(data);
        }

        public SuccessResult SuccessResult()
        {
            return new SuccessResult();
        }
    }
}
