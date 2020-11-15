using System;
using System.Collections.Generic;

namespace HBSIS.Padawan.Produtos.Application.Common
{
    public class FornecedorException : Exception
    {
        public List<Errors> Errors { get; private set; }

        public FornecedorException(List<Errors> errors)
        {
            this.Errors = errors;
        }

        public FornecedorException(string message) : base(message) { }
        public FornecedorException(string message, Exception inner) : base(message, inner) { }
        protected FornecedorException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
