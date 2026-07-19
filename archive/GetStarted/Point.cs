public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) => (X,Y) = (x,y);
}

// public struct Point
// {
//     public double X { get; }
//     public double Y { get; }

//     public Point(double x, double y) => (X,Y) = (x,y);
// }

public class PointFactory(int numberOfPoints)
{
    public IEnumerable<Point> CreatePoints()
    {
        var generator = new Random();

        for (int i = 0; i < numberOfPoints; i++)
        {
            yield return new Point(generator.Next(), generator.Next());
        }
    }
}

public class Point3D : Point
{
    public int Z { get; set; }

    public Point3D(int x, int y, int z) : base(x,y)
    {
        Z = z;
    }
}