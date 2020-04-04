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
        public void ConsginacionConValor0()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(0, "bogota"));
            Assert.AreEqual(ex.Message, "Valor Invalido");
        }

        [Test]
        public void ConsginacionConValorNegativo()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(-1000, "bogota"));
            Assert.AreEqual(ex.Message, "Valor Invalido");
        }

        [Test]
        public void CuentaAhorroPrimeraConsignacionIgual50000DiferenteCiudad()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(50000,"bogota");

            Assert.AreEqual(40000, cuenta.Saldo);
        }

        [Test]
        public void CuentaAhorroPrimeraConsignacionIgual50000MismaCiudad()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(50000, "Valledupar");

            Assert.AreEqual(50000, cuenta.Saldo);
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

        [Test]
        public void CuentaAhorroConsignacionOtraCiudadFondosInsuficientes()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 1000;
            cuenta.ConsignacionInicial = false;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(1000, "bogota"));
            Assert.AreEqual(ex.Message, "Fondos insuficientes");
        }

        [Test]
        public void CuentaAhorroSegundaConsignacionMayor50000DiferenteCiudad()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(60000, "Valledupar");
            cuenta.Consignar(60000, "bogota");

            Assert.AreEqual(110000, cuenta.Saldo);
        }

        [Test]
        public void CuentaAhorroSegundaConsignacionMayor50000MismaCiudad()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(60000, "Valledupar");
            cuenta.Consignar(60000, "Valledupar");

            Assert.AreEqual(120000, cuenta.Saldo);
        }

        [Test]
        public void CuentaAhorroRetirarValorInvalido()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(-10000));
            Assert.AreEqual(ex.Message, "Valor Invalido");
        }

        [Test]
        public void PrimerRetirarSinCosto()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 35000;
            cuenta.Retirar(1000);

            Assert.AreEqual(34000, cuenta.Saldo);

        }

        [Test]
        public void SegundoRetirarSinCosto()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 35000;
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);


            Assert.AreEqual(33000, cuenta.Saldo);

        }

        [Test]
        public void TercerRetirarSinCosto()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 35000;
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);

            Assert.AreEqual(32000, cuenta.Saldo);

        }

        [Test]
        public void PrimerRetiroConCosto()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 35000;
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);
            cuenta.Retirar(1000);

            Assert.AreEqual(26000, cuenta.Saldo);

        }

        [Test]
        public void RetirarTopedeRetiro()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Saldo = 20000;

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(10000));
            Assert.AreEqual(ex.Message, "No es Posible Retirar, El saldo minimo deber ser 20000");

        }

        // Pruebas Cuenta Corriente

        [Test]
        public void CuentaCorrienteConsignacionValorValido()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";
            

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(0, "bogota"));
            Assert.AreEqual(ex.Message, "Valor Invalido");
        }

        [Test]
        public void CuentaCorrientePrimeraConsignacionValorValido()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(100000, "bogota");

            Assert.AreEqual(100000, cuenta.Saldo);
        }

        [Test]
        public void CuentaCorrientePrimeraConsignacionValorInvalido()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(90000, "bogota"));
            Assert.AreEqual(ex.Message, "No es posible realizar la consignacion, la primera consignacion debe ser mayor a 100000");
        }

        [Test]
        public void CuentaCorrienteSegundaConsignacionValorValido()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(100000, "bogota");
            cuenta.Consignar(20000, "bogota");

            Assert.AreEqual(120000, cuenta.Saldo);
        }

        [Test]
        public void CuentaCorrienteRetirarValorInvalido()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Consignar(-10000, "bogota"));
            Assert.AreEqual(ex.Message, "Valor Invalido");
        }

        [Test]
        public void CuentaCorrienteRetirarFondosSuficientes()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.ConsignacionInicial = false;
            cuenta.Consignar(30000, "bogota");
            cuenta.Retirar(6000);

            Assert.AreEqual(20000, cuenta.Saldo);
        }

        [Test]
        public void CuentaCorrienteRetirarFondosInsuficientes()
        {
            CuentaCorriente cuenta = new CuentaCorriente();
            cuenta.Numero = "111";
            cuenta.Nombre = "Corriente Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(100000, "bogota");
            cuenta.Retirar(70000);

            
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(300000));
            Assert.AreEqual(ex.Message, "Fondos insuficientes");
        }



        [Test]
        public void ConsignarCertificadoDepositoTerminoTest()
        {
            var cuenta = new CertificadoDeDepositoATermino();
            cuenta.Numero = "111";
            cuenta.Nombre = "CDT Ejemplo";
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
            cuenta.Nombre = "CDT Ejemplo";
            cuenta.FechaDeInicio = new DateTime(2020, 1, 3);
            cuenta.FechaDeTermino = new DateTime(2020, 5, 4);
            cuenta.Consignar(1000000);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(10000));
            Assert.AreEqual(ex.Message, "No es posible realizar el Retiro, porque no se ha cumplido la fecha a termino");
            
        }
       

    }
}