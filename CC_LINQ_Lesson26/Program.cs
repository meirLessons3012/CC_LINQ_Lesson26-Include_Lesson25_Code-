using CC_LINQ_Lesson26;

List<Student> students = new List<Student>();
students.Add(new Student(41782, "Yaki", 21, 100, 1));
students.Add(new Student(4178222, "Shimi", 33, 88, 1));
students.Add(new Student(12341782, "David", 52, 50, 2));
students.Add(new Student(5234, "Ron", 33, 74, 3));
students.Add(new Student(52412334, "Rani", 29, 20, 5));

List<Course> courses = new List<Course>()
{
    new Course(1,"System",365,DateTime.Now),
    new Course(2,"Azure",55,DateTime.Now.AddDays(7)),
    new Course(3,"Java",400,DateTime.Now.AddMonths(1)),
    new Course(4, "C#",500,DateTime.Now.AddMinutes(30))
};

//Lesson 1
SelectMethod(students);
OrderAndThenMethod(students);
WhereMethod(students);

//Lesson 2
AggregationMethods(students);
SpecificElement(students);
SequenceEqual(students);
List<Student> concatTwoLists = students.Concat(students).ToList();
CreateNewCollections();
DuplicateAndSingleElements(students);
GroupsAndJoinMethod(students, courses);
ContainsAllAny(students, courses);

#region Select Methods

static void SelectMethod(List<Student> students)
{
    // long long way
    List<double> selectGrades = new List<double>();
    foreach (Student std in students)
    {
        selectGrades.Add(std.Grade);
    }

    //long way
    List<double> selectGrades2 = students.Select<Student, double>((Student std) => { return std.Grade; }).ToList();

    //shotest way
    List<double> selectGrades3 = students.Select(std => std.Grade).ToList();
    var selectGrades3_1 = students.Select(std => std.Grade);

    //special way
    List<double> selectGrades4 = students.Select(GetGradeDiv2).ToList();
    List<double> selectGrades5 = students.Select(GetGrade).ToList();
}

static double GetGrade(Student std)
{
    return std.Grade;
}

static double GetGradeDiv2(Student std)
{
    return std.Grade / 2;
}

#endregion

#region Order By And Then By Methods

static void OrderAndThenMethod(List<Student> students)
{
    Func<Student, int> myFunc = (std) => std.Age;
    List<Student> ordersByAge = students.OrderBy(myFunc).ToList();
    List<Student> ordersByAgeAndThenById = students.OrderBy(MyOrderByAgeMethod).ThenBy(std => std.Id).ToList();

    Console.WriteLine("ordersByAgeAndThenById With Lambda:");
    ordersByAgeAndThenById.ForEach(student => Console.WriteLine(student));

    Action<Student> myAct = (std) => Console.WriteLine($"From Action: {std}");
    Console.WriteLine();
    Console.WriteLine("ordersByAgeAndThenById With Action:");
    ordersByAgeAndThenById.ForEach(myAct);

    Console.WriteLine();
    Console.WriteLine("ordersByAgeAndThenById With Method:");
    ordersByAgeAndThenById.ForEach(MyForEachMethod);

}

static int MyOrderByAgeMethod(Student std)
{
    return std.Age;
}

static void MyForEachMethod(Student std)
{
    Console.WriteLine($"From Method: {std}");
}

#endregion

#region Where Method

static void WhereMethod(List<Student> students)
{
    List<Student> onlyHigherThan80OrAgeHigherFrom50 = students.Where(std => std.Grade > 85 || std.Age > 50).ToList();
    List<Student> whereByMethod = students.Where(OnlyWhereStartWithA).ToList();
    List<Student> whereByMethod2ByMyWhereMethod = MyWhereMethod(students, OnlyWhereStartWithA);
    //myStudents2 = myStudents2.Where(x => x.CourseId == 4);
    //myStudents2 = myStudents2.Where(x => x.Grade > 80 && x.CourseId == 4);
}

static bool OnlyWhereStartWithA(Student myStd)
{
    return myStd.Name.StartsWith("A");
}

static List<Student> MyWhereMethod(List<Student> students, Func<Student, bool> checkMethod)
{
    List<Student> result = new List<Student>();
    foreach (Student curStd in students)
    {
        if (checkMethod.Invoke(curStd))
            result.Add(curStd);
    }
    return result;
}

#endregion

#region Aggregation Methods

static void AggregationMethods(List<Student> students)
{
    List<int> myNums = new List<int>() { 10, 10, 10, 25, 30 };
    double listAvg = myNums.Average();
    double gradeSum = students.Sum(stud => stud.Grade);
    double maxGradeByCourse = students.Max(st => st.Grade);
    double maxGradeByCourse2 = students.Where(std => std.Age > 30).Max(st => st.Grade);
    double minGradeByCourse2 = students.Where(std => std.Age > 30).Min(st => st.Grade);

}

#endregion

#region Specific Element

static void SpecificElement(List<Student> students)
{
    try
    {


        //by index
        Student someStudentsOrDefault = students.ElementAtOrDefault(13);
        Student someStudents = students.ElementAt(13);

        //first or by condition
        Student firstStudentsOrDefaultWithoutCond = students.FirstOrDefault();
        Student firstStudentsWithoutCond = students.First();
        Student firstStudentsOrDefault = students.FirstOrDefault(std => std.Grade > 50);
        Student firstStudents = students.First(std => std.Age > 19);

        //last
        Student lastStudentsOrDefaultWithoutCond = students.LastOrDefault();
        Student lastStudentsWithoutCond = students.Last();
        Student lastStudentsOrDefault = students.LastOrDefault(std => std.Name.Contains("i"));
        Student lastStudents = students.Last(std => std.Name.Contains("i"));

        //single
        Student singleStudentsOrDefaultWithoutCond = students.SingleOrDefault();
        Student singleStudentsWithoutCond = students.Single();
        Student singleStudentsOrDefault = students.SingleOrDefault(std => std.Grade > 70);
        Student singleStudents = students.Single(std => std.Grade > 70);
    }
    catch (Exception ex)
    {
    }
}

#endregion

#region Sequence Equal

static void SequenceEqual(List<Student> students)
{
    List<int> ints = new List<int> { 1, 2, 3, 4 };
    List<int> ints2 = new List<int> { 1, 2, 3, 4, 5 };
    List<int> ints3 = new List<int> { 4, 3, 2, 1 };
    List<Student> students2 = new List<Student>();
    bool twoIntListAreEqual = ints.SequenceEqual(ints2);//by value
    bool twoIntListAreEqual2 = ints.SequenceEqual(ints3);//by value
    bool twoCoursesListAreEqual = students.SequenceEqual(students2);//by reference
}

#endregion

#region Create New Collections

static void CreateNewCollections()
{
    List<int> rangeList = Enumerable.Range(3, 10).ToList();
    List<int> repeatList = Enumerable.Repeat(3, 6).ToList();
    List<int> emptyList = Enumerable.Empty<int>().ToList();

    rangeList.ForEach(i => Console.WriteLine(i));
    repeatList.ForEach(i => Console.WriteLine(i));
    emptyList.ForEach(i => Console.WriteLine(i));
}

#endregion

#region Duplicate And Single Elements

static void DuplicateAndSingleElements(List<Student> students)
{
    List<int> distinctInts = Enumerable.Range(10, 25).ToList();
    distinctInts.AddRange(Enumerable.Range(15, 5));
    List<int> res = distinctInts.Distinct().ToList();
    var res1 = distinctInts.Except(res); //מביא את הנתונים שלא מופיעים ברשימה השנייה
    var res2 = distinctInts.Intersect(res); //מביא את רק את הנתונים שמופיעים ברשימה השנייה
    List<int> myInts1 = new List<int> { 1, 2, 3, 4, 5 };
    List<int> myInts2 = new List<int> { 4, 5, 6, 7, 8 };
    var unionList = myInts1.Union(myInts2);
}

#endregion

#region Groups And Join Methods

static void GroupsAndJoinMethod(List<Student> students, List<Course> courses)
{
    //Group By
    var groupsStudsByCourse = students.GroupBy(std => std.CourseId);

    groupsStudsByCourse.Where(grpStd => grpStd.Key < 3)
                            .ToList()
                            .ForEach(grpStd => grpStd.ToList()
                            .ForEach(std => Console.WriteLine(std)));

    foreach (var key in groupsStudsByCourse)
    {
        Console.WriteLine(key.Key);
        List<Student> stds = key.ToList();
        foreach (Student std in stds)
        {
            Console.WriteLine(std);
        }
    }

    //Join (Inner Join)
    var groupedStudentsByCourseId1 = courses.Join(students,
    course => course.Id,
    student => student.CourseId,
    (course, student) => new /*CourseStudent()*/
    {
        CourseId = course.Id,
        CourseName = course.Category,
        StudentDetails = student,
        StudentName = student.Name
    }).ToList();

    //GroupJoin - (Left join)
    var groupedStudentsByCourseId = courses.GroupJoin(students,
    course => course.Id,
    student => student.CourseId,
    (course, studentsByCourse) => new
    {
        CourseId = course.Id,
        CourseName = course.Category,
        StudentsByCourse = studentsByCourse.ToList(),
    }).ToList();

    //GroupJoin - (Left join)
    var groupedCourseByStudentId = students.GroupJoin(courses,
    std => std.CourseId,
    course => course.Id,
    (std, courseByStd) => new
    {
        CourseId = std.CourseId,
        Name = std.Name,
        CourseByStd = courseByStd.ToList(),
    }).ToList();

}

#endregion

#region Contains/All/Any

static void ContainsAllAny(List<Student> students, List<Course> courses)
{
    bool anyTrue = students.Any(s => s.Name.StartsWith("D"));//true
    bool allTrue = students.All(s => s.Name.StartsWith("D"));//false
}

#endregion