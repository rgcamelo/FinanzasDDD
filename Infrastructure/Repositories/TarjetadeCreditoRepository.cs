using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TarjetadeCreditoRepository : GenericRepository<Tarjeta_de_Credito>, ITarjetaCreditoRepository
    {
        public TarjetadeCreditoRepository(IDbContext context)
              : base(context)
        {

        }
    }
}
