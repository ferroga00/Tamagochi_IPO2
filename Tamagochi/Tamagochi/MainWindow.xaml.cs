using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tamagochi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool hambriento = false;
        bool enfadado = false;
        bool cansado = false;
        bool entrenando = false;
        DispatcherTimer t1;
        DispatcherTimer t2;
        double variacion = 5.0;
        int tiempoJuego = 0;
        int contadorClics = 0;
        int contadorEntrenamientos = 0;
        int ticksEntrenando = 0;
        public MainWindow()
        {
            InitializeComponent();
            VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();
            t1 = new DispatcherTimer();
            t2 = new DispatcherTimer();
            t1.Start();
            t2.Start();
            t1.Interval = TimeSpan.FromMilliseconds(1000.0);
            t2.Interval = TimeSpan.FromMilliseconds(1000.0);
            t1.Tick += new EventHandler(reloj);
            t2.Tick += new EventHandler(cambiarLogros);

        }

        private void btnComer_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sbhambre = (Storyboard)this.Resources["hambre"];
            Storyboard sbComer = (Storyboard)this.Resources["moverBoca"];
            deshabilitarBotones(sender, e);
            this.contadorClics += 1;
            this.pbEnergia.Value += 30;
           if (this.pbEnergia.Value >= 40)
            {
                this.pbEnergia.Foreground = Brushes.Green;
                hambriento = false;
                sbhambre.Stop();
            }
            else if (this.pbEnergia.Value <= 40)
            {
                this.pbEnergia.Foreground = Brushes.Red;
            }
            sbComer.Completed += new EventHandler(finAnimaciones);
            sbComer.Begin();
            

        }

        private void btnDormir_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sbDormir = (Storyboard)this.Resources["cerrarOjos"];
            Storyboard sbCansancio = (Storyboard)this.Resources["cansancio"];
            deshabilitarBotones(sender, e);
            this.contadorClics += 1;
            this.pbCansancio.Value += 20;
            if (this.pbCansancio.Value >= 40)
            {
                this.pbCansancio.Foreground = Brushes.Green;
                cansado = false;
                sbCansancio.Stop();
            }
            else if (this.pbCansancio.Value <= 40)
            {
                this.pbCansancio.Foreground = Brushes.Red;
            }
            sbDormir.Completed += new EventHandler(finAnimaciones);
            sbDormir.Begin();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sbJugar = (Storyboard)this.Resources["moverBrazos"];
            Storyboard sbEnfadado = (Storyboard)this.Resources["enfadado"];
            deshabilitarBotones(sender, e);
            this.contadorClics += 1;
            
            this.pbDiversion.Value += 20;
            if (this.pbDiversion.Value >= 40)
            {
                this.pbDiversion.Foreground = Brushes.Green;
                enfadado = false;
                sbEnfadado.Stop();
                enfado.Visibility = Visibility.Hidden;

            }
            else if (this.pbDiversion.Value <= 40)
            {
                this.pbDiversion.Foreground = Brushes.Red;
            }
            sbJugar.Completed += new EventHandler(finAnimaciones);
            sbJugar.Begin();
        }

        private void reloj(object sender, EventArgs e)
        {

            Storyboard sbHambre = (Storyboard)this.Resources["hambre"];
            Storyboard sbCansancio = (Storyboard)this.Resources["cansancio"];
            Storyboard sbEnfadado = (Storyboard)this.Resources["enfadado"];
            this.pbEnergia.Value -= variacion;
            this.pbCansancio.Value -= variacion;
            this.pbDiversion.Value -= variacion;
            

           if (this.pbEnergia.Value >= 40)
            {
                this.pbEnergia.Foreground = Brushes.Green;
                
            }
            else if (this.pbEnergia.Value <= 40 && hambriento==false)
            {
                this.pbEnergia.Foreground = Brushes.Red;
                sbHambre.Begin();
                hambriento = true;
            }

            if (this.pbCansancio.Value >= 40)
            {
                this.pbCansancio.Foreground = Brushes.Green;
            }
            else if (this.pbCansancio.Value <= 40 && cansado == false)
            {
                this.pbCansancio.Foreground = Brushes.Red;
                sbCansancio.Begin();
                cansado = true;

            }

           
            if (this.pbDiversion.Value >= 40)
            {
                this.pbDiversion.Foreground = Brushes.Green;
            }
            else if (this.pbDiversion.Value <= 40 && enfadado == false)
            {
                this.pbDiversion.Foreground = Brushes.Red;
                sbEnfadado.Begin();
                enfadado = true;

            }
            if (entrenando == true)
            {
                ticksEntrenando += 1;
            }
            
            if (ticksEntrenando >= 5)
            {
                ticksEntrenando = 0;
                t1.Interval = TimeSpan.FromMilliseconds(1000.0);
                btnEntrenar.IsEnabled = true;
                entrenando = false;
            }
            if (pbEnergia.Value <= 0 || pbCansancio.Value <= 0 || pbDiversion.Value <= 0)
            {
                t1.Stop();
                t2.Stop();
                lblGameOver.Visibility = Visibility.Visible;
                this.btnComer.IsEnabled = false;
                this.btnDormir.IsEnabled = false;
                this.btnJugar.IsEnabled = false;
            }
        }

        private void cambiarLogros(object sender, EventArgs e)
        {
            this.tiempoJuego += 1;

            if (this.tiempoJuego >= 10)
            {
                this.imgLogro1.Opacity = 100;
                this.imgSinceridad.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Sinceridad.png", UriKind.RelativeOrAbsolute));
                this.imgPaisaje3.IsEnabled = true;
                this.imgPaisaje3.Opacity = 100;
            }

            if (this.tiempoJuego >= 30)
            {
                this.imgLogro2.Opacity = 100;
                this.imgAmistad.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Amistad.png", UriKind.RelativeOrAbsolute));
            }

            if (this.contadorClics >= 10)
            {
                this.imgLogro3.Opacity = 100;
                this.imgAmor.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Amor.png", UriKind.RelativeOrAbsolute));
                this.imgPaisaje2.IsEnabled = true;
                this.imgPaisaje2.Opacity = 100;
            }

            if (this.contadorClics >= 30)
            {
                this.imgLogro4.Opacity = 100;
                this.imgConocimiento.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Conocimiento.png", UriKind.RelativeOrAbsolute));
                this.imgBigote.IsEnabled = true;
                this.imgBigote.Opacity = 100;
            }

            if (this.contadorEntrenamientos >= 4)
            {
                this.imgLogro5.Opacity = 100;
                this.imgInocencia.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Inocencia.png", UriKind.RelativeOrAbsolute));
            }

            if (this.tiempoJuego >= 60 && contadorClics >= 40 && contadorEntrenamientos > 6)
            {
                this.btnDigievolucionar.Visibility = Visibility.Visible;
                this.btnDigievolucionar.IsEnabled = true;
                
            }
        }

      
        public void setNombre (String nombre)
        {
            this.lblNombre.Content = nombre;  

        }

        private void btnDigievolucionar_Click(object sender, RoutedEventArgs e)
        {
            cvCabezaAgumon.Visibility = Visibility.Hidden;
            cvCabezaGreymon.Visibility = Visibility.Visible;
            linea1.Visibility = Visibility.Visible;
            linea2.Visibility = Visibility.Visible;
            linea3.Visibility = Visibility.Visible;
            linea4.Visibility = Visibility.Visible;
            linea5.Visibility = Visibility.Visible;
            linea6.Visibility = Visibility.Visible;
            linea7.Visibility = Visibility.Visible;
            linea8.Visibility = Visibility.Visible;
            this.imgLogro6.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\bin\Debug\greymon.jpg", UriKind.RelativeOrAbsolute));
            this.imgLogro6.Opacity = 100;
            this.imgLogro6.ToolTip = "Has Digivolucionado a Greymon";
            this.imgValor.Source = new BitmapImage(new Uri(@"D:\Descargas\Tamagochi\Tamagochi\Valor.png", UriKind.RelativeOrAbsolute));
            this.btnDigievolucionar.Visibility = Visibility.Hidden;
            this.btnDigievolucionar.IsEnabled = false;

        }

        private void finAnimaciones(object sender, EventArgs e)
        {
            btnComer.IsEnabled = true;
            btnDormir.IsEnabled = true;
            btnJugar.IsEnabled = true;
        }

        private void deshabilitarBotones(object sender, EventArgs e)
        {
            btnComer.IsEnabled = false;
            btnDormir.IsEnabled = false;
            btnJugar.IsEnabled = false;
        }
       
        private void btnEntrenar_Click(object sender, RoutedEventArgs e)
        {
            contadorEntrenamientos += 1;
            entrenando = true;
            t1.Interval = TimeSpan.FromMilliseconds(500.0);
            btnEntrenar.IsEnabled = false;

        }

        private void ponerBigote(object sender, MouseButtonEventArgs e)
        {
            DataObject dataBigote = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataBigote, DragDropEffects.Move);
        }

        private void cvCabezaAgumon_Drop(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imgBigote":
                    bigoteAgumon.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ponerBigote(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imgBigote":
                    bigoteGreymon.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imgFondoCanvas.Source = ((Image)sender).Source;
        }
    }
}
