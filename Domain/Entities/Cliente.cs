using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Cliente
    {
        public Cliente() { }

        public int ClienteID { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string DocRgNumero { get; set; }
        public string DocRgOrgaoEmissor { get; set; }
        public string DocRgUF { get; set; }

        public string DocCPF { get; set; }
        public string Endereco { get; set; }
        public int Ativo { get; set; }
        
        public bool? EstaAtivo
        {
            get
            {
                return (Ativo == 1) ? true : false;
            }
        }
        public DateTime DataCadastro { get; set; }
    }
}

