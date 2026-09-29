using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista_Seguridad
{
    public partial class FrmPeliculas : Form
    {
        public FrmPeliculas()
        {
            InitializeComponent();
            navegador1.NavegadorMetConfigurar("Peliculas", 4, 16);
            SeguridadMetInterceptarAyuda();
        }

        private void SeguridadMetInterceptarAyuda()
        {
            var btn = navegador1.Controls.Find("NavegadorBtnAyuda", true).FirstOrDefault();
            if (btn == null) return;

            var fi = typeof(Component).GetField("events", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventList = fi?.GetValue(btn) as EventHandlerList;
            var clickKey = typeof(Control).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static)
                                          ?.GetValue(null);

            if (eventList != null && clickKey != null)
                eventList.RemoveHandler(clickKey, eventList[clickKey]);

            btn.Click += (s, ev) =>
                System.Diagnostics.Process.Start(
                    @"C:\SeguridadAyudas\SeguridadAyudas.chm");
        }
    }
}
