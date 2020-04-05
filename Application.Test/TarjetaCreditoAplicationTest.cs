using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using Application;

namespace Application.Test
{
    class TarjetaCreditoAplicationTest
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

            _context = new BancoContext(optionsInMemory);
        }

        [Test]
        public void CrearTarjetaCreditoTest()
        {
            var request = new CrearTarjetaCreditoRequest { Numero = "1111", Nombre = "TarjetaPrueba", Cupo = 100000 };
            CrearTarjetaCreditoService _service = new CrearTarjetaCreditoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito la Tarjeta 1111.", response.Mensaje);
        }

        /*
        [Test]
        public void ConsignarTarjetadeCredito()
        {
            var request0 = new CrearTarjetaCreditoRequest { Numero = "1111", Nombre = "TarjetaPrueba", Cupo = 100000 };
            CrearTarjetaCreditoService _service = new CrearTarjetaCreditoService(new UnitOfWork(_context));
            var response1 = _service.Ejecutar(request0);
            var request1 = new ConsignarRequest { Numero = "1111", Valor = 10000 };
            ConsignarService _service1 = new ConsignarService(new UnitOfWork(_context));
            var response = _service1.Ejecutar(request1, "TarjetaCredito");
            Assert.AreEqual("Su Nuevo saldo es 100000.", response.Mensaje);
        }*/


    }
}
