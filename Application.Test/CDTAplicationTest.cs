using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Application.Test
{
    class CDTAplicationTest
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
        public void CrearCDTTest()
        {
            var FechaDeInicio = new DateTime(2020, 2, 4);
            var FechaDeTermino = new DateTime(2020, 3, 4); //Año Mes Dia
            var request = new CrearDepositoATerminoRequest { Numero = "1111", Nombre = "aaaaa", FechaDeInicio = FechaDeInicio, FechaDeTermino = FechaDeTermino, TasaInteres = 0.2, Ciudad = "Valledupar" };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con exito el deposito 1111.", response.Mensaje);
        }

        [Test]
        public void ConsignarCDTTest()
        {
            var FechaDeInicio = new DateTime(2020, 2, 4);
            var FechaDeTermino = new DateTime(2020, 3, 4); //Año Mes Dia
            var request = new CrearDepositoATerminoRequest { Numero = "1111", Nombre = "aaaaa", FechaDeInicio = FechaDeInicio, FechaDeTermino = FechaDeTermino, TasaInteres = 0.2, Ciudad = "Valledupar" };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            var request1 = new ConsignarRequest { Numero = "1111", Valor = 1000000, Ciudad="Valledupar" };
            ConsignarService _service1 = new ConsignarService(new UnitOfWork(_context));
            var response2 = _service1.Ejecutar(request1, "CDT");
            Assert.AreEqual("Su Nuevo saldo es 1000000.", response2.Mensaje);
        }


    }
}
