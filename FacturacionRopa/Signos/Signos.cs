using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utilidades
{
    public enum Signos { Mayor, Menor, Igual, MayorIgual, MenorIgual, Distinto }

    public static class Colores
    {
        public static Color Error
        {
            get
            {
                return Color.Red;
            }
        }

        public static Color Normal
        {
            get
            {
                return Color.Black;
            }
        }

        public static Color Correcto
        {
            get
            {
                return Color.Green;
            }
        }
    }
}
