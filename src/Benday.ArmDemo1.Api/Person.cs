namespace Benday.ArmDemo1.Api;

public class Person
{
    public Person()
    {
        FirstName = string.Empty;
        LastName= string.Empty;
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
