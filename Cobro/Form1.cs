using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cobro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha = dateTimePicker1.Value;
            label1.Text = fecha.ToShortDateString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha = dateTimePicker1.Value;

            var mesSeleccionado = fecha.Month;
            var anioSeleccionado = fecha.Year;
            var diaSeleccionado = fecha.Day;

            var diaUltimo = UltimoLunesDelMes(anioSeleccionado, mesSeleccionado);
            var nuevoDia = VerificarFin(fecha, diaSeleccionado);
           
            if (nuevoDia < diaUltimo){//si el dia cae antes de la ultima semana (no aplica si es fin de semana) la fecha de cobro es ese mismo dia
                label1.Text = " ";
                label2.Text = nuevoDia+"/"+mesSeleccionado+"/"+anioSeleccionado; //fecha con formato dd/mm/yyyy
            }
            else
            {
                //obtengo el primer dia del siguiente mes
                var m = fecha.AddMonths(1);
                DateTime primerDia = new DateTime(anioSeleccionado,m.Month, 1);
                //verifico si el primer dia del mes no es fin de semana
                var nuevaFecha = VerificarFin(primerDia, 1);
                label1.Text = " ";
                label2.Text = nuevaFecha+"/"+primerDia.Month+"/"+primerDia.Year;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //Calulo el Ultimo lunes del mes: retorna valor entero
        public int UltimoLunesDelMes(int anio, int mes)
        {
            var ulimoLunesDelMes = new DateTime(anio, mes, DateTime.DaysInMonth(anio, mes));

            //es un loop pequeño sin problemas de performance
            while (ulimoLunesDelMes.DayOfWeek != DayOfWeek.Monday)
                ulimoLunesDelMes = ulimoLunesDelMes.AddDays(-1);

            return ulimoLunesDelMes.Day;
        }

        public int VerificarFin(DateTime fecha, int diaSeleccionado)
        {           
            //primero verifico que la fecha seleccionada no sea un fin de semana
            if (fecha.DayOfWeek == DayOfWeek.Saturday)//si es sabado sumo dos dias para que la fecha de cobro sea el lunes
            {
                diaSeleccionado += 2;
            }
            else if (fecha.DayOfWeek == DayOfWeek.Sunday)//si es domingo solo sumo un dia mas para que el cobro sea el lunes
            {
                diaSeleccionado++;
            }
            return diaSeleccionado;
        }
    }
}
