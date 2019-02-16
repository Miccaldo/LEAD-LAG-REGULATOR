using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMM {

    public class Regulator {

        public delegate double Stimulation(double x);

        public List<DataPoint> Points { get; private set; }

        private double dt = 0.001;
        public int timeRange { get; set; }

        public double a1 { get; set; }              // Parametry regulatora
        public double a0 { get; set; }
        public double b2 { get; set; }
        public double b1 { get; set; }
        public double b0 { get; set; }
        public double p1 { get; set; }
        public double p0 { get; set; }
        public double z1 { get; set; }
        public double z0 { get; set; }

        private double x1, x2, x3, x4, dx1, dx2, dx3, dx4;
        public double w11, w12, w13, w14, w21, w22, w23, w24;      // Zmienne przechowujace wartosci macierzy stanu


        public void DrawPlot(Stimulation stimulation, int type) {       // Funkcja oblicza zmienne stanu metoda Eulera oraz tworzy wykres sygnalu wyjsciowego lub wejsciowego

            Points = new List<DataPoint>();
            Points.Add(new DataPoint(0, 0));

            for (int i = 0; i < timeRange / dt; i++) {

                if (type == 1) {
                    x1 = x1 + (w11 * dx1 + dx2) * dt + w21 * stimulation(i * dt) * dt;
                    x2 = x2 + (w12 * dx1 + dx3) * dt + w22 * stimulation(i * dt) * dt;
                    x3 = x3 + (w13 * dx1 + dx4) * dt + w23 * stimulation(i * dt) * dt;
                    x4 = x4 + (w14 * dx1) * dt + w24 * stimulation(i * dt) * dt;

                    dx1 = x1;
                    dx2 = x2;
                    dx3 = x3;
                    dx4 = x4;

                    Points.Add(new DataPoint(i * dt, x1));
                } else {
                    x1 = stimulation(i * dt);
                    Points.Add(new DataPoint(i * dt, x1));
                }
            }
        }

        // OBLICZENIA 

        public Regulator(double a0 = 1, double a1 = -1, double b0 = 1, double b1 = 1, double b2 = 1, double z0 = 1, double z1 = 4, double p0 = 2, double p1 = 3, int timeRange = 20) {

            this.a0 = a0;
            this.a1 = a1;
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.z0 = z0;
            this.z1 = z1;
            this.p0 = p0;
            this.p1 = p1;

            w11 = -(a1 / b2 + p0 + p1 + b1 / b2);
            w12 = -(a1 * z1 / b2 + a0 / b2 + a1 * z0 / b2 + p0 * p1 + b1 * p1 / b2 + b1 * p0 / b2 + b0 / b2);
            w13 = -(b0 * p0 / b2 + b0 * p1 / b2 + b1 * p0 * p1 / b2 + a0 * z0 / b2 + a0 * z1 / b2 + a1 * z0 * z1 / b2);
            w14 = -(a0 * z0 * z1 / b2 + b0 * p0 * p1 / b2);


        

            w21 = a1 / b2;
            w22 = (a1 * z1 / b2 + a1 * z0 / b2 + a0 / b2);
            w23 = (a1 * z0 * z1 / b2 + a0 * z1 / b2 + a0 * z0 / b2);
            w24 = a0 * z0 * z1 / b2;

            this.timeRange = timeRange;
        }
    }
}