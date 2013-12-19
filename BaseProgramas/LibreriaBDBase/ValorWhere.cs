using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades;

namespace LibreriaBD
{
    public class ValorWhere
    {
        //public enum Signos { Igual, MenorIgual, MayorIgual, Distinto, Menor, Mayor }
        public string ValorIzquierda { get; set; }
        public bool ValorIzquierdaComillas { get; set; }
        public string ValorDerecha { get; set; }
        public bool ValorDerechaComillas { get; set; }
        public string Signo { get; set; }
        public ValorWhere(string vIzq, bool IzqComillas, string vDer, bool DerComillas, Signos signo)
        {
            ValorIzquierda = vIzq;
            ValorDerecha = vDer;
            ValorDerechaComillas = DerComillas;
            ValorIzquierdaComillas = IzqComillas;
            switch (signo)
            {
                case Signos.Distinto:
                    Signo = "<>";
                    break;
                case Signos.Igual:
                    Signo = "=";
                    break;    
                case Signos.Mayor:
                    Signo = ">";
                    break;
                case Signos.MayorIgual:
                    Signo = ">=";
                    break;
                case Signos.Menor:
                    Signo = "<";
                    break;
                case Signos.MenorIgual:
                    Signo = "<=";
                    break;
            }
        }
    }
}
