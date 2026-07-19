using System;

public sealed class Book : Publication
{
    public Book(string title, string author, string publisher) :
        this(title, string.Empty, author, publisher)
    { }

    public Book(string title, string isbn, string author, string publisher) :
        base(title, publisher, PublicationType.Book)
    {
        if (!string.IsNullOrEmpty(isbn))
        {
            if (!(isbn.Length == 10 || isbn.Length == 13))
                throw new ArgumentException("isbn must be 10 or 13 chars");
            if (!ulong.TryParse(isbn, out _))
                throw new ArgumentException("isbn must be numeric");
        }

        ISBN = isbn;

        Author = author;
    }

    public string ISBN { get; }

    public string Author { get; }

    public decimal Price { get; private set; }

    public string? Currency { get; private set; }

    public decimal SetPrice(decimal price, string currency)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "price can't be negative");
        decimal oldValue = Price;
        Price = price;

        if (currency.Length != 3)
            throw new ArgumentException("iso currency symbol is 3 chars");
        Currency = currency;

        return oldValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Book book)
            return false;
        else
            return ISBN == book.ISBN;
    }

    public override int GetHashCode() => ISBN.GetHashCode();

    public override string ToString() => $"{(string.IsNullOrEmpty(Author) ? "" : Author + ", ")}{Title}";
}