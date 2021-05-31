using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IProdutoFunction
    {
        List<Produto> GetProdutos();
        Produto GetProdutoById(int id);
        Produto GetProdutoByName(string name);
        List<Produto> GetProdutosById(int id);
        List<Produto> GetProdutosByName(string name);
    }
}
