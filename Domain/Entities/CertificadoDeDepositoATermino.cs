using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base;

namespace Domain.Entities
{
    public class CertificadoDeDepositoATermino : Entity<int>, IServicioFinanciero
    {

        public DateTime FechaDeTermino { get; set; }

        public DateTime FechaDeInicio { get; set; }

        public bool ConsignacionInicial = true;

        public const double MINIMOCONSIGNACION = 1000000;

        public double TasaInteres { get; set; }

        public double Saldo { get; protected set; }
        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }

        public CertificadoDeDepositoATermino()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }


        public void Consignar(double valor,string ciudad)
        {
            if (this.ConsignacionInicial == true)
            {
                if (valor >= MINIMOCONSIGNACION)
                {
                    NuevoMovimiento(valor, "Consignar");
                    this.ConsignacionInicial = false;
                    this.FechaDeInicio = DateTime.Now;

                }
                else
                {
                    throw new InvalidOperationException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 1 millon");
                }
            }
            else
            {
                throw new InvalidOperationException("Solo se puede consignar una vez");

            }

        }

        public void Retirar(double valor)
        {
            if (FechaDeTermino < DateTime.Now)
            {
                SaldoConIntereses();
                NuevoMovimiento(valor, "Retirar");
                Saldo -= valor;
            }
            else
            {
                throw new InvalidOperationException("No es posible realizar el Retiro, porque no se ha cumplido la fecha a termino");
            }
        }

        public double SaldoConIntereses()
        {
            TimeSpan meses = (FechaDeTermino - FechaDeInicio);
            int dias = (meses.Days / 30);
            double saldoConInteres = Saldo * (1 + TasaInteres * dias); //tasa de interes mensual simple
            Saldo = saldoConInteres;
            return saldoConInteres;
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, double valor, string ciudad)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor, ciudad);
        }

        public void NuevoMovimiento(double valor, string tipoMovimiento)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();

            switch (tipoMovimiento)
            {
                case "Consignar":
                    Saldo += valor;
                    movimiento.ValorConsignacion = valor;
                    break;
                case "Retirar":
                    Saldo -= valor;
                    movimiento.ValorRetiro = valor;
                    break;
            }

            movimiento.FechaMovimiento = DateTime.Now;

            this.Movimientos.Add(movimiento);

        }

    }
}