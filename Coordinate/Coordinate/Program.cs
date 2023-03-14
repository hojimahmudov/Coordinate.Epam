using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        // create a bird object
        Bird bird = new Bird();
        bird.CurrentPosition = new Coordinate(0, 0, 0);

        // create an airplane object
        Airplane airplane = new Airplane();
        airplane.CurrentPosition = new Coordinate(0, 0, 0);

        // create a drone object
        Drone drone = new Drone();
        drone.CurrentPosition = new Coordinate(0, 0, 0);

        // fly the bird to a new location
        Coordinate birdDestination = new Coordinate(50, 50, 0);
        bird.FlyTo(birdDestination);
        Console.WriteLine($"The bird took {bird.GetFlyTime(birdDestination):F2} hours to fly to ({birdDestination.X}, {birdDestination.Y}, {birdDestination.Z}).");

        // fly the airplane to a new location
        Coordinate airplaneDestination = new Coordinate(500, 500, 100);
        airplane.FlyTo(airplaneDestination);
        Console.WriteLine($"The airplane took {airplane.GetFlyTime(airplaneDestination):F2} hours to fly to ({airplaneDestination.X}, {airplaneDestination.Y}, {airplaneDestination.Z}).");

        // fly the drone to a new location
        Coordinate droneDestination = new Coordinate(100, 100, 50);
        drone.FlyTo(droneDestination);
        Console.WriteLine($"The drone took {drone.GetFlyTime(droneDestination):F2} hours to fly to ({droneDestination.X}, {droneDestination.Y}, {droneDestination.Z}).");

        Console.ReadLine();
    }
}

public struct Coordinate
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Coordinate(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
}

interface IFlyable
{
    Coordinate CurrentPosition { get; set; }
    void FlyTo(Coordinate newPosition);
    double GetFlyTime(Coordinate newPosition);
}

class Bird : IFlyable
{
    private const double MaxSpeed = 20;
    private const double MaxDistance = 1000;

    public Coordinate CurrentPosition { get; set; }

    public void FlyTo(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        if (distance > MaxDistance)
        {
            throw new ArgumentOutOfRangeException("The bird cannot fly more than 1000 km.");
        }
        double time = distance / MaxSpeed;
        Console.WriteLine($"The bird flew to ({newPosition.X}, {newPosition.Y}, {newPosition.Z}) in {time:F2} hours.");
        CurrentPosition = newPosition;
    }

    public double GetFlyTime(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        if (distance > MaxDistance)
        {
            throw new ArgumentOutOfRangeException("The bird cannot fly more than 1000 km.");
        }
        double time = distance / MaxSpeed;
        return time;
    }

    private double GetDistance(Coordinate point1, Coordinate point2)
    {
        double dx = point2.X - point1.X;
        double dy = point2.Y - point1.Y;
        double dz = point2.Z - point1.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}

class Airplane : IFlyable
{
    private const double MaxSpeed = 200;
    private const double SpeedIncrease = 10;
    private const double SpeedIncreaseDistance = 10;

    public Coordinate CurrentPosition { get; set; }

    public void FlyTo(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        double time = 0;
        double speed = MaxSpeed;
        while (distance > 0)
        {
            if (distance > SpeedIncreaseDistance)
            {
                speed += SpeedIncrease;
                distance -= SpeedIncreaseDistance;
                time += SpeedIncreaseDistance / speed;
            }
            else
            {
                time += distance / speed;
                distance = 0;
            }
        }
        Console.WriteLine($"The airplane flew to ({newPosition.X}, {newPosition.Y}, {newPosition.Z}) in {time:F2} hours.");
        CurrentPosition = newPosition;
    }

    public double GetFlyTime(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        double time = 0;
        double speed = MaxSpeed;
        while (distance > 0)
        {
            if (distance > SpeedIncreaseDistance)
            {
                speed += SpeedIncrease;
                distance -= SpeedIncreaseDistance;
                time += SpeedIncreaseDistance / speed;
            }
            else
            {
                time += distance / speed;
                distance = 0;
            }
        }
        return time;
    }

    private double GetDistance(Coordinate point1, Coordinate point2)
    {
        double dx = point2.X - point1.X;
        double dy = point2.Y - point1.Y;
        double dz = point2.Z - point1.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}

class Drone : IFlyable
{
    private const double MaxSpeed = 30;
    private const double HoverTime = 1;
    private const double HoverInterval = 10;
    private const double MaxDistance = 500;

    public Coordinate CurrentPosition { get; set; }

    public void FlyTo(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        if (distance > MaxDistance)
        {
            throw new ArgumentOutOfRangeException("The drone cannot fly more than 500 km.");
        }
        double time = 0;
        double hoverTime = 0;
        while (distance > 0)
        {
            if (hoverTime >= HoverInterval)
            {
                time += HoverTime / 60;
                hoverTime = 0;
            }
            else
            {
                double travelDistance = MaxSpeed / 60;
                if (distance < travelDistance)
                {
                    time += distance / MaxSpeed;
                    distance = 0;
                }
                else
                {
                    time += travelDistance / MaxSpeed;
                    distance -= travelDistance;
                    hoverTime++;
                }
            }
        }
        Console.WriteLine($"The drone flew to ({newPosition.X}, {newPosition.Y}, {newPosition.Z}) in {time:F2} hours.");
        CurrentPosition = newPosition;
    }

    public double GetFlyTime(Coordinate newPosition)
    {
        double distance = GetDistance(CurrentPosition, newPosition);
        if (distance > MaxDistance)
        {
            throw new ArgumentOutOfRangeException("The drone cannot fly more than 500 km.");
        }
        double time = 0;
        double hoverTime = 0;
        while (distance > 0)
        {
            if (hoverTime >= HoverInterval)
            {
                time += HoverTime / 60;
                hoverTime = 0;
            }
            else
            {
                double travelDistance = MaxSpeed / 60;
                if (distance < travelDistance)
                {
                    time += distance / MaxSpeed;
                    distance = 0;
                }
                else
                {
                    time += travelDistance / MaxSpeed;
                    distance -= travelDistance;
                    hoverTime++;
                }
            }
        }
        return time;
    }

    private double GetDistance(Coordinate point1, Coordinate point2)
    {
        double dx = point2.X - point1.X;
        double dy = point2.Y - point1.Y;
        double dz = point2.Z - point1.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

}
