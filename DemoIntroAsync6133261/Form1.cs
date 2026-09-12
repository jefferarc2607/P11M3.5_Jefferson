using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoIntroAsync6133261
{
    public partial class Form1 : Form
    {
        HttpClient httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            var directorioActual = AppDomain.CurrentDomain.BaseDirectory;
            var directoriobasesecuencial = Path.Combine(directorioActual, @"Pictures\resultado-secuencial");
            var directoriobaseparalelo = Path.Combine(directorioActual, @"Pictures\resultado-paralelo");
            PrepararEjecucion(directoriobaseparalelo, directoriobasesecuencial);
            Console.WriteLine("Inicio");
            List<Imagen> imagenes = ObtenerImagenes();
            var sw = new Stopwatch();
            sw.Start();

            foreach (var imagen in imagenes)
            {
                await ProcesarImagen(directoriobasesecuencial, imagen);
            }

            Console.WriteLine("Secuencial - duracion en segundos {0}", sw.ElapsedMilliseconds / 1000.0);
            sw.Reset();
            sw.Start();

            var tareasEnumerable = imagenes.Select(async imagen =>
            {
                await ProcesarImagen(directoriobaseparalelo, imagen);
            }
            );

            await Task.WhenAll(tareasEnumerable);
            Console.WriteLine("Paralelo - duracion en segundos {0}", sw.ElapsedMilliseconds / 1000.0);
            sw.Stop();
            pictureBox1.Visible = false;
        }

        private async Task ProcesarImagen(string directorio, Imagen imagen)
        {
            var respuesta = await httpClient.GetAsync(imagen.URL);
            var contenido = await respuesta.Content.ReadAsByteArrayAsync();
            Bitmap bitmap;
            using (var ms = new MemoryStream(contenido))
            {
                bitmap = new Bitmap(ms);
            }

            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            var destino = Path.Combine(directorio, imagen.Nombre);
            bitmap.Save(destino);
        }

        private static List<Imagen> ObtenerImagenes()
        {
            var imagenes = new List<Imagen>();
            for (int i = 0; i <= 7; i++)
            {
                imagenes.Add(
                    new Imagen()
                    {
                        Nombre = $"Lisa {i}.png",
                        URL = $"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9xdp5eROoG5rVfCYnkD_xMRHIzU32TzrbEvBHYtHC9am2-sAkkO891zo&s=10{i}"
                    });
                imagenes.Add(
                    new Imagen()
                    {
                        Nombre = $"Jennie {i}.jpg",
                        URL = $"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrr5-RfGkmKU3qH-vyV1f_3px7dLLAeb9hHPT6hBadOg&s=10{i}"
                    });
                imagenes.Add(
                    new Imagen()
                    {
                        Nombre = $"Olise {i}.jpg",
                        URL = $"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmkyNKUi-jVt4-k9klHWlfMpSJDS1x59HbqGQZ6b4SSw&s=10{i}"
                    });
            }
            return imagenes;
        }

        private void BorrarArchivos(string directorio)
        {
            var archivos = Directory.EnumerateFiles(directorio);
            foreach (var archivo in archivos)
            {
                File.Delete(archivo);
            }
        }

        private void PrepararEjecucion(string destinobaseparalelo, string destinobasesecuencial)
        {
            if (!Directory.Exists(destinobaseparalelo))
            {
                Directory.CreateDirectory(destinobaseparalelo);
            }

            if (!Directory.Exists(destinobasesecuencial))
            {
                Directory.CreateDirectory(destinobasesecuencial);
            }

            BorrarArchivos(destinobasesecuencial);
            BorrarArchivos(destinobaseparalelo);
        }

        private async Task<string> ProcesamientoLargo()
        {
            await Task.Delay(5000);
            return "Jeffer";
        }

        private async Task RealizarProcesamientoLargoA()
        {
            await Task.Delay(1000);
            Console.WriteLine("Procesamiento A ha finalizado");
        }

        private async Task RealizarProcesamientoLargoB()
        {
            await Task.Delay(1000);
            Console.WriteLine("Procesamiento B ha finalizado");
        }

        private async Task RealizarProcesamientoLargoC()
        {
            await Task.Delay(1000);
            Console.WriteLine("Procesamiento C ha finalizado");
        }
    }
}
