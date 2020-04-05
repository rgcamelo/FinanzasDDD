﻿using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using System;

namespace Application
{
    public class ConsignarService 
    {
        readonly IUnitOfWork _unitOfWork;
        
        public ConsignarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Ejecutar(ConsignarRequest request,string tipo)
        {
            switch (tipo)
            {
                case "CuentaBancaria":
                    var cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
                    Consignar(cuenta, request.Valor);
                    break;
                case "CDT":
                    var cdt = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
                    Consignar(cdt, request.Valor);
                    break;
                case "TarjetaCredito":
                    break;
                default:
                    throw new ArgumentOutOfRangeException(message: "Tipo de Servicio Financiero No Valido", innerException: null);
            }
            
            
        }

        public ConsignarResponse Consignar(IServicioFinanciero servicioFinanciero, double valor)
        {
            if (servicioFinanciero != null)
            {
                servicioFinanciero.Consignar(valor, "Valledupar");
                _unitOfWork.Commit();
                return new ConsignarResponse() { Mensaje = $"Su Nuevo saldo es {servicioFinanciero.Saldo}." };
            }
            else
            {
                return new ConsignarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class ConsignarRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }
        public string Ciudad { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
