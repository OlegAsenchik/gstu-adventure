using System.Collections.Generic;

public class Course
{
    public int CourseID { get; set; } // Primary Key
    public string CourseName { get; set; }
    public string CourseProgram { get; set; }
    public string Description { get; set; }
    public int Intensity { get; set; }
    public int GroupSize { get; set; }
    public int AvailableSeats { get; set; }
    public int TotalHours { get; set; }
    public decimal Price { get; set; }

    // Navigation Property
    public ICollection<ListenerCourse> ListenerCourses { get; set; }
}
