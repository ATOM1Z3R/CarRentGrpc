using Domain.Enums;

namespace Domain.Models;

public class Employee : Person
{
    public EmployeeType Role { get; set; } = EmployeeType.Marketing;
    
    public string Password { get; set; } = string.Empty;

    public byte[] Salt { get; set; } = new byte[] {};
}
