using Domain.Entities;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Factories
{
    public class CuentaBancariaFactory : IGenericFactory<CuentaBancaria>
    {
        public CuentaBancaria CreateEntity(string type)
        {
            switch (type)
            {
                case "Ahorro":
                    return new CuentaAhorro();
                case "Corriente":
                    return new CuentaCorriente();
                default:
                    throw new ArgumentOutOfRangeException(message: "Tipo de Cuenta No Valido", innerException: null);
            }
        }
    }


}
