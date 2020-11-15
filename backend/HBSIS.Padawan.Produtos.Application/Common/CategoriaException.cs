using System;
using System.Collections.Generic;

namespace HBSIS.Padawan.Produtos.Application.Common
{
    public class CategoriaException : Exception
    {
        public List<Errors> Errors { get; private set; }

        public CategoriaException(List<Errors> errors)
        {
            this.Errors = errors;
        }

        public CategoriaException(string message) : base(message) { }
        public CategoriaException(string message, Exception inner) : base(message, inner) { }
        protected CategoriaException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
