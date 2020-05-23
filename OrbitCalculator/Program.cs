using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitCalculator
{
    class Program
    {
        public static double Mu = 1.327e20;

        static void Main(string[] args)
        {
            double axis = 0;
            for (int i = 0; axis < 1.5e11; i++)
            {
                axis = CalcMajorAxis(3600 * i, 3000);
                Debug.WriteLine(i + " : " + axis / 1000000000);
            }
            CalcOrbit(3000, 3600 * 3135, 2e30);
            CalcOrbit(1500, 86400 * 700, 2e30);

            Console.ReadKey();
        }

        static void CalcOrbit(double velocity, double period, double mass)
        {
            double axis = CalcMajorAxis(period, velocity);
            double[] v = { 0, velocity, 0 };
            double[] p = { axis, 0, 0 };
            Orbit Roid = new Orbit(v, p, mass);
            Console.WriteLine("e: " + Roid.e);
            Console.WriteLine("a: " + (Roid.a / 1000000000) + " M Km");
            Console.WriteLine("Period: " + (Roid.GetT() / 3600) + " hours");
            Console.WriteLine("Perihelion: " + (Roid.GetRp() / 1000) + " Km");
            Console.WriteLine("Aphelion: " + (Roid.GetRa() / 1000000000) + " M Km");

            Console.WriteLine("Sun Orbit Dist: " + (Roid.GetRp() / 1000) + " Km");
            Console.WriteLine("Sun speed: " + (Roid.Getv(0) / 1000) + " Km/s");
            Console.WriteLine("c%: " + (Roid.Getv(0) / 3000000) + "%");
            Console.WriteLine("Object Orbit Dist: " + ((Roid.GetRa() - 1.5e11) / 1000) + " Km");
            Console.WriteLine("Object speed: " + Roid.Getv(Convert.ToInt32(Roid.GetT()) / 2));
            Console.WriteLine("\r\n");
        }

        static double CalcMajorAxis(double period, double velocity)
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
