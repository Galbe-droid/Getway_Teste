using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMainFunction _mainFunction;
        private readonly IProdutoFunction _produtoFunction;

        public ProdutosController(IMainFunction mainFunction, IProdutoFunction produtoFunction)
        {
            _mainFunction = mainFunction;
            _produtoFunction = produtoFunction;
        }
        
        [HttpPost("buscarProdutos")]
        public IActionResult GetProdutos(int? id, string? nome, int? paginaCarregada)
        {
            if (paginaCarregada == null)
            {
                paginaCarregada = 1;
            }

            if (id == null || nome == null)
            {
                var todosProdutos = _produtoFunction.GetProdutos();
                int paginaAtual = 1;
                int itensPorPagina = 8;
                int itemAdicionado = 0 + (8 * (paginaAtual - 1));

                List<Produto> produtoPorPagina = new List<Produto>();

                while(produtoPorPagina.Count() <= itensPorPagina)
                {
                    if(todosProdutos.Count <= itemAdicionado)
                    {
                        return Ok(produtoPorPagina);
                    }
                    else
                    {
                        produtoPorPagina.Add(todosProdutos.ElementAt(itemAdicionado));
                        itemAdicionado++;
                    }
                    produtoPorPagina.Distinct();
                }
                return Ok(produtoPorPagina);
            }
            else
            {
                List<Produto> produtosProcuradosTotal = new List<Produto>();
                List<Produto> produtosPorID = new List<Produto>();
                List<Produto> produtosPorNome = new List<Produto>();
                List<Produto> produtoPorPagina = new List<Produto>();

                if (id != null)
                {
                    produtosPorID = _produtoFunction.GetProdutosById(Convert.ToInt32(id));
                }

                if (nome != null)
                {
                    produtosPorNome = _produtoFunction.GetProdutosByName(nome);
                }

                if (produtosPorID.Count > 0)
                {
                    produtosProcuradosTotal.AddRange(produtosPorID);
                }

                if (produtosPorNome.Count > 0)
                {
                    produtosProcuradosTotal.AddRange(produtosPorNome);
                }

                if (produtosProcuradosTotal.Count > 0)
                {
                    int paginaAtual = 1;
                    int itensPorPagina = 8;
                    int itemAdicionado = 0 + (8 * (paginaAtual - 1));

                    while (produtoPorPagina.Count <= itensPorPagina)
                    {
                        if (produtosProcuradosTotal.Count <= itemAdicionado)
                        {
                            var produtosSemDuplicatas = produtoPorPagina.Distinct(new SearchServices());
                            return Ok(produtosSemDuplicatas);
                        }
                        else
                        {
                            produtoPorPagina.Add(produtosProcuradosTotal.ElementAt(itemAdicionado));
                            itemAdicionado++;                            
                        }                        
                    }
                }
            }
            return BadRequest("Algo deu errado");
        }

        [HttpGet("obterProdutos/{id}")]
        public IActionResult GetProdutoById(int id)
        {
            var model = _produtoFunction.GetProdutoById(id);
            return Ok(model);
        }

        [HttpPost("salvarProduto")]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                string precoCorrectionString = produto.Preco.ToString("N2", new CultureInfo("pt-BR"));
                decimal precoCorrectionDecimal = Convert.ToDecimal(precoCorrectionString);

                produto.Preco = precoCorrectionDecimal;

                produto.Data = DateTime.Now.ToString("HH/mm/ss - dd/MM/yyyy");                

                _mainFunction.Add(produto);
                await (_mainFunction.SaveChangesAsync());

                return Ok(produto);
            }
            catch(Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpPost("salvarProduto/{id}")]
        public async Task<IActionResult> Update(Produto produto, int id)
        {
            try
            {
                Produto oldProduto;
                if(_produtoFunction.GetProdutoById(id) == null)
                {
                    return BadRequest("Error: Produto não encontrado");
                }
                else
                {
                    oldProduto = _produtoFunction.GetProdutoById(id);
                }

                string precoCorrectionString = produto.Preco.ToString("N2", new CultureInfo("pt-BR"));
                decimal precoCorrectionDecimal = Convert.ToDecimal(precoCorrectionString);

                produto.Preco = precoCorrectionDecimal;

                produto.Data = DateTime.Now.ToString("HH/mm/ss - dd/MM/yyyy");

                oldProduto = produto;

                _mainFunction.Update(oldProduto);
                await (_mainFunction.SaveChangesAsync());

                return Ok(oldProduto);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpDelete("excluirProduto/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_produtoFunction.GetProdutoById(id) == null)
            {
                return BadRequest("Error: Produto não encontrado");
            }
            else
            {
                var produto = _produtoFunction.GetProdutoById(id);
                _mainFunction.Delete(produto);

                await (_mainFunction.SaveChangesAsync());

                return Ok(produto);
            }
        }
    }
}
