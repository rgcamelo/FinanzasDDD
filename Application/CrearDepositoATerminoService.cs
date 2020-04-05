using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;

namespace Application
{
    public class CrearDepositoATerminoService
    {
        readonly IUnitOfWork _unitOfWork;

        public CrearDepositoATerminoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearDepositoATerminoResponse Ejecutar(CrearDepositoATerminoRequest request)
        {
            CertificadoDeDepositoATermino cdt = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.Numero);
            if (cdt == null)
            {
                CertificadoDeDepositoATermino cdtNuevo = new CertificadoDeDepositoATermino();  
                cdtNuevo.Nombre = request.Nombre;
                cdtNuevo.Numero = request.Numero;
                _unitOfWork.DepositoATerminoRepository.Add(cdtNuevo);
                _unitOfWork.Commit();
                return new CrearDepositoATerminoResponse() { Mensaje = $"Se creó con exito el deposito {cdtNuevo.Numero}." };
            }
            else
            {
                return new CrearDepositoATerminoResponse() { Mensaje = $"El número de deposito ya exite" };
            }
        }

       


    }

    public class CrearDepositoATerminoRequest
    {
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public DateTime FechaDeTermino { get; set; }

        public DateTime FechaDeInicio { get; set; }
        public double TasaInteres { get; set; }
    }

    public class CrearDepositoATerminoResponse
    {
        public string Mensaje { get; set; }
    }

}

