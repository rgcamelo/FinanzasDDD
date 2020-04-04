using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class CuentaBancaria : Entity<int>, IServicioFinanciero
    {
        public CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Saldo { get; set; }
        public string Ciudad { get; set; }


        public abstract void Consignar(double valor, string ciudad);
        
        public abstract void Retirar(double valor);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, double valor,string ciudad)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor,ciudad);
        }

        public void NuevoMovimiento(double valor, string tipoMovimiento)
        {
            MovimientoFinanciero consignacion = new MovimientoFinanciero();

            switch (tipoMovimiento)
            {
                case "Consignar":
                    Saldo += valor;
                    consignacion.ValorConsignacion = valor;
                    break;
                case "Retirar":
                    Saldo -= valor;
                    consignacion.ValorRetiro = valor;
                    break;
            }

            consignacion.FechaMovimiento = DateTime.Now;

            this.Movimientos.Add(consignacion);
            
        }

        public bool ValorMinimoConsignacion(double valor, double minimo)
        {
            if (valor >= minimo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidadValorMayorYDiferenteZero(double valor)
        {
            if (valor > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaldoMinimoAlRetirar(double valor, double tope)
        {
            double nuevoSaldo = this.Saldo - valor;
            if (nuevoSaldo >= tope)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FondosSuficientes(double valor, double minimo)
        {
            var nuevoSaldo = Saldo + valor;
            if (nuevoSaldo >= minimo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void CostoMovimiento(double valor)
        {
            this.Saldo = Saldo - valor;
        }


    }
}
