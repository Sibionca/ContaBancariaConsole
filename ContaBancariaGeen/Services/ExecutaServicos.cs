using ContaBancariaGeen.Models;
using ContaBancariaGeen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ContaBancariaGeen.Services
{
    class ExecutaServicos
    {
        private readonly ContaService _contaService;
        private readonly ClienteService _clienteService;

        public ExecutaServicos(ContaService contaService, ClienteService usuarioService)
        {
            _contaService = contaService;
            _clienteService = usuarioService;
        }

        public void EscolhaServico()
        {
            Art.Inicial();
            WriteLine("=== BEM VINDO AO SISTEMA BANCÁRIO SAIYAJIN ===");

            var condicao = true;
            while (condicao)
            {

                WriteLine("\nSelecione o serviço desejado: \n");
                WriteLine("1 - Serviços de usuarios");
                WriteLine("2 - Serviços de contas");
                WriteLine("3 - Sair\n");
                Write("Insira o número do serviço: ");

                var validaResposta = ValidaInput.ValidaInt(ReadLine());
                var receba = RespostaFinal(validaResposta);

                if (receba == 3) return;

                if (receba == 2)
                {
                    if(!_clienteService.GetClientes().Any())
                    {
                        WriteLine("Você precisa cadastrar um cliente primeiro, redirecionando...\n\n");
                        var cliente = _clienteService.CadastraCliente();
                    }

                    _contaService.ServicosConta();
                } else
                {
                    _clienteService.ServicosCliente();
                }

                Clear();
            }

        }

        private int RespostaFinal(int opcao)
        {
            if (opcao == 3)
            {
                Art.Final();
                WriteLine("Obrigado por usar nosso sistema meu guerreiro! Até a proxima");
                ReadKey();
                return opcao;
            }

            while (opcao != 1 && opcao != 2)
            {
                WriteLine("\nDigite uma opção valida carai! 1 ou 2");
                Write("Insira 1 ou 2: ");
                opcao = ValidaInput.ValidaInt(ReadLine());
            }


            return opcao;
        }
    }
}
