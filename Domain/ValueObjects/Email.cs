namespace Domain.ValueObjects;

public record Email
{
    public string Value { get; init; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
        {
            throw new ArgumentException("Invalid email format.");
        }
        Value = value;
    }

    public static Email Create(string value) => new(value);

}
