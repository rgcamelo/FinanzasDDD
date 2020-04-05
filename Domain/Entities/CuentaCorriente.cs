using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : CuentaBancaria
    {
        public double SOBREGIRO = 10000;
        public bool ConsignacionInicial = true;
        private const double MINIMO_CONSIGNACION = 100000;

        public override void Consignar(double valor, string ciudad)
        {
            bool validoMayorZero = ValidadValorMayorYDiferenteZero(valor);

            if (validoMayorZero)
            {
                if (ConsignacionInicial)
                {
                    if (ValorMinimoConsignacion(valor, MINIMO_CONSIGNACION))
                    {
                        this.ConsignacionInicial = false;
                        NuevoMovimiento(valor, "Consignar");
                    }
                    else
                    {
                        throw new InvalidOperationException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 100000");
                    }
                }
                else
                {
                    NuevoMovimiento(valor, "Consignar");
                }

            }
            else
            {
                throw new InvalidOperationException("Valor Invalido");
            }


        }

        public override void Retirar(double valor)
        {

            bool valorValido = ValidadValorMayorYDiferenteZero(valor);

            if (valorValido)
            {
                if (SaldoMinimoAlRetirar(valor, SOBREGIRO + 4000))
                {
                    CostoMovimiento(4000);
                    NuevoMovimiento(valor, "Retirar");
                }
                else
                {
                    throw new InvalidOperationException("Fondos insuficientes");
                }
            }
            else
            {
                throw new InvalidOperationException("Valor Invalido");
            }
        }
    }

}
