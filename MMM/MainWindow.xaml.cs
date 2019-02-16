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
using System.Diagnostics;
using OxyPlot;
using MMM;

namespace MMM {
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
     
        public Regulator regulator { get; set; }
        public int amplitude { get; private set; }
        public int periodTriangle { get; set; }
        public int stepTime { get; set; }
        public int typePlotFlag { get; set; }

        public MainWindow() {

            InitializeComponent();

            amplitude = 1;
            typePlotFlag = 1;
            periodTriangle = amplitude;

            regulator = new Regulator();

            stepTime = regulator.timeRange;

            RadioButton_Step.IsChecked = true;

            parametersGrid.DataContext = regulator;

            //MessageBox.Show($"{regulator.w11}, {regulator.w12}, {regulator.w13}, {regulator.w14}");
            //MessageBox.Show($"{regulator.w21}, {regulator.w22}, {regulator.w23}, {regulator.w24}");
        }

       // ************************ Zdarzenia oblugujace parametry regulatora  ************************

        private void a1_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if(Double.TryParse(a1_Box.Text, out value) == true) {
                    regulator.a1 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    a1_Box.Text = null;
                }
            }
        }
        private void a0_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(a0_Box.Text, out value) == true) {
                    regulator.a0 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    a0_Box.Text = null;
                }
            }
        }
        private void b2_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(b2_Box.Text, out value) == true) {
                    regulator.b2 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    b2_Box.Text = null;
                }
            }
        }
        private void b1_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(b1_Box.Text, out value) == true) {
                    regulator.b1 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    b1_Box.Text = null;
                }
            }
        }
        private void b0_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(b0_Box.Text, out value) == true) {
                    regulator.b0 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    b0_Box.Text = null;
                }
            }
        }
        private void z1_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(z1_Box.Text, out value) == true) {
                    regulator.z1 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    z1_Box.Text = null;
                }
            }
        }
        private void z0_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(z0_Box.Text, out value) == true) {
                    regulator.z0 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    z0_Box.Text = null;
                }
            }
        }
        private void p1_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(p1_Box.Text, out value) == true) {
                    regulator.p1 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    p1_Box.Text = null;
                }
            }
        }
        private void p0_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                double value;
                if (Double.TryParse(p0_Box.Text, out value) == true) {
                    regulator.p0 = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane");
                    p0_Box.Text = null;
                }
            }
        }

        // ************************ Zdarzenia oblugujace czas trwania sygnalu, amplitude, czas trwania skoku jednostkowego  ************************


        private void Time_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                int value;
                if (int.TryParse(Time_Box.Text, out value) == true) {
                    regulator.timeRange = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane(int)");
                    Time_Box.Text = null;
                }
            }
        }

        private void Amplitude_KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                int value;
                if (int.TryParse(Amplitude_Box.Text, out value) == true) {
                    this.amplitude = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane(int)");
                    Amplitude_Box.Text = null;
                }
            }
        }

        private void StepTime_KeyDownHandler(object sender, KeyEventArgs e) {  
            if (e.Key == Key.Return) {
                int value;
                if (int.TryParse(StepTime_Box.Text, out value) == true) {
                    this.stepTime = value;
                    UpdateGrid();
                } else {
                    MessageBox.Show("Nieprawidłowe dane(int)");
                    StepTime_Box.Text = null;
                }
            }
        }


        // ************************ -----------------------------  ************************


        private void UpdateGrid() {             //  Metoda aktualizuje wykres

            regulator = new Regulator(regulator.a0, regulator.a1, regulator.b0, regulator.b1, regulator.b2, regulator.z0, regulator.z1, regulator.p0, regulator.p1, regulator.timeRange);
            SelectStimulation();

            gridek.DataContext = regulator;
        }


        private double Step(double time) {      // Funkcje skoku, sinusa, trojkata
            if(time <= stepTime)
                return amplitude * 1;
            else {
                return 0;
            }
        }

        private double Sinus(double time) {         
            return amplitude * Math.Sin(time);
        }

        private double Triangle(double time) {         
            return (amplitude / periodTriangle) * (periodTriangle - Math.Abs(time % (2 * periodTriangle) - periodTriangle));
        }

        private void RadioButton_Step_Checked(object sender, RoutedEventArgs e) {       // Zdarzenia wyboru pobudzenia 
            UpdateGrid();
            RadioButton_Sinus.Margin = new Thickness(0, 50, 0, 0);
            RadioButton_Triangle.Margin = new Thickness(0, 80, 0, 0);
            StepTime_Box.Visibility = Visibility.Visible;
        }

        private void RadioButton_Sinus_Checked(object sender, RoutedEventArgs e) {
            UpdateGrid();
            RadioButton_Sinus.Margin = new Thickness(0, 30, 0, 0);
            RadioButton_Triangle.Margin = new Thickness(0, 60, 0, 0);
            StepTime_Box.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Triangle_Checked(object sender, RoutedEventArgs e) {
            UpdateGrid();
            RadioButton_Sinus.Margin = new Thickness(0, 30, 0, 0);
            RadioButton_Triangle.Margin = new Thickness(0, 60, 0, 0);
            StepTime_Box.Visibility = Visibility.Hidden;
        }

        private void SelectStimulation() {      // Metoda rysuje wykres w zaleznosci od pobudzenia

            if(RadioButton_Step.IsChecked == true){
                regulator.DrawPlot(Step, typePlotFlag);
            }else if(RadioButton_Sinus.IsChecked == true) {
                regulator.DrawPlot(Sinus, typePlotFlag);
            } else {
                regulator.DrawPlot(Triangle, typePlotFlag);
            }
        }

        private void TypePlot_Click(object sender, RoutedEventArgs e) {     // Zdarzenie zmieniajace rodzaj wykresu (wejscie lub wyjscie)

            typePlotFlag++;

            if (typePlotFlag > 1) {
                y_axis.Content = "u(t)";
                typePlotButton.Content = "Odpowiedź";
                typePlotFlag = 0;
            } else {
                y_axis.Content = "y(t)";
                typePlotButton.Content = "Pobudzenie";
            }

            UpdateGrid();
        }

        private void reset_Click(object sender, RoutedEventArgs e) {

            regulator = new Regulator();
            UpdateGrid();
            parametersGrid.DataContext = regulator;
        }
    }
}