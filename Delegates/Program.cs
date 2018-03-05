using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    static class Ext
    {
        public static int Gematria(this string s)
        {
            return s.Select(c => Char.ToUpper(c) - 'A' + 1)
                    .Sum();
        }
    }
    class Program
    {
        delegate void FooSignature(int x, double d4325); // delegate type

        static void Foo(int x, double y)
        {
            Console.WriteLine($"{x}, {y}");
        }

        static void ForEach(IEnumerable<Tuple<int, double> > list, FooSignature action)
        {
            foreach (var elt in list)
            {
                action(elt.Item1, elt.Item2);
            }
        }

        delegate double Function(double d);
        static double Avg(IEnumerable<double> dList, Function f)
        {
            double total = 0;
            foreach (var elt in dList)
            {
                total += f(elt);
            }
            return total / dList.Count();
        }
        static void Main2(string[] args)
        {
            FooSignature delegateVariable;

            delegateVariable = Foo;

            Foo(2, Math.PI);

            delegateVariable(20, Math.E);

            var list = new List< Tuple<int,double> >();
            list.Add(new Tuple<int, double>(3, 5.7));
            list.Add(new Tuple<int, double>(31, 53.7));
            ForEach(list, Foo);

            var dList = new double[] {1, 2, 3, 4, 5, 6, 7, 89};
            Console.WriteLine(Avg(dList, Math.Cos));
            Console.WriteLine(Avg(dList, Math.Sin));
            Console.WriteLine(Avg(dList, Math.Sinh));
            Console.WriteLine(Avg(dList, x => x*x));

            delegateVariable = (x, y) => Console.WriteLine(x + y);
                // method takes 2 args, performs this action

            delegateVariable(3, 4.4);

            Function f;
            f = arg1 => arg1 * arg1 * arg1;
            // no parentheses for single arg, multiplemargs required

            f = (double arg1) =>
                                {
                                    return arg1 * arg1 * arg1;
                                };
            // double foobar(arg1) { return arg1 * arg1 * arg1;}
            Console.WriteLine(f(10));
            Console.ReadKey();
        }
        class Junk {
            public string Name{ get; set; }
        }


        public static void Main3(string[] args)
        {
            var anonClassInstance = new
            {
                Name = "Bob",
                Id = 613,
                List = new [] {0,1,1,2,3,5,8}
            };

            Console.WriteLine(anonClassInstance);
            Console.WriteLine(new Junk{Name = "Sam"}); // no ToiString
            Console.ReadLine();
        }

        public static void Main(string[] args)
        {
            string sentence = "the quick brown fox jumps over the lazy dog";
            // Split the string into individual words to create a collection.  
            string[] words = sentence.Split(' ');

            var wordCounts = words.Select(w => w.Length).ToArray();
            for (int i=0;i<words.Length;i++)
            {
                Console.WriteLine($"{wordCounts[i]} : {words[i]}");

            }
            Console.ReadLine();

            foreach (var word in words.Select(w => w + w))
            {
                Console.WriteLine(word);
            }
            foreach (var gem in words.Select(w => w.Gematria()))
            {
                Console.WriteLine(gem);
            }

            Console.WriteLine(words.Sum(w => w.Gematria()));
            
            Console.ReadLine();
        }
        public static void Main4(string[] args)
        {
            string sentence = "the quick brown fox jumps over the lazy dog";
            // Split the string into individual words to create a collection.  
            string[] words = sentence.Split(' ');

            // Using query expression syntax.  
            // LINQ for Objects (vs LINQ for Entities)
            //var query = from word in words
            //    group word.ToUpper() by word.Length into gr
            //    orderby gr.Key 
            //    select new { Length = gr.Key, Words = gr };

            // Using method-based query syntax.  
            var query2 = words.
                GroupBy(w => w.Length, w => w.ToUpper()).
                OrderBy(g => g.Key).
                Select(g => new { Length = g.Key, Words = g });
            // OrderBy(o => o.Length);

            var query3 = words.
                Select( w => w.ToUpper()).
                GroupBy(w => w.Length).
                OrderBy(g => g.Key).
                Select(g => new { Length = g.Key, Words = g });

            foreach (var obj in query2.ToList() )
            {
                Console.WriteLine(obj.GetType().FullName);
                Console.WriteLine("Words of length {0}:", obj.Length);
                foreach (string word in obj.Words)
                    Console.WriteLine(word);
            }

            Console.ReadKey();
        }
    }
}
