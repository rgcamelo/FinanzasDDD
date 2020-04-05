using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;


namespace Application
{
    public class CrearTarjetaCreditoService
    {
        readonly IUnitOfWork _unitOfWork;

        public CrearTarjetaCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearTarjetaCreditoResponse Ejecutar(CrearTarjetaCreditoRequest request)
        {
            Tarjeta_de_Credito tarjetaDeCredito = _unitOfWork.TarjetaCreditoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
            if (tarjetaDeCredito == null)
            {
                Tarjeta_de_Credito tarjetaNueva = new Tarjeta_de_Credito();
                tarjetaNueva.Nombre = request.Nombre;
                tarjetaNueva.Numero = request.Numero;
                _unitOfWork.TarjetaCreditoRepository.Add(tarjetaNueva);
                _unitOfWork.Commit();
                return new CrearTarjetaCreditoResponse() { Mensaje = $"Se creó con exito la Tarjeta {tarjetaNueva.Numero}." };
            }
            else
            {
                return new CrearTarjetaCreditoResponse() { Mensaje = $"El número de deposito ya exite" };
            }
        }

        
    }

    public class CrearTarjetaCreditoRequest
    {
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Cupo { get; set; }
    }

    public class CrearTarjetaCreditoResponse
    {
        public string Mensaje { get; set; }
    }
}
