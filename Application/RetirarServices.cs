using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using System;


namespace Application
{
    public class RetirarServices
    {
        readonly IUnitOfWork _unitOfWork;

        public RetirarServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public RetirarResponse Ejecutar(RetirarRequest request, string tipo)
        {
            switch (tipo)
            {
                case "CuentaBancaria":
                    var cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Retirar(cuenta, request);

                case "CDT":
                    var cdt = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Retirar(cdt, request);

                case "TarjetaCredito":
                    var tarjetaCredito = _unitOfWork.TarjetaCreditoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
                    return Retirar(tarjetaCredito, request);
                default:
                    return new RetirarResponse() { Mensaje = "Tipo Financiero No Válido." };
            }


        }

        public RetirarResponse Retirar(IServicioFinanciero servicioFinanciero, RetirarRequest request)
        {
            if (servicioFinanciero != null)
            {
                servicioFinanciero.Retirar(request.Valor);
                _unitOfWork.Commit();
                return new RetirarResponse() { Mensaje = $"Su Nuevo saldo es {servicioFinanciero.Saldo}." };
            }
            else
            {
                return new RetirarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }


    }

    public class RetirarRequest
    {
        public string Numero { get; set; }
        public double Valor { get; set; }
    }
    public class RetirarResponse
    {
        public string Mensaje { get; set; }
    }
}
