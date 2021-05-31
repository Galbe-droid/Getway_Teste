using Backend.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProdutoFunctions : IProdutoFunction
    {
        private readonly DataContext _context;

        public ProdutoFunctions(DataContext context)
        {
            _context = context;
        }

        public Produto GetProdutoById(int id)
        {
            return _context.produtos.Where(obj => obj.Id == id).FirstOrDefault();
        }

        public Produto GetProdutoByName(string name)
        {
            return _context.produtos.Where(obj => obj.Nome == name).FirstOrDefault();
        }

        public List<Produto> GetProdutos()
        {
            return _context.produtos.ToList();
        }

        public List<Produto> GetProdutosById(int id)
        {
            return _context.produtos.Where(obj => obj.Id == id).ToList();
        }

        public List<Produto> GetProdutosByName(string name)
        {
            return _context.produtos.Where(obj => obj.Nome == name).ToList();
        }
    }
}
