using ContaBancariaGeen.Models;
using ContaBancariaGeen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ContaBancariaGeen.Services
{
    class ClienteService
    {
        private readonly List<Cliente> _clientes = new List<Cliente>();
        private readonly ContaService _contaService;

        public ClienteService(ContaService contaService)
        {
            _contaService = contaService;
        }

        public List<Cliente> GetClientes()
        {
            return _clientes;
        }

        public Cliente CadastraCliente()
        {
            WriteLine("\n=== Cadastro de Cliente ===\n");
            Write("Digite o nome do cliente: ");

            var entrada = ReadLine();
            var nome = ValidaInput.ValidaString(entrada);

            Write("\n Agora digite o CPF: ");

            var cpf = ReadLine();

            cpf = ValidaCpfLength(cpf);
            cpf = ValidaCpfIgual(cpf);

            var cliente = new Cliente(nome, cpf);

            _clientes.Add(cliente);
            _contaService.CadastrarConta(cliente);

            return cliente;
        }

        public Cliente AtualizarCliente()
        {
            if(!_clientes.Any())
            {
                WriteLine("Não existem clientes cadastrados para serem atualizados, redirecionado ao cadastro...");
                var clienteNovo = CadastraCliente();
                return clienteNovo;
            }

            WriteLine("Qual cliente você gostaria de atualizar?");

            foreach(var c in  _clientes)
            {
                WriteLine($"Sr(a) {c.Nome}, CPF: {c.Cpf}");
            }

            Write("Informe o CPF: ");

            var cpf = ReadLine();

            cpf = ValidaCpfLength(cpf);

            var cliente = _clientes.FirstOrDefault(c => c.Cpf == cpf);

            WriteLine("\nO que deseja atualizar?: ");
            WriteLine("1 - Nome");
            WriteLine("2 - Cpf");
            Write("Digite o valor referente: ");

            var entrada = ReadLine();
            var opcao = ValidaInput.ValidaInt(entrada);

            var resposta = RespostaFinal(opcao);

            if(resposta == 1)
            {
                Write("Digite o Nome atualizado: ");
                var texto = ReadLine();
                var nome = ValidaInput.ValidaString(texto);
                cliente.Nome = nome;
            } 
            else
            {
                Write("Digite o CPF atualizado: ");
                var texto = ReadLine();
                var cpfAtualizado = ValidaCpfLength(texto);
                var cpfs = _clientes.Where(c => c.Cpf != cpf).Select(c => c.Cpf);

                while(cpfs.Any(c => c == cpfAtualizado))
                {
                    WriteLine("O CPF informado é igual ao de outro cliente, por favor informe outro!");
                    texto = ReadLine();
                    cpfAtualizado = ValidaCpfLength(texto);
                }

                cliente.Cpf = cpfAtualizado;
            }

            return cliente;
        }

        public void ServicosCliente()
        {
            var condicao = true;

            while (condicao)
            {

                WriteLine("\nQual operações de cliente você deseja efetuar?: ");
                WriteLine("1 - Cadastro");
                WriteLine("2 - Atualizar");
                WriteLine("3 - Listar");
                WriteLine("4 - Voltar ao menu");
                Write("Digite a opção: ");

                var entrada = ReadLine();
                var resposta = ValidaInput.ValidaInt(entrada);

                switch (resposta)
                {
                    case 1:
                        CadastraCliente();
                        break;
                    case 2:
                        AtualizarCliente();
                        break;
                    case 3:
                        GetClientes();
                        break;
                    case 4:
                        condicao = false;
                        WriteLine("Retornando ao menu....");
                        break;
                    default:
                        WriteLine("Opa, você escolheu uma opção errada!");
                        break;
                }
            }
        }

        private string ValidaCpfLength(string cpf)
        {
            if(cpf.Length != 11)
            {
                while(cpf.Length != 11)
                {
                    Write("\n Digite um CPF válido com 11 digitos numericos: ");
                    cpf = ReadLine();
                }
            }

            return cpf;
        }

        private string ValidaCpfIgual(string cpf)
        {
            while (_clientes.Any(c => c.Cpf == cpf))
            {
                WriteLine("O CPF informado é igual ao de outro cliente, por favor informe outro!");
                Write("\n Digite um CPF válido com 11 digitos numericos: ");
                cpf = ReadLine();
            }

            return cpf;
        }

        private int RespostaFinal(int opcao)
        {
           
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
