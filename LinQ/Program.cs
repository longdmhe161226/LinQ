using System.Xml.Linq;

namespace LinQ
{

    public class Class
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ClassID { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Data
            List<Class> classes = new List<Class>();
            List<Student> students = new List<Student>();
            classes.AddRange(new[] { new Class { ID = 1, Name = "c1" },
                new Class { ID = 2, Name = "c2" },
                new Class { ID = 3, Name = "c3" } });
            students.AddRange(new[] { new Student { ID = 11, Name = "st1", ClassID = 1 },
                new Student { ID = 12, Name = "st2", ClassID = 1 } ,
                new Student { ID = 13, Name = "st3", ClassID = 2 } ,
                new Student { ID = 14, Name = "st3", ClassID = 4 } });
            #endregion

            #region clolection 1
            var result = (classes.Select(classItem =>
            students.Select(student =>
                                new
                                {
                                    StudentID = student.ID,
                                    ClassID = classItem.ID
                                })));


            foreach (var item in result)
            {
                foreach (var student in item)
                {
                    Console.WriteLine(student.StudentID + "\t" + student.ClassID);
                }
            }

            //cach 2
            Console.WriteLine("cach 2");
            var result2 = classes.SelectMany(classItem =>
            students.Select(student =>
                                new
                                {
                                    StudentID = student.ID,
                                    ClassID = classItem.ID
                                }));

            foreach (var item in result2)
            {
                Console.WriteLine(item.StudentID + "\t" + item.ClassID);
            }

            var ok = new[] {
                new { Id = 1,
                    Name = new[] {
                        new{ id = 11, Name =
                                            new {
                                                Id = 111,
                                                Name = new[] {"123","234" } } },
                        new{ id = 12, Name =
                                            new {
                                                Id = 112,
                                                Name = new[] {"123","234" } } } } },
                new { Id = 2,
                        Name = new[] {
                            new {id = 22, Name =
                                                new {
                                                    Id = 222,
                                                    Name = new[] {"345","456" } } } } }
            };

            foreach (var item in ok.SelectMany((p) => p.Name))
            {
                Console.WriteLine(item);
            }

            //cach 3

            Console.WriteLine("cach 3");
            var result3 = from classItem in classes
                          join student in students on 1 equals 1
                          select new { StudentID = student.ID, ClassID = classItem.ID };

            //var result3 = classes.Join(students, c => 1, s => 1,
            //    (classItem, student) => new { StudentID = student.ID, ClassID = classItem.ID });

            foreach (var item in result3)
            {
                Console.WriteLine(item.StudentID + "\t" + item.ClassID);
            }

            //cach 4
            Console.WriteLine("cach 4");
            //var result4 = from classItem in classes
            //              join student in students on 1 equals 1 into grouped
            //              from item in grouped
            //              select new { StudentID = item.ID, ClassID = classItem.ID };

            var result4 = classes.GroupJoin(students, c => 1, s => 1, (classItem, grouped) =>
            {
                return new
                {
                    Students = grouped,
                    ClassID = classItem.ID
                };

            });

            foreach (var item in result4)
            {
                foreach (var student in item.Students)
                {
                    Console.WriteLine(student.ID + "\t" + item.ClassID);
                }
            }


            Console.WriteLine("left join");

            //left join

            //var result5 = students.GroupJoin(classes, s => s.ClassID, c => c.ID, (s, c) => new { StudentID = s.ID, Classes = c })
            //    .SelectMany(sc => sc.Classes.DefaultIfEmpty(), (x, y) => new { StudentID = x.StudentID, ClassID = y != null ? y.ID : 0 })
            //    .Select(result => new
            //    {
            //        StudentID = result.StudentID,
            //        ClassID = result.ClassID,
            //    });

            var result5 = from s in students
                          join c in classes on s.ClassID equals c.ID into classGroup
                          from cg in classGroup.DefaultIfEmpty()
                          select new
                          {
                              StudentID = s.ID,
                              ClassID = cg != null ? cg.ID : 0
                          };

            foreach (var item in result5)
            {
                Console.WriteLine(item.StudentID + "\t" + item.ClassID);
            }

            #endregion

            #region colection 2
            Console.WriteLine("class name, count student:");

            var query = from classItem in classes
                        join studentItem in students on classItem.ID equals studentItem.ClassID into studentGroup
                        select new
                        {
                            ClassName = classItem.Name,
                            CountStuden = studentGroup.Count(),
                        };

            //var query = classes.GroupJoin(students, c => c.ID, s => s.ClassID, (classItem, grouped) => new
            //{
            //    ClassName = classItem.Name,
            //    CountStudent = grouped.Count(),
            //});

            //var query = from c in classes
            //            join s in students on c.ID equals s.ClassID into classGroup
            //            from cg in classGroup.DefaultIfEmpty()
            //            group cg by cg.ClassID into resultGroup
            //            select new
            //            {
            //                ClassName = classes.FirstOrDefault(x => x.ID == resultGroup.Key)?.Name,
            //                StudentCount = resultGroup.Count(s => s != null)
            //            };


            //var query = classes.GroupJoin(students, c => c.ID, s => s.ClassID, (c, classGroup) => new { ClassItem = c, ClassGroup = classGroup })
            //                   .SelectMany(x => x.ClassGroup.DefaultIfEmpty(), (x, student) => new { ClassItem = x.ClassItem, Student = student })
            //                   .GroupBy(x => x.ClassItem.ID)
            //                   .Select(group => new
            //                   {
            //                       ClassName = group.FirstOrDefault()?.ClassItem.Name,
            //                       StudentCount = group.Count(s => s.Student != null)
            //                   });

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region colection 3
            Console.WriteLine("class Id, count student:");

            //var query2 = from c in classes
            //             join s in students on c.ID equals s.ClassID into classGroup
            //             from cg in classGroup
            //             group cg by c.ID into resultGroup
            //             select new
            //             {
            //                 ClassID = resultGroup.Key,
            //                 StudentCount = resultGroup.Count()
            //             };

            var query2 = classes.GroupJoin(students, c => c.ID, s => s.ClassID, (c, classGroup) => new { ClassItem = c, ClassGroup = classGroup })
                              .SelectMany(x => x.ClassGroup, (x, student) => new { ClassItem = x.ClassItem, Student = student })
                              .GroupBy(x => x.ClassItem.ID)
                              .Select(group => new
                              {
                                  ClassID = group.Key,
                                  StudentCount = group.Count(s => s.Student != null)
                              });


            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region colection 4
            Console.WriteLine("class name, count student:");

            //var query3 = from c in classes
            //             join s in students on c.ID equals s.ClassID into classGroup
            //             from cg in classGroup
            //             group cg by c.ID into resultGroup
            //             select new
            //             {
            //                 ClassName = classes.FirstOrDefault(x => x.ID == resultGroup.Key)?.Name,
            //                 StudentCount = resultGroup.Count()
            //             };

            var query3 = classes.GroupJoin(students, c => c.ID, s => s.ClassID, (c, classGroup) => new { ClassItem = c, ClassGroup = classGroup })
                              .SelectMany(x => x.ClassGroup, (x, student) => new { ClassItem = x.ClassItem, Student = student })
                              .GroupBy(x => x.ClassItem.ID)
                              .Select(group => new
                              {
                                  ClassName = group.FirstOrDefault()?.ClassItem.Name,
                                  StudentCount = group.Count(s => s.Student != null)
                              });


            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }

            #endregion


            #region colection 5
            Console.WriteLine("class name, name of first students:");

            var query4 = from c in classes
                         join s in students on c.ID equals s.ClassID into classGroup
                         from cg in classGroup.DefaultIfEmpty()
                         group cg by c.ID into resultGroup
                         select new
                         {
                             ClassName = classes.FirstOrDefault(x => x.ID == resultGroup.Key)?.Name,
                             StudentName = resultGroup.FirstOrDefault() != null ? resultGroup.FirstOrDefault()?.Name : "null"
                         };

            //var query4 = classes.GroupJoin(students, c => c.ID, s => s.ClassID, (c, classGroup) => new { ClassItem = c, ClassGroup = classGroup })
            //                  .SelectMany(x => x.ClassGroup.DefaultIfEmpty(), (x, student) => new { ClassItem = x.ClassItem, Student = student })
            //                  .GroupBy(x => x.ClassItem.ID)
            //                  .Select(group => new
            //                  {
            //                      ClassName = group.FirstOrDefault()?.ClassItem.Name,
            //                      StudentName = group.FirstOrDefault()?.Student != null ? group.FirstOrDefault()?.Student.Name : "null"
            //                  });


            foreach (var item in query4)
            {
                Console.WriteLine(item);
            }

            #endregion
        }
    }
}
