namespace Pictionary.Domain.UserModel;

using Pictionary.Domain.Models;
using Pictionary.Domain.UserModel.ValueObjects;

public sealed class User : AggregateRoot<UserId>
{
    private User(
        UserId id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string password,
        string? role,
        long version) : base(id, version)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Role = role ?? "Member";
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public static User Parse(
        UserId id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string password,
        string role,
        long version)
    {
        return new(id,firstName, lastName, phoneNumber, email, password, role, version);
    }

    public static User Create(
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string password)
    {
        return new(UserId.Create(), firstName, lastName, phoneNumber, email, password, null, 1);
    }

    public static User CreateAdmin(
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string password)
    {
        return new(UserId.Create(), firstName, lastName, phoneNumber, email, password, "Admin", 1);
    }

    #pragma warning disable CS8618
        private User()
        {
        }
    #pragma warning restore CS8618
}