using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CC_LINQ_Lesson26
{
    internal class Student : IEqualityComparer<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public int CourseId { get; set; }

        public Student(int id, string name, int age, double grade, int courseId)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
            CourseId = courseId;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public bool Equals(Student? x, Student? y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return obj.GetHashCode();
        }
    }
}
