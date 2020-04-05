using Domain.Contracts;
using Domain.Entities;
using Domain.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearCuentaBancariaService 
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IGenericFactory<CuentaBancaria> _genericFactory;
        
        public CrearCuentaBancariaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _genericFactory = new CuentaBancariaFactory(); 
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            CuentaBancaria cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {

                CuentaBancaria cuentaNueva = new CuentaBancariaFactory().CreateEntity(request.TipoCuenta);
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                cuentaNueva.Ciudad = request.Ciudad;
                _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero}."};
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }



    }
    public class CrearCuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public string TipoCuenta { get; set ; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
    }
    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}
