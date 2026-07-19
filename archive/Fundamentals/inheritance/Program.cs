public class Program
{
    public static void Main2()
    {
        var o = new DerivedClass();
        o.AbstractMethod();
        Console.WriteLine($"x = {o.X}, y = {o.Y}");
    }

    public static void Main3()
    {
        var book = new Book("Leviathan Wakes (Expanse #1)", "1841499889", "Corey, James SA", "Orbit");
        ShowPublicationInfo(book);
        book.Publish(new DateTime(2011, 6, 11));
        ShowPublicationInfo(book);

        var book2 = new Book("Leviathan Wakes (Expanse #1)", "Tor", "Corey, James SA");
        Console.WriteLine($"{book.Title} and {book2.Title} are the same publication: {((Publication)book).Equals(book2)}");
    }

    public static void ShowPublicationInfo(Publication pub)
    {
        string pubDate = pub.GetPublicationDate();
        Console.WriteLine($"{pub.Title}, {(pubDate == "NYP" ? "Not Yet Published" : "published on " + pubDate):d} by {pub.Publisher}");
    }

    public static void Main()
    {
        Shape[] shapes = { new Rectangle(10, 12), new Square(5), new Circle(3)};

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"{shape}: area, {Shape.GetArea(shape)}; perimeter, {Shape.GetPerimeter(shape)}");

            if (shape is Rectangle rect)
            {
                Console.WriteLine($"    Is Square: {rect.IsSquare()}, Diagonal: {rect.Diagonal}");
                continue;
            }
            if (shape is Square sq)
            {
                Console.WriteLine($"    Diagonal: {sq.Diagonal}");
                continue;
            }
        }
    }
}