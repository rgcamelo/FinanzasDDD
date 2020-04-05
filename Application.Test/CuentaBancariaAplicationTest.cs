using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Application.Test
{
    public class Tests
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
        public void CrearCuentaBancariaAhorroTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1111", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad="Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito la cuenta 1111.", response.Mensaje);
        }

        [Test]
        public void CrearCuentaBancariaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1112", Nombre = "aaaaa", TipoCuenta = "Corriente", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito la cuenta 1112.", response.Mensaje);
        }

        [Test]
        public void CrearCuentaBancariaTipoCuentaInvalido()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1113", Nombre = "aaaaa", TipoCuenta = "Invalido", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));


            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.Ejecutar(request));
            Assert.AreEqual(ex.Message, "Tipo de Cuenta No Valido");
        }

        [Test]
        public void CrearCuentaNumeroInvalidoTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1114", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            var response1 = _service.Ejecutar(request);
            Assert.AreEqual("El número de cuenta ya exite",response1.Mensaje);
        }

        [Test]
        public void ConsignarCuentaBancaria()
        {
            var cuenta = new CrearCuentaBancariaRequest { Numero = "1215", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response1 = _service.Ejecutar(cuenta);
            var request = new ConsignarRequest { Numero = "1215", Valor = 100000, Ciudad="Valledupar" };
            ConsignarService _service1 = new ConsignarService(new UnitOfWork(_context));
            var response = _service1.Ejecutar(request, "CuentaBancaria");
            Assert.AreEqual("Su Nuevo saldo es 100000.", response.Mensaje);
        }

        [Test]
        public void RetirarCuentaBancaria()
        {
            var cuenta = new CrearCuentaBancariaRequest { Numero = "1116", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response1 = _service.Ejecutar(cuenta);

            var request = new ConsignarRequest { Numero = "1116", Valor = 100000, Ciudad="Valledupar" };
            ConsignarService _service1 = new ConsignarService(new UnitOfWork(_context));
            var response = _service1.Ejecutar(request, "CuentaBancaria");

            var request1 = new RetirarRequest { Numero = "1116", Valor = 10000 };
            RetirarServices retirarServices = new RetirarServices(new UnitOfWork(_context));
            var response2 = retirarServices.Ejecutar(request1, "CuentaBancaria");
            Assert.AreEqual("Su Nuevo saldo es 90000.", response2.Mensaje);
        }




    }
}