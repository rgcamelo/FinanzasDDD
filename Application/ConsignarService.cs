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


        public ConsignarResponse Ejecutar(ConsignarRequest request,string tipo)
        {
            switch (tipo)
            {
                case "CuentaBancaria":
                    var cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Consignar(cuenta, request);
                   
                case "CDT":
                    var cdt = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Consignar(cdt, request);

                case "TarjetaCredito":
                    var tarjetaCredito = _unitOfWork.TarjetaCreditoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Consignar(tarjetaCredito, request);
                default:
                    return new ConsignarResponse() { Mensaje = "Tipo Financiero No Válido." };
            }
            
            
        }

        public ConsignarResponse Consignar(IServicioFinanciero servicioFinanciero, ConsignarRequest request)
        {
            if (servicioFinanciero != null)
            {
                servicioFinanciero.Consignar(request.Valor, request.Ciudad);
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
        public string Numero { get; set; }
        public double Valor { get; set; }
        public string Ciudad { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
