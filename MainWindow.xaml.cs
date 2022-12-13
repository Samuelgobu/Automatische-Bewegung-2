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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Timer
using System.Windows.Threading;

namespace Automatische_Bewegung_2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Timer add
        DispatcherTimer timer = new DispatcherTimer();
        double dt = 1.0 / 60.0; //sekunden


        

        public MainWindow()
        {
            InitializeComponent();
            //Fügen Sie im Canvas ein Rechteck mit einer Höhe von 20 und einer Breite von 20,
            //der Position Canvas.Left = 10 und Canvas.Bottom = 10 ein.
            //Das Rechteck soll eine beliebige Färbung haben. Geben Sie dem Rechteck den Namen "box".
            long ticks = Convert.ToInt64(dt * Math.Pow(10, 7));
            TimeSpan timespan = new TimeSpan(ticks);
            timer.Interval = timespan; //Unsere Uhr tickt 60 mal pro Sekunde.
            timer.Tick += Timer_Tick;
            //Dieser Befehl erzeugt eine Methode, die jedes Mal,
            //wenn die Uhr tickt, aufgerufen wird.
            timer.Start();
        }
        int zahlimLabel = 0;
        double vx = 150; //pixel / sekunde
        double vy = 100;
        double G = 981; // y(t) = (g/2)*t^2 + vy0*t + y0

        private void Timer_Tick(object sender, EventArgs e)
        {
            zahlimLabel++;
            label.Content = zahlimLabel;
            //Wir lesen die Anfangswerte aus.
            double x0 = Canvas.GetLeft(box);
            double y0 = Canvas.GetBottom(box);
            SpawnChild();
            //Wir berechnen die neuen Werte.
            double x = vx * dt + x0;
            double y = vy * dt + y0;
            //Wir setzen das Rechteck an die neue Position.
            Canvas.SetLeft(box, x);
            Canvas.SetBottom(box, y);
            //Wenn x bzw. y nicht mehr auf dem Canvas liegen,
            //müssen vx bzw. vy geändert werden.
            //Wann treffen wir die linke oder rechte Seite des Canvas?
            if (x <= 0 || x >= canvas.ActualWidth - box.Width)
            {
                vx = -vx;
            }
            else if (y <= 0 || y >= canvas.ActualHeight - box.Height)
            {
                vy = -vy;
            }
        }

        private void SpawnChild()
        {
            double x0 = Canvas.GetLeft(box);
            double y0 = Canvas.GetBottom(box);
            Ellipse el1 = new Ellipse()
            {
                Width = 2,
                Height = 2,
                Fill = Brushes.Blue,
            };
            Canvas.SetLeft(el1, x0 + box.Width / 2);
            Canvas.SetBottom(el1, y0 + box.Height / 2);
            canvas.Children.Add(el1);
        }
    }
}
