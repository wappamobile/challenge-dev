using Microsoft.AspNetCore.Mvc;
using Wappa.Models.Errors;

namespace Wappa.Commom {
    internal static class Criticas {
        private static BadRequestObjectResult BadRequest (string code, string message) => BadRequest (code, message, null);

        private static BadRequestObjectResult BadRequest (string code, string message, string tag) =>
            new BadRequestObjectResult (new Error () { Code = code, Message = message, Tag = tag });

        internal static class Motorista {
            
            // Erro - motorista já cadastrado
            internal static BadRequestObjectResult MotoristaJaCadastrado () => 
                Criticas.BadRequest ("ERMOT001", "Motorista já cadastrado.");
        }
    }
}