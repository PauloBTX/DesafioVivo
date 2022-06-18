using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModel
{
    public partial class ResponseModel
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        
    }

}
