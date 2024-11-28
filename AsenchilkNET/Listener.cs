using System;
using System.Collections.Generic;

public class Listener
{
    public int ListenerID { get; set; } // Primary Key
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string PassportData { get; set; }

    // Navigation Properties
    public ICollection<ListenerCourse> ListenerCourses { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public Debtor Debtor { get; set; } // One-to-One relationship
}
