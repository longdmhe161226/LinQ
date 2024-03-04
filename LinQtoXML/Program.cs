using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    public static void Main()
    {
        //XDocument xmlDocument = new XDocument(
        //    new XDeclaration("1.0", "utf-8", "yes"),

        //    new XComment("Creating an XML Tree using LINQ to XML"),

        //    new XElement("Students",

        //        new XElement("Student", new XAttribute("Id", 101),
        //            new XElement("Name", "Mark"),
        //            new XElement("Gender", "Male"),
        //            new XElement("TotalMarks", 800)),

        //        new XElement("Student", new XAttribute("Id", 102),
        //            new XElement("Name", "Rosy"),
        //            new XElement("Gender", "Female"),
        //            new XElement("TotalMarks", 900)),

        //        new XElement("Student", new XAttribute("Id", 103),
        //            new XElement("Name", "Pam"),
        //            new XElement("Gender", "Female"),
        //            new XElement("TotalMarks", 850)),

        //        new XElement("Student", new XAttribute("Id", 104),
        //            new XElement("Name", "John"),
        //            new XElement("Gender", "Male"),
        //            new XElement("TotalMarks", 950))));



        var students = new[]
        {
            new
            {
                Id = 101, Name = "Pam", Gender = false, TotalMarks = 912
            },
            new
            {
                Id = 102, Name = "Nong", Gender = true, TotalMarks = 936
            },
            new
            {
                Id = 103, Name = "John", Gender = true, TotalMarks = 721
            }
        };

        XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),

                new XComment("Creating an XML Tree using LINQ to XML"),

                new XElement("Students",

                    from student in students
                    select new XElement("Student", new XAttribute("Id", student.Id),
                                new XElement("Name", student.Name),
                                new XElement("Gender", student.Gender),
                                new XElement("TotalMarks", student.TotalMarks))
                            ));

        string path = @"D:\NCC\Training\LinQ\LinQtoXML\Data.xml";
        xmlDocument.Save(path);


        //IEnumerable<string> names = from student in XDocument.Load(path).Descendants("Student")
        //                            where (int)student.Element("TotalMarks") > 800
        //                            orderby (int)student.Element("TotalMarks") descending
        //                            select student.Element("Name").Value;

        IEnumerable<string> names = from student in XDocument.Load(path).Element("Students").Elements("Student")
                                    where (int)student.Element("TotalMarks") > 800
                                    orderby (int)student.Element("TotalMarks") descending
                                    select student.Element("Name").Value;

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }


        Console.WriteLine();
        Console.WriteLine("Before:");

        xmlDocument = XDocument.Load(path);
        Console.WriteLine(xmlDocument);

        xmlDocument.Element("Students").Add(
                new XElement("Student", new XAttribute("Id", 104),
                    new XElement("Name", "Todd"),
                    new XElement("Gender", "Male"),
                    new XElement("TotalMarks", 980)
                    ));
        xmlDocument.Element("Students").AddFirst(
                new XElement("Student", new XAttribute("Id", 100),
                    new XElement("Name", "Mong"),
                    new XElement("Gender", "Male"),
                    new XElement("TotalMarks", 920)
                    ));
        xmlDocument.Element("Students")
                    .Elements("Student")
                    .Where(x => x.Attribute("Id").Value == "103").FirstOrDefault()
                    .AddBeforeSelf(
                        new XElement("Student", new XAttribute("Id", 106),
                            new XElement("Name", "Meee"),
                            new XElement("Gender", "Male"),
                            new XElement("TotalMarks", 900)
                            ));

        Console.WriteLine("After add: ");
        Console.WriteLine(xmlDocument);

        xmlDocument.Element("Students")
                    .Elements("Student")
                    .Where(x => x.Attribute("Id").Value == "106").FirstOrDefault()
                    .SetElementValue("TotalMarks", 999);

        xmlDocument.Element("Students")
                    .Elements("Student")
                    .Where(x => x.Attribute("Id").Value == "100")
                    .Select(x => x.Element("TotalMarks")).FirstOrDefault().SetValue(1000);

        Console.WriteLine("After update: ");
        Console.WriteLine(xmlDocument);

        xmlDocument.Root.Elements().Where(x => x.Attribute("Id").Value == "106").Remove();

        Console.WriteLine("After delete: ");
        Console.WriteLine(xmlDocument);
        xmlDocument.Save(path);

        XDocument result = new XDocument
                                (new XElement("table", new XAttribute("border", 1),
                                        new XElement("thead",
                                            new XElement("tr",
                                                new XElement("th", "Name"),
                                                new XElement("th", "Gender"),
                                                new XElement("th", "TotalMarks"))),
                                        new XElement("tbody",
                                            from student in xmlDocument.Descendants("Student")
                                            select new XElement("tr",
                                                       new XElement("td", student.Element("Name").Value),
                                                       new XElement("td", student.Element("Gender").Value),
                                                       new XElement("td", student.Element("TotalMarks").Value)))));

        result.Save(@"D:\NCC\Training\LinQ\LinQtoXML\ok.html");

    }
}
