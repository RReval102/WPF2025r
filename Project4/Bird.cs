using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace angrybirds
{
    class BirdEventArg
    {
        public string Message { get; }
        public double TimeOfFligth { get; }
        public double MaxY { get; }
        public double Range { get; }
        public BirdEventArg(string message, double timeOfFligth, double maxY, double range)
        {
            Message = message;
            TimeOfFligth = timeOfFligth;
            MaxY = maxY;
            Range = range;
        }
    }
    class Bird
    {
        public static double g = 9.8;
        public static double step = 0.01;

        public double t, x, y, velocity, angle, k, m;
        List<double> X = new List<double>();
        List<double> Y = new List<double>();
        List<double> Velocity_X = new List<double>();
        List<double> Velocity_Y = new List<double>();

        public delegate void FallHandler(Bird sender, BirdEventArg e, StreamWriter writer);
        public event FallHandler Notify;

        public Bird(double x = 0, double y = 0, double velocity = 0, double angle = 0, double k = 0, double m = 0, double t = 0)
        {
            this.x = x;
            this.y = y;
            this.velocity = velocity;
            this.angle = angle;
            this.k = k;
            this.t = t;
        }

        public string[] ReadInputData(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            velocity = double.Parse(lines[0]);
            angle = double.Parse(lines[1]);
            m = double.Parse(lines[2]);
            k = double.Parse(lines[3]);
            return lines;
        }

        public void ReadFromTextBox(List<TextBox> textBoxes)
        {

            velocity = double.Parse(textBoxes[0].Text);
            angle = double.Parse(textBoxes[1].Text);
            m = double.Parse(textBoxes[2].Text);
            k = double.Parse(textBoxes[3].Text);
        }
        public void WriteFlightData(string outputFile)
        {
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                angle = angle * Math.PI / 180.0;
                Velocity_X.Add(velocity * Math.Cos(angle));
                Velocity_Y.Add(velocity * Math.Sin(angle));
                X.Add(0);
                Y.Add(0);
                int totalSteps = (int)(100 / step);
                for (int i = 0; i <= totalSteps; i += 1)
                {
                    t += step;
                    Velocity_X.Add(Velocity_X[i] - step * (k * Velocity_X[i]) / m);
                    Velocity_Y.Add(Velocity_Y[i] - step * (g + k * Velocity_X[i] / m));
                    X.Add(X[i] + step * Velocity_X[i]);
                    Y.Add(Y[i] + step * Velocity_Y[i]);
                    writer.WriteLine($"t = {t:F2} c: x = {X[i]:F2} м, y = {Y[i]:F2} м");
                    if (Y[i] < 0)
                    {
                        double timeOfFlight = t - step;
                        double range = X[i - 1];
                        double maxY = Y.Max();
                        Notify?.Invoke(this, new BirdEventArg($"Тело упало. Время падения {timeOfFlight:F2} с," +
                            $" дальность броска {range:F2} м, максимальная высота {maxY:F2} м", timeOfFlight, maxY, range), writer);

                        break;
                    }
                }
            }
        }
        public List<double> GetX()
        {
            return X;
        }
        public List<double> GetY()
        {
            return Y;
        }

    }
}