using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Tarjeta_de_Credito: Entity<int>, IServicioFinanciero
    {
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Saldo { get; set; }
        public double Cupo { get; set; }

        public void Consignar(double valor, string ciudad)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException("No es posible realizar la consignacion, Valor Invalido");
            }
            else
            {
                if( valor <= Saldo)
                {
                    Cupo += valor;
                    Saldo -= valor;
                }
                else
                {
                    throw new InvalidOperationException("No es posible realizar la consignacion, Maximo Saldo Excedido");
                }
            }
        }
        public void Retirar(double valor)
        {
            if(valor <= 0)
            {
                throw new InvalidOperationException("No es posible realizar el retiro, Valor Invalido");
            }
            else
            {
                if( valor <= Cupo)
                {
                    Cupo -= valor;
                }
                else
                {
                    throw new InvalidOperationException("No es posible realizar el retiro, Maximo Cupo Excedido");
                }
            }
        }


    }
}
