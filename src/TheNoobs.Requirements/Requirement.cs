using System.Collections;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TheNoobs.Requirements.Abstractions;
using TheNoobs.Requirements.Exceptions;

namespace TheNoobs.Requirements;

public class Requirement : IRequirement
{
    private static readonly IRequirement _instance = new Requirement();
    private readonly Func<Exception>? _createException;

    private Requirement(Func<Exception>? createException = null)
    {
        _createException = createException;
    }

    public static IRequirement To() => _instance;

    public static IRequirement To(Func<Exception> createException)
    {
        return new Requirement(createException);
    }

    public void NotBeNull(object? obj, Func<Exception>? createException = null)
    {
        if (obj is not null)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeNull(object? obj, Func<Exception>? createException = null)
    {
        if (obj is null)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeTrue(bool condition, Func<Exception>? createException = null)
    {
        if (condition)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeFalse(bool condition, Func<Exception>? createException = null)
    {
        if (!condition)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeEmpty(string text, Func<Exception>? createException = null)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeEmpty(ICollection collection, Func<Exception>? createException = null)
    {
        if (collection.Count == 0)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void NotBeEmpty(string text, Func<Exception>? createException = null)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        throw CreateException(createException);
    }

    public void NotBeEmpty(ICollection collection, Func<Exception>? createException = null)
    {
        if (collection.Count > 0)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void Match(string value, string pattern, Func<Exception>? createException = null)
    {
        if (Regex.IsMatch(value, pattern, RegexOptions.Compiled))
        {
            return;
        }

        throw CreateException(createException);
    }

    public void NotMatch(string value, string pattern, Func<Exception>? createException = null)
    {
        if (!Regex.IsMatch(value, pattern, RegexOptions.Compiled))
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeGreaterThanOrEqualTo(int value, int compareValue, Func<Exception>? createException = null)
    {
        if (value >= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeGreaterThanOrEqualTo(long value, long compareValue, Func<Exception>? createException = null)
    {
        if (value >= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeGreaterThanOrEqualTo(decimal value, decimal compareValue,
        Func<Exception>? createException = null)
    {
        if (value >= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeGreaterThanOrEqualTo(double value, double compareValue, Func<Exception>? createException = null)
    {
        if (value >= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeGreaterThanOrEqualTo(DateTime value, DateTime compareValue,
        Func<Exception>? createException = null)
    {
        if (value >= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeLessThanOrEqualTo(int value, int compareValue, Func<Exception>? createException = null)
    {
        if (value <= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeLessThanOrEqualTo(long value, long compareValue, Func<Exception>? createException = null)
    {
        if (value <= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeLessThanOrEqualTo(decimal value, decimal compareValue, Func<Exception>? createException = null)
    {
        if (value <= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeLessThanOrEqualTo(double value, double compareValue, Func<Exception>? createException = null)
    {
        if (value <= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeLessThanOrEqualTo(DateTime value, DateTime compareValue,
        Func<Exception>? createException = null)
    {
        if (value <= compareValue)
        {
            return;
        }

        throw CreateException(createException);
    }

    public void BeUrl(string url, UriKind uriKind = UriKind.RelativeOrAbsolute, Func<Exception>? createException = null)
    {
        if (Uri.TryCreate(url, uriKind, out _))
        {
            return;
        }

        throw CreateException(createException);
    }
    
    public void BeEmail(string email, Func<Exception>? createException = null)
    {
        const string PATTERN = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$";
        try
        {
            _ = new MailAddress(email);
            if (Regex.IsMatch(email, PATTERN, RegexOptions.Compiled))
            {
                return;
            }
            throw CreateException(createException);
        }
        catch
        {
            throw CreateException(createException);
        }
    }

    private Exception CreateException(Func<Exception>? createException = null, [CallerMemberName] string? requirement = null)
    {
        return createException?.Invoke()
            ?? _createException?.Invoke()
            ?? new RequirementFailedException($"Requirement \"{requirement}\" was not fulfilled");
    }
}
