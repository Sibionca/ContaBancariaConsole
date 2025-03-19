using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancariaGeen.Models
{
    class Conta
    {
        private static int numeroContaIncrement = 1;
        public int Numero { get; set; }
        private decimal Saldo { get; set; }
        public Cliente Cliente { get; set; }

        public Conta(Cliente cliente)
        {
            Numero = numeroContaIncrement++;
            Saldo = 0;
            Cliente = cliente;
        }

        public void Depoisitar(decimal saldo)
        {
            Saldo += saldo;
        }

        public void Sacar(decimal saldo)
        {
            Saldo -= saldo;
        }
        public decimal Consultar()
        {
            return Saldo;
        }
        public void Transferir(decimal saldo, Conta destino)
        {
            Sacar(saldo);
            destino.Depoisitar(saldo);
        }
    }
}
