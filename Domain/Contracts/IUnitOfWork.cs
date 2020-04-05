using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICuentaBancariaRepository CuentaBancariaRepository { get; }
        IDepositoATerminoRepository DepositoATerminoRepository { get; }
        ITarjetaCreditoRepository TarjetaCreditoRepository { get; }
        int Commit();
    }
}
