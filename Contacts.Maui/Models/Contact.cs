using System;

namespace Contacts.Maui.Models;

public class Contact
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public string? Avatar { get; set; } // Path or URL to avatar image
    public bool IsFavorite { get; set; } // Mark as favorite contact

}
