namespace LinQExs2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list1 = new[] { new { id = 1, name = "a" }, new { id = 2, name = "b" }, new { id = 3, name = "c" } };
            var list2 = new[] { new { id = 2, name = "b" }, new { id = 4, name = "d" }, new { id = 5, name = "e" } };

            var list3 = list1.Intersect(list2);
            var list4 = list1.Except(list2);

            foreach (var item in list3)
            {
                Console.WriteLine(item);
            }

            foreach (var item in list4)
            {
                Console.Write(item);
            }
        }
    }

}
