class Example
{
    public static void Main()
    {
        var s = new Collections.Stack<int>();
        s.Push(1);
        s.Push(10);
        s.Push(100);
        Console.WriteLine(s.Pop());
        Console.WriteLine(s.Pop());
        Console.WriteLine(s.Pop());

        var p1 = new Point(0, 0);
        var p2 = new Point(10, 20);

        var factory = new PointFactory(10);

        foreach (var point in factory.CreatePoints())
        {
            Console.WriteLine($"({point.X}, {point.Y})");
        }

        Point a = new(10, 20);
        Point b = new Point3D(10, 20, 30);

        Expression e = new Operation(new VariableReference("x"), '+', new Constant(3));
        Dictionary<string, object> vars = new();
        vars["x"] = 7;
        Console.WriteLine(e.Evaluate(vars));

        Expression e2 = new Operation(
            new VariableReference("x"),
            '*',
            new Operation(new VariableReference("y"), '+', new Constant(2))
        );
        vars["x"] = 3;
        vars["y"] = 5;
        Console.WriteLine(e2.Evaluate(vars));
        vars["x"] = 1.5;
        vars["y"] = 9;
        Console.WriteLine(e2.Evaluate(vars));

        MyList<string> list1 = new();
        MyList<string> list2 = new(10);

        MyList<string> names = new();
        // names.Capacity = 100;
        // int i = names.Count;
        // int j = names.Capacity;

        names.Changed += new EventHandler(ListChanged);
        Console.WriteLine(s_changeCount);
        
        names.Add("Liz");
        names.Add("Martha");
        names.Add("Beth");
       
        Console.WriteLine(s_changeCount);
      
        for (int i = 0; i < names.Count; i++)
        {
            string s2 = names[i];
            names[i] = s2.ToUpper();
        }

        Console.WriteLine(s_changeCount);

        MyList<int> a2 = new();
        a2.Add(1);
        a2.Add(2);
        MyList<int> b2 = new();
        b2.Add(1);
        b2.Add(2);
        Console.WriteLine(a2 == b2);
        b2.Add(3);
        Console.WriteLine(a2 == b2);

        int[] a3 = new int[10];
        for (int i = 0; i < a3.Length; i++)
        {
            a3[i] = i * i;
        }
        for (int i = 0; i < a3.Length; i++)
        {
            Console.WriteLine($"a3[{i}] = {a3[i]}");
        }

        double[] c = { 0.0, 0.5, 1.0 };
        double[] squares = Apply(c, (x) => x * x);
        double[] sines = Apply(c, Math.Sin);
        Multiplier m = new(2.0);
        double[] doubles  = Apply(c, m.Multiply);
    }

    static int s_changeCount;

    static void ListChanged(object sender, EventArgs e)
    {
        s_changeCount++;
    }

    static double[] Apply(double[] a, Function f)
    {
        var result = new double[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = f(a[i]);
        }
        return result;
    }
} 

delegate double Function(double x);

class Multiplier
{
    double _factor;

    public Multiplier(double factor) => _factor = factor;

    public double Multiply(double x) => x * _factor;
}