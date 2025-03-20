using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancariaGeen.Utils
{
    public static class ValidaInput
    {
        public static int ValidaInt(string entrada)
        {
            int numero;
            while (true)
            {
                if (int.TryParse(entrada, out numero))
                    return numero;

                Console.WriteLine("Entrada inválida! Digite um número válido.\n");

                Console.Write("Digite um número: ");
                entrada = Console.ReadLine();
            }
        }

        public static decimal ValidaDecimal(string entrada)
        {
            decimal numero;
            while (true)
            {        
                if (decimal.TryParse(entrada, out numero))
                {
                    if(numero < 0)
                    {
                        Console.WriteLine("O valor não pode ser negativo! Digite um valor positivo.");
                        Console.Write("Digite o novo valor: ");
                        entrada = Console.ReadLine();
                        continue;
                    }

                    return numero;
                }

                Console.WriteLine("Entrada inválida! Digite um valor decimal.\n");

                Console.Write("Digite o valor: ");
                entrada = Console.ReadLine();
            }
        }


        public static string ValidaString(string entrada)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(entrada))
                    return entrada;

                Console.WriteLine("Entrada inválida! O texto não pode estar vazio.");

                Console.Write("Digite um texto: ");
                entrada = Console.ReadLine();
            }
        }



    }
}
