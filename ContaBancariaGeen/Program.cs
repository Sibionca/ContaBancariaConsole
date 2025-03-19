using ContaBancariaGeen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ContaBancariaGeen
{
    class Program
    {
        static void Main(string[] args)
        {
            var contaService = new ContaService();
            var clienteService = new ClienteService(contaService);
            var executaService = new ExecutaServicos(contaService, clienteService);

            executaService.EscolhaServico();
        }
    
    }
}
