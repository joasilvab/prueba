using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Formularios
{
    public delegate void StatusLabelChangedEventHandler(object sender, StatusLabelArgs e);

    public class StatusLabelArgs : EventArgs
    {
        public string Texto { get; set; }
        public Color Color { get; set; }
        public bool AgregarHora { get; set; }
    }

    public class ContenidoBase: UserControl
    {
        //string _statusText;
        //public string StatusLabelText 
        //{
        //    get
        //    {
        //        return _statusText;
        //    }
        //    set 
        //    {
        //        _statusText = value;
        //        StatusLabelArgs sla = new StatusLabelArgs();
        //        sla.Texto = _statusText;
        //        StatusLabelChanged(this, sla);
        //    } 
        //}

        //Color _statusColor;

        //public Color StatusLabelColor
        //{
        //    get
        //    {
        //        return _statusColor;
        //    }
        //    set
        //    {
        //        _statusColor = value;
        //        StatusLabelArgs sla = new StatusLabelArgs();
        //    }
        //}

        public void SetStatusText(string texto, Color color, bool agregarHora)
        {
            StatusLabelArgs sla = new StatusLabelArgs();
            sla.Color = color;
            sla.Texto = texto;
            sla.AgregarHora = agregarHora;
            StatusLabelChanged(this, sla);
        }
        public StatusLabelChangedEventHandler StatusLabelChanged;
    }
}
