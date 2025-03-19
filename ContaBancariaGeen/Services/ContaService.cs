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
    class ContaService
    {
        private readonly List<Conta> _contas = new List<Conta>();

        List<Conta> GetContas()
        {
            return _contas;
        }

        public Conta CadastrarConta(Cliente cliente)
        {
            var conta = new Conta(cliente);

            _contas.Add(conta);

            return conta;
        }

        private void ExecutaDeposito()
        {
            MostraContas("Para qual conta você deseja depositar?");

            var conta = ValidaConta();

            Write("Informe o valor a ser depositado: ");

            var entrada = ReadLine();
            var deposito = ValidaInput.ValidaDecimal(entrada);

            conta.Depoisitar(deposito);

            WriteLine("Deposito realizado com sucesso!!\n");
        }

        private void ExecutaSaque()
        {
            MostraContas("De qual conta você deseja sacar?");

            var conta = ValidaConta();

            if(conta.Consultar() == 0)
            {
                WriteLine("Não é possível realizar saque pois seu saldo atual é 0.");
                ReadKey();
                return;
            }

            WriteLine($"\nSeu saldo atual é de {conta.Consultar()}");
            Write("Qual valor que deseja sacar?: ");

            var entrada = ReadLine();
            var valor = ValidaInput.ValidaDecimal(entrada);

            if(valor > conta.Consultar())
            {
                WriteLine("O valor informado é maior que o saldo disponível em conta! Por favor escolha outra operação.");
                ReadKey();
                return;
            }

            conta.Sacar(valor);
            WriteLine($"Saque no valor de {valor} realizado com sucesso!!\n");
            ReadKey();
        }

        private void ExecutaConsulta()
        {
            MostraContas("De qual conta você deseja ver o saldo?");
            var conta = ValidaConta();


            WriteLine($"Seu saldo é de {conta.Consultar()}");
            ReadKey();
        }

        private void ExecutaTransferencia()
        {
            if(_contas.Count == 1)
            {
                WriteLine("É necessário ter mais de uma conta cadastrada para realizar transferências!");
                ReadKey();
                return;
            }

            var textoOrigem = "\nDe qual conta origem você deseja transferir?\n";
            MostraContas(textoOrigem);

            var contaOrigem = ValidaConta();

            if (contaOrigem.Consultar() == 0)
            {
                WriteLine("Não é possível realizar transferências pois seu saldo atual é 0.");
                ReadKey();
                return;
            }

            WriteLine($"\nSeu saldo atual é de {contaOrigem.Consultar()}");

            var textoDestino = "\nPara qual conta destino você deseja transferir?\n";
            MostraContas(textoDestino);

            var contaDestino = ValidaConta();

            if(contaOrigem.Numero == contaDestino.Numero)
            {
                WriteLine("Não é possível realizar transferências para a mesma conta.");
                ReadKey();
                return;
            }

            Write("\nQual valor que deseja transferir?: ");

            var entrada = ReadLine();
            var valor = ValidaInput.ValidaDecimal(entrada);

            if (valor > contaOrigem.Consultar())
            {
                WriteLine("O valor informado é maior que o saldo disponível em conta! Por favor escolha outra operação.");
                ReadKey();
                return;
            }

            contaOrigem.Transferir(valor, contaDestino);
            WriteLine($"\nTransferencia no valor de {valor} realizada com sucesso!!\n");
            ReadKey();
        }

        public void ServicosConta()
        {
            var condicao = true;

            while(condicao)
            {

                WriteLine("\n\nQual operações bancárias você deseja efetuar?: ");
                WriteLine("1 - Depósito");
                WriteLine("2 - Saque");
                WriteLine("3 - Consultar saldo");
                WriteLine("4 - Transferir");
                WriteLine("5 - Voltar ao menu");
                Write("\nDigite a opção: \n");

                var entrada = ReadLine();
                var resposta = ValidaInput.ValidaInt(entrada);

                switch(resposta)
                {
                    case 1:
                        ExecutaDeposito();
                        break;
                    case 2:
                        ExecutaSaque();
                        break;
                    case 3:
                        ExecutaConsulta();
                        break;
                    case 4:
                        ExecutaTransferencia();
                        break;
                    case 5:
                        condicao = false;
                        WriteLine("Retornando ao menu....");
                        ReadKey();
                        break;
                    default:
                        WriteLine("Opa, você escolheu uma opção errada!");
                        break;
                }
            }
        }

        private void MostraContas(string msg)
        {
            WriteLine(msg);
            foreach (var conta in _contas)
            {
                WriteLine($"Conta Nº {conta.Numero} do Sr.(a) {conta.Cliente.Nome}");
            }
        }

        private Conta ValidaConta()
        {
            Write("Informe o número da conta desejada: ");
            
            var entrada = ReadLine();
            var numero = ValidaInput.ValidaInt(entrada);

            while (!_contas.Any(c => c.Numero == numero))
            {
                WriteLine("\nO numéro da conta informado não existe! Informe um número válido.");
                Write("\nInforme o número da conta desejada: ");

                entrada = ReadLine();
                numero = ValidaInput.ValidaInt(entrada);
            }

            var conta = _contas.First(c => c.Numero == numero);
            return conta;
        }
    }
}
