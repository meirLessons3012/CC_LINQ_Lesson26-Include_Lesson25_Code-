using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_LINQ_Lesson26
{
    internal class Course
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int Duration { get; set; }
        public DateTime StartAt { get; set; }

        public Course(int id, string category, int duration, DateTime startAt)
        {
            Id = id;
            Category = category;
            Duration = duration;
            StartAt = startAt;
        }
    }
}
