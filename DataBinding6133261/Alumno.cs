using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataBinding6133261
{
    public class Alumno : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string nombre;

        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
                OnPropertyChanged(nameof(MostrarNombre));
            }
        }

        public string MostrarNombre => $"Hola bienvenido: {Nombre}";

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
