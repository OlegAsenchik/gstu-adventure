using System;

public class ListenerCourse
{
    public int ListenerCourseID { get; set; } // Primary Key
    public int ListenerID { get; set; } // Foreign Key
    public int CourseID { get; set; } // Foreign Key
    public DateTime EnrollmentDate { get; set; }

    // Navigation Properties
    public Listener Listener { get; set; }
    public Course Course { get; set; }
}
