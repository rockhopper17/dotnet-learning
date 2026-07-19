using System;

namespace Classes;

public enum PublicationType { Misc, Book, Magazine, Article };

public abstract class Publication
{
    // ---------------------------------
    // constants
    // ---------------------------------

    // ---------------------------------
    // fields
    // ---------------------------------
    private bool _published = false;
    private DateTime _datePublished;
    private int _totalPages;

    // ---------------------------------
    // properties
    // ---------------------------------
    public string Publisher { get; }
    public string Title { get; }
    public PublicationType Type { get; }
    public string? CopyrightName { get; private set; }
    public int CopyrightDate { get; private set; }

    public int Pages
    {
        get { return _totalPages; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "number of pages cannot be zero or negative");
            _totalPages = value;
        }
    }

    // ---------------------------------
    // constructors
    // ---------------------------------
    public Publication(string title, string publisher, PublicationType type)
    {
        if (string.IsNullOrWhiteSpace(publisher))
            throw new ArgumentException("publisher required");
        Publisher = publisher;

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("title required");
        Title = title;

        Type = type;
    }

    // ---------------------------------
    // methods
    // ---------------------------------
    public string GetPublicationDate()
    {
        if (!_published)
            return "NYP";
        else
            return _datePublished.ToString("d");
    }

    public void Publish(DateTime datePublished)
    {
        _published = true;
        _datePublished = datePublished;
    }

    public void Copyright(string copyrightName, int copyrightDate)
    {
        if (string.IsNullOrWhiteSpace(copyrightName))
            throw new ArgumentException("name of copyright holder requird");
        CopyrightName = copyrightName;

        int currentYear = DateTime.Now.Year;
        if (copyrightDate < currentYear - 10 || copyrightDate > currentYear + 2)
            throw new ArgumentException($"copyright year must be between {currentYear - 10} and {currentYear + 1}");
        CopyrightDate = copyrightDate;
    }

    public override string ToString() => Title;
}
