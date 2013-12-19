using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Formularios
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool cerrarSesion = true;
            while (cerrarSesion)
            {
                FormLogin fl = new FormLogin();
                cerrarSesion = false;
                FormPrincipal fp;
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    fp = new FormPrincipal(fl.Conexion,fl.Usuario);
                    Application.Run(fp);
                    cerrarSesion = fp.CerrarSesion;
                }
            }
        }
    }
}
