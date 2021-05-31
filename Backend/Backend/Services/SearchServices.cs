using Backend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SearchServices : IEqualityComparer<Produto>
    {
        public bool Equals(Produto x, Produto y)
        {
            return x.Id == y.Id &&
            x.Nome == y.Nome &&
            x.Preco == y.Preco &&
            x.Data == y.Data;
        }

        public int GetHashCode(Produto obj)
        {
            return obj.Id.GetHashCode() ^
            obj.Nome.GetHashCode() ^
            obj.Preco.GetHashCode() ^
            obj.Data.GetHashCode();
        }
    }
}
