using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Produto
    {
        public Int64 Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public string Data { get; set; }

        public Produto()
        {
                
        }

        public Produto(string nome, decimal preco, string data)
        {
            Nome = nome;
            Preco = preco;
            Data = data;
        }
    }
}
