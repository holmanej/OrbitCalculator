using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitCalculator
{
    class Orbit
    {
        double Mu;
        public double a;
        public double e;

        public Orbit(double[] velocity, double[] position, double mass)
        {
            Mu = mass * 6.674e-11;
            a = Geta(Mu, velocity, position);
            e = Gete(Mu, velocity, position);
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

        public double GetRp()
        {
            return a * (1 - e);
        }

        public double GetRa()
        {
            return a * (1 + e);
        }

        public double GetT()
        {
            return (2 * Math.PI * Math.Sqrt(Math.Pow(a, 3) / Mu));
        }

        static double Magnitude(double[] v)
        {
            double m = Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2) + Math.Pow(v[2], 2));
            return m;
        }

        static double Gete(double mu, double[] v, double[] r)
        {
            double temp1 = Math.Pow(Magnitude(v), 2) - mu / Magnitude(r);
            double temp2 = v[0] * r[0] + v[1] * r[1] + v[2] * r[2];
            double[] eVec = { (r[0] * temp1 - v[0] * temp2) / mu, (r[1] * temp1 - v[1] * temp2) / mu, (r[2] * temp1 - v[2] * temp2) / mu };
            return Magnitude(eVec);
        }

        static double Geta(double mu, double[] v, double[] r)
        {
            double E = (Math.Pow(Magnitude(v), 2) / 2) - (mu / Magnitude(r));
            return -mu / (2 * E);
        }
    }
}
