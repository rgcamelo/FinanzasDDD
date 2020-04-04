using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const double TOPERETIRO = 20000;
        public bool ConsignacionInicial = true;
        private const double MINIMO_CONSIGNACION = 50000;
        private int ContadorRetiroMes = 0;



        public override void Consignar(double valor, string ciudad)
        {
            bool mayoryDiferenteDeZero = ValidadValorMayorYDiferenteZero(valor);
            bool valorMinimoConsignacion = ValorMinimoConsignacion(valor,MINIMO_CONSIGNACION);
            bool mismaCiudad = MismaCiudad(ciudad, valor);

            if (!mayoryDiferenteDeZero)
            {
                throw new InvalidOperationException("Valor Invalido");
            }
            else
            {
                if (ConsignacionInicial)
                {
                    if (mismaCiudad)
                    {
                        if (valorMinimoConsignacion)
                        {
                            
                            NuevoMovimiento(valor, "Consignar");
                            this.ConsignacionInicial = false;
                        }
                        else
                        {
                            throw new InvalidOperationException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
                        }
                    }
                    else
                    {
                        if (valorMinimoConsignacion)
                        {
                            if (FondosSuficientes(valor, 10000))
                            {
                                CostoMovimiento(10000);
                                NuevoMovimiento(valor, "Consignar");
                            }
                            else
                            {
                                throw new InvalidOperationException("Fondos insuficientes");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
                        }
                        
                        
                    }
                    
                }
                else
                {
                    if (mismaCiudad)
                    {
                        NuevoMovimiento(valor, "Consignar");
                    }
                    else
                    {
                        if (FondosSuficientes(valor,10000))
                        {
                            CostoMovimiento(10000);
                            NuevoMovimiento(valor, "Consignar");
                        }
                        else
                        {
                            throw new InvalidOperationException("Fondos insuficientes");
                        }
                    }
                    

                }
            }

            
        }

        

        public bool MismaCiudad( string ciudad,double valor)
        {
            if ( this.Ciudad == ciudad)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }

        //////////

        public override void Retirar(double valor)
        {
            bool mayoryDiferenteDeZero = ValidadValorMayorYDiferenteZero(valor);

            if (!mayoryDiferenteDeZero)
            {
                throw new InvalidOperationException("Valor Invalido");
            }
            else
            {
                if (RetirosSinCosto())
                {
                    if (SaldoMinimoAlRetirar(valor,TOPERETIRO))
                    {
                        NuevoMovimiento(valor, "Retirar");
                        ContadorRetiroMes++;
                    }
                    else
                    {
                        throw new InvalidOperationException("No es Posible Retirar, El saldo minimo deber ser 20000 ");
                    }
                }
                else
                {
                    if (SaldoMinimoAlRetirar(valor,TOPERETIRO))
                    {
                        CostoMovimiento(5000);
                        NuevoMovimiento(valor, "Retirar");
                    }
                    else
                    {
                        throw new InvalidOperationException("No es Posible Retirar, El saldo minimo deber ser 20000 ");
                    }
                }
            }
        }

        
        public bool RetirosSinCosto()
        {
            if(this.ContadorRetiroMes < 3)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        
    }


}
