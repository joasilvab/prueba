using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilidades
{
    static public class ControlesTextBox
    {
        static public void tbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = sender as TextBox;
                string precio = tb.Text;
                if (e.KeyChar == '.' || e.KeyChar == ',')
                    if (precio.Contains(','))
                        e.Handled = true;
                    else
                        e.KeyChar = ',';
                else
                    if (!char.IsDigit(e.KeyChar))
                    {
                        if (e.KeyChar != (char)Keys.Back)
                            e.Handled = true;
                    }
                    else
                    {
                        int indexComa = precio.IndexOf(',');
                        if (indexComa != -1 && tb.SelectionStart > indexComa)
                        {
                            if (precio.Substring(precio.IndexOf(',')).Count() == 3)
                                e.Handled = true;
                        }
                    }
            }
        }
    }
}
