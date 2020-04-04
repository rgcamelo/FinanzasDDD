using Domain.Entities;
using NUnit.Framework;
using System;

namespace Domain.Test
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CuentaAhorroPrimeraConsignacionIgual50000()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(50000,"bogota");

            Assert.AreEqual(40000, cuenta.Saldo);
        }


        [Test]
        public void CuentaAhorroPrimeraConsignacionMayor50000()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(60000, "bogota");

            Assert.AreEqual(50000, cuenta.Saldo);
        }


        [Test]
        public void CuentaAhorroPrimeraConsignacionMenor50000() 
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>( () => cuenta.Consignar(10000,"bogota"));
            Assert.AreEqual(ex.Message, "No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
        }

        

        //Segunda consignacion = 0;
        /*
        [Test]
        public void ConsignarCuentaCorrienteTest()
        {
            var cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Bogota";
            cuenta.Consignar(100000);
            Assert.AreEqual(100000, cuenta.Saldo);
        }

        [Test]
        public void ConsignarCertificadoDepositoTerminoTest()
        {
            var cuenta = new CertificadoDeDepositoATermino();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.FechaDeInicio = DateTime.Now; 
            cuenta.FechaDeTermino = new DateTime(2020, 3, 4); // A�o Mes Dia
            cuenta.Consignar(1000000);
            Assert.AreEqual(1000000, cuenta.Saldo);
        }

        [Test]
        public void RetirarCertificadoDepositoTerminoTest()
        {
            var cuenta = new CertificadoDeDepositoATermino();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.FechaDeInicio = new DateTime(2020, 1, 3);
            cuenta.FechaDeTermino = new DateTime(2020, 5, 4);
            cuenta.Consignar(1000000);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(10000));
            Assert.AreEqual(ex.Message, "No es posible realizar el Retiro, porque no se ha cumplido la fecha a termino");
            
        }
        */

    }
}