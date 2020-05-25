using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitCalculator
{
    class Orbit
    {
        double Mu;
        public double a;
        public double b;
        public double e;
        public double Ra;
        public double Rp;
        public double T;
        public int tCurrent;

        public class PathData
        {
            public double E { get; set; }
            public double Distance { get; set; }
            public double Velocity { get; set; }

            public PathData(double e, double r, double v)
            {
                E = e;
                Distance = r;
                Velocity = v;
            }
        }

        public List<PathData> Path;

        public Orbit(double velocity, double period, double mass)
        {
            Mu = mass * 6.674e-11;
            a = Geta(period, velocity);
            e = Gete(Mu, new double[] { 0, velocity, 0 }, new double[] { a, 0, 0 } );
            b = Getb(a, e);
            Ra = GetRa();
            Rp = GetRp();
            T = GetT();
            Path = CompleteOrbit();
        }

        private List<PathData> CompleteOrbit()
        {
            List<PathData> orbitData = new List<PathData>();
            tCurrent = 0;
            double E = 0;
            for (int i = 0; i <= GetT(); i += 60)
            {
                E -= (E - e * Math.Sin(E) - i * Math.Sqrt(Mu / Math.Pow(a, 3))) / (1 - e * Math.Cos(E));
                double r = a * (1 - Math.Pow(e, 2)) / (1 + e * ((Math.Cos(E) - e) / (1 - e * Math.Cos(E))));
                double v = Math.Sqrt(Mu * (2 / r - 1 / a));
                orbitData.Add(new PathData(E, r, v));
            }
            return orbitData;
        }

        public double GetE(int t)
        {
            double E = 0;
            for (int i = 0; i <= t; i++)
            {
                E -= (E - e * Math.Sin(E) - i * Math.Sqrt(Mu / Math.Pow(a, 3))) / (1 - e * Math.Cos(E));
            }
            return E;
        }

        public double Getr(int t)
        {
            double E = GetE(t);
            return (a * (1 - Math.Pow(e, 2))) / (1 + e * ((Math.Cos(E) - e) / (1 - e * Math.Cos(E))));
        }

        public double Getv(int t)
        {
            double r = Getr(t);
            return Math.Sqrt(Mu * (2 / r - 1 / a));
        }

        private double GetRp()
        {
            return a * (1 - e);
        }

        private double GetRa()
        {
            return a * (1 + e);
        }

        private double GetT()
        {
            return (2 * Math.PI * Math.Sqrt(Math.Pow(a, 3) / Mu));
        }

        private double Magnitude(double[] v)
        {
            double m = Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2) + Math.Pow(v[2], 2));
            return m;
        }

        private double Gete(double mu, double[] v, double[] r)
        {
            double temp1 = Math.Pow(Magnitude(v), 2) - mu / Magnitude(r);
            double temp2 = v[0] * r[0] + v[1] * r[1] + v[2] * r[2];
            double[] eVec = { (r[0] * temp1 - v[0] * temp2) / mu, (r[1] * temp1 - v[1] * temp2) / mu, (r[2] * temp1 - v[2] * temp2) / mu };
            return Magnitude(eVec);
        }

        private double Getb(double a, double e)
        {
            return Math.Sqrt(Math.Pow(a, 2) * (1 - Math.Pow(e, 2)));
        }

        private double Geta(double period, double velocity)
        {
            double temp = period / 6.28;
            temp = Math.Pow(temp, 2);
            temp *= Mu;
            temp = Math.Pow(temp, (double)1 / 3);
            temp *= (-2 / Mu);
            temp = 1 / temp;
            temp = (Math.Pow(velocity, 2) / 2) - temp;
            temp = 1 / temp;
            temp *= Mu;
            return temp;
        }
    }
}
