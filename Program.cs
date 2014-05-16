using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoulombsLawSolver
{
    class Program
    {
        static void Main(string[] args) {
            Console.Title = "Coulombs Law Solver";
            Console.Out.WriteLine(Math.Atan2(-11.51, 17.25) * 180/Math.PI);
            Menu testMenu = new Menu();
            testMenu.clearMenu();
            testMenu.startMenu();

            HoldOpen();
        }


        static void HoldOpen()
        {
            Console.In.ReadLine();
        }
    }

    class Point
    {
        private double xCord;
        private double yCord;
        private double charge;
        
        public double getXCord()
        {
            return this.xCord;
        }
        public double getYCord()
        {
            return this.yCord;
        }
        public double getCharge()
        {
           return this.charge;
        }
        public void setXCord(double xCord)
        {
            this.xCord = xCord;
        }
        public void setYCord(double yCord)
        {
            this.yCord = yCord;
        }
        public void setCharge(double charge) 
        {
            this.charge = charge;
        }

        public Point(double initXCord, double initYCord) 
        {
            this.xCord = initXCord;
            this.yCord = initYCord;
        }
        public Point(double initXCord, double initYCord, double charge) 
        {
            this.xCord = initXCord;
            this.yCord = initYCord;
            this.charge = charge;
        
        }
        public String toReadableString()
        {
            String returnString = "Xcord: " + this.xCord + " Ycord: " + this.yCord + "  Charge: " + charge;
            return returnString;
        }


    }

    class Menu
    {

        List<Point> listOfPoints = new List<Point>();
        public void clearMenu(){
            for (int i = 0; i < 100; i++) { 
                Console.Out.WriteLine(); 
            }
                
        }

        public void startMenu(){
            Console.Out.WriteLine("Charge Simulation");
            Console.Out.WriteLine("Press 1 to create a new point");
            Console.Out.WriteLine("Press 2 to recieve results");
            Console.Out.WriteLine("Press 3 to remove a charge");
            Console.Out.WriteLine("Press 4 to run tests");
            string choice = Console.In.ReadLine();
            if (choice.Equals("1"))
            {
                createPoint();
                startMenu();
            }
            if (choice.Equals("2"))
            {
                if (listOfPoints.Count < 2)
                {
                    Console.Out.WriteLine("You need more than two points");
                    Console.Out.WriteLine();
                    startMenu();
                }

                else
                {
                    Console.Out.WriteLine("Which point would you like to solve for?");
                    displayListOfPoints();
                    int solveChoice = int.Parse(Console.In.ReadLine());
                    double x = solveForVectorsX(listOfPoints.ElementAt(solveChoice));
                    double y = solveForVectorsY(listOfPoints.ElementAt(solveChoice));
                    double resultant = Math.Atan2(y, x) * 180 / Math.PI;
                    double force = Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2));
                    Console.Out.WriteLine("X: " + x);
                    Console.Out.WriteLine("Y: " + y);
                    Console.Out.WriteLine("Force: " + force );
                    Console.Out.WriteLine("Angle:" + resultant );
                    startMenu();
                }

            }
            if (choice.Equals("3"))
            {
                Console.Out.WriteLine("Which charge would you like to remove?");
                displayListOfPoints();
                int delete = int.Parse(Console.In.ReadLine());
                listOfPoints.RemoveAt(delete);  
                startMenu();
            }
            if (choice.Equals("4"))
            {
                Point pointOne = new Point(0, 0, 1);
                Point pointTwo = new Point(3, 4, 2);
                listOfPoints.Add(pointOne);
                listOfPoints.Add(pointTwo);
                Console.Out.WriteLine("Force: " + calculateForce(pointOne,pointTwo));
                Console.Out.WriteLine("Distance " + distanceBetweenPoints(pointOne, pointTwo));
                Console.Out.WriteLine("Angle: " + calculateAngle(pointOne, pointTwo) * 180/Math.PI);
            }

        }

        public void displayListOfPoints()
        {
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                Console.Out.WriteLine(i + ": " + listOfPoints[i].toReadableString());
            }
        }

        static double distanceBetweenPoints(Point point1, Point point2)
        {
            double x = Math.Pow((point1.getXCord() - point2.getXCord()), 2);
            double y = Math.Pow((point1.getYCord() - point2.getYCord()), 2);
            return Math.Sqrt(x + y);
        }
        private void createPoint()
        {
            double xCord;
            double yCord;
            double charge;

            Console.Out.WriteLine("X-Position: ");
            xCord = double.Parse(Console.In.ReadLine());
            Console.Out.WriteLine("Y-Postition: ");
            yCord = double.Parse(Console.In.ReadLine());
            Console.Out.WriteLine("Charge: ");
            charge = double.Parse(Console.In.ReadLine());

            Point testPoint = new Point(xCord, yCord, charge);
            listOfPoints.Add(testPoint);
            

        }
        private double calculateForce(Point pointOne, Point pointTwo)
        {

            //Point One is the point being acted upon
            double k = 9 * Math.Pow(10, 9);
            double distance = distanceBetweenPoints(pointOne, pointTwo);
            double answer = (k * pointOne.getCharge() * pointTwo.getCharge())/Math.Pow(distance,2);
            return answer;

        }

        private double calculateAngle(Point pointOne, Point pointTwo)
        {
            double x;
            double y;
            x = pointOne.getXCord() - pointTwo.getXCord();
            y = pointOne.getYCord() - pointTwo.getYCord();
            return ((Math.Atan2(y, x))); 

        }

        private double solveForVectorsX(Point pointSolvingFor)
        {
            double x = 0;
            double force;
            double angle;
            int indexOfPointToNotCalculate = listOfPoints.IndexOf(pointSolvingFor);
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                if (i != indexOfPointToNotCalculate)
                {
                    force = calculateForce(pointSolvingFor, listOfPoints.ElementAt(i));
                    angle = calculateAngle(pointSolvingFor, listOfPoints.ElementAt(i));
                    x += force * Math.Cos(angle)
                }
            }
            return x;
        }

        private double solveForVectorsY(Point pointSolvingFor)
        {
            double y = 0;
            double force;
            double angle;
            int indexOfPointToNotCalculate = listOfPoints.IndexOf(pointSolvingFor);
            for(int i = 0; i < listOfPoints.Count; i++)
            {
                if (i != indexOfPointToNotCalculate)
                {
                    force = calculateForce(pointSolvingFor, listOfPoints.ElementAt(i));
                    angle = calculateAngle(pointSolvingFor, listOfPoints.ElementAt(i));
                    y += force * Math.Sin(angle);
                }
            }
            return y;
        }

        static void HoldOpen(){
            Console.In.ReadLine();
        }
    }
}
