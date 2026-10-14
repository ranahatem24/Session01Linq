namespace Session01Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region var vs dynamic

            //var name = "Salma";                  // compiler infers: string
            //name = "Mohamed";                    // still a string
            //									 //name = 99;                         // compile error — cannot convert int to string

            //Console.WriteLine(name.GetType());   // System.String
            //Console.WriteLine(name.Length);

            ////var x;                             // compile error — must be initialized at declaration
            ////var y = null;                      // compile error — compiler cannot infer a type from null


            //dynamic value;                       // no initialization needed
            //value = 42;
            //Console.WriteLine(value.GetType());  // System.Int32

            //value = true;
            //Console.WriteLine(value.GetType());  // System.Boolean

            //value = new Customer { CustomerName = "Ali" };
            //Console.WriteLine(value.GetType());  // Customer

            //Console.WriteLine(value.CustomerName);   // resolved at runtime 

            //value = "Ahmed";
            //Console.WriteLine(value.CustomerName);   // compiles fine , resolved at runtime throws RuntimeBinderException at runtime

            #endregion

            #region Anonymous Types

            //var employee1 = new { Id = 1, Name = "Ahmed", Salary = 5000 };

            //Console.WriteLine(employee1);            // ToString() is overridden → { Id = 1, Name = Ahmed, Salary = 5000 }
            //Console.WriteLine(employee1.GetType());  // compiler-generated name, e.g. <>f__AnonymousType0`3[...]


            //var employee2 = new { Id = 2, Name = "Ahmed", Salary = 6000 };
            //// Same property names + same property types + same ORDER → compiler reuses one type
            //Console.WriteLine(employee1.GetType() == employee2.GetType());   // True — same type, different values

            //// Different property names (case-sensitive) → different type
            //var employee3 = new { id = 4, name = "Omar", salary = 7000 };
            //Console.WriteLine(employee1.GetType() == employee3.GetType());   // False

            //// Same names but different order → different type
            //var employee4 = new { Id = 3, Salary = 7000, Name = "Omar" };
            //Console.WriteLine(employee1.GetType() == employee4.GetType());   // False

            //// ===== Equality =====
            //// Equals() and GetHashCode() are overridden → compare by VALUES
            //// == is not overridden → compares REFERENCES

            //var employee1Copy = new { Id = 1, Name = "Ahmed", Salary = 5000 };
            //Console.WriteLine(employee1.Equals(employee1Copy));   // True  — same values
            //Console.WriteLine(employee1 == employee1Copy);        // False — two different objects in memory

            //// ===== Immutability =====
            //// All properties are read-only (get only)
            ////employee1.Name = "Ali";   // compile error — property is read-only

            //// To "change" a value, we create a NEW object:

            //// Way 1: projection initializers — property names are taken from the source (Id, Salary)
            //var employee5 = new { employee4.Id, employee4.Salary, Name = "Salma" };
            //Console.WriteLine(employee5);
            //Console.WriteLine(employee5.GetType() == employee4.GetType());   // True — same names, types and order

            //// Way 2: 'with' expression (C# 10+) — copies the object and changes only what we specify
            //var employee6 = employee4 with { Name = "Salma" };
            //Console.WriteLine(employee6);
            //Console.WriteLine(employee4);            // original object is unchanged


            #endregion

            #region Query Syntax vs Method Syntax

            //var students = new List<Student>
            //                      {
            //                      	new() { Name = "Sara",  Grade = 90, Major = "CS"   },
            //                      	new() { Name = "Omar",  Grade = 75, Major = "Math" },
            //                      	new() { Name = "Layla", Grade = 88, Major = "CS"   },
            //                      	new() { Name = "Karim", Grade = 60, Major = "CS"   },
            //                      	new() { Name = "Nour",  Grade = 95, Major = "Math" },
            //                      };

            ///*
            //    Select  s.Name , s.Grade
            //    from Students s 
            //    where s.Grade >= 80 and s.Major = 'CS'
            //    Order By s.Grade Desc
            // */			

            //// ===== Query Syntax (Query Expression) =====
            //// SQL-like keywords. Must start with 'from' and end with 'select' or 'group by'.

            //var queryResult = from s in students                        // s = range variable (each student)
            //				  where s.Grade >= 80 && s.Major == "CS"    // filter
            //				  orderby s.Grade descending                // sort
            //				  select new { s.Name, s.Grade };           // project into an anonymous type

            //// ===== Method Syntax (Fluent Syntax) =====
            //// Extension methods chained together, each one takes a lambda expression.

            //var methodResult = students
            //	.Where(s => s.Grade >= 80 && s.Major == "CS")           // filter
            //	.OrderByDescending(s => s.Grade)                        // sort
            //	.Select(s => new { s.Name, s.Grade });                  // project into an anonymous type

            //// Both are the SAME: query syntax is syntactic sugar,
            //// the compiler translates it into the method calls above.

            //Console.WriteLine("Query Syntax:");
            //foreach (var item in queryResult)
            //	Console.WriteLine($"{item.Name} => {item.Grade}");

            //Console.WriteLine("Method Syntax:");
            //foreach (var item in methodResult)
            //	Console.WriteLine($"{item.Name} => {item.Grade}");

            //// Output (same for both):
            //// Sara => 90
            //// Layla => 88
            #endregion

            #region Deferred vs Immediate Execution

            //var numbers = new List<int> { 1, 2, 3, 4, 5 };

            //// ===== DEFERRED Execution =====

            //var result01 = numbers.Where(n => n % 2 == 0);

            //numbers.Add(6);                           // list changed AFTER defining the query

            //// Execution happens HERE, when we enumerate the query
            //foreach (var num in result01)
            //	Console.WriteLine(num);               // 2 4 6 — sees the 6, because it runs now

            //// The query runs again from scratch on EVERY enumeration
            //numbers.Add(10);
            //foreach (var num in result01)
            //	Console.WriteLine(num);               // 2 4 6 10

            //// ===== IMMEDIATE Execution =====

            //var result02 = numbers.Where(n => n % 2 == 0).ToList();

            //numbers.Add(8);                           // list changed AFTER execution
            //foreach (var num in result02)
            //	Console.WriteLine(num);               // 2 4 6 10 — no 8, the snapshot was already taken

            //// ===== Which operators are which? =====
            //// Deferred:  operators that return IEnumerable<T>
            ////            Where, Select, OrderBy, Take, Skip, Distinct ...
            //// Immediate: operators that return a single value or a ready collection
            ////            ToList, ToArray, ToDictionary, Count, Sum, Max, First, Single, Any ...
            #endregion

            #region Filtering - Restriction

            #region Where

            //// Out of stock products
            //var outOfStock = ProductList.Where(p => p.UnitsInStock == 0);       // method syntax

            //outOfStock = from p in ProductList                                  // query syntax — same result
            //			 where p.UnitsInStock == 0
            //			 select p;

            //foreach (var product in outOfStock)
            //	Console.WriteLine(product);

            //// In stock AND price > $20 — combine conditions with && (or ||)
            //var inStockExpensive = ProductList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 20m);

            //inStockExpensive = from p in ProductList
            //				   where p.UnitsInStock > 0 && p.UnitPrice > 20m
            //				   select p;

            //foreach (var product in inStockExpensive)
            //	Console.WriteLine(product);
            #endregion

            #region Indexed Where
            //// Overload that also gives us the INDEX of each element (zero-based position in the source).
            //// Available in method syntax ONLY — no query syntax equivalent.

            //// Out of stock products among the first 20 products (index 0 → 19)
            //var outOfStockInFirst20 = ProductList.Where((p, i) => p.UnitsInStock == 0 && i < 20);

            //foreach (var product in outOfStockInFirst20)
            //	Console.WriteLine(product);
            #endregion

            #region OfType<>
            //// Filters elements by their TYPE and returns IEnumerable<T>.
            //// where Where() is not available because the elements are stored as object.

            //ArrayList mixed = new() { 1, "hello", 3.14, "world", true, "linq" };

            //var strings = mixed.OfType<string>();    // keeps strings only, skips everything else

            //foreach (var str in strings)
            //	Console.WriteLine(str);              // hello world linq

            ////var casted = mixed.Cast<string>().ToList();   // runtime error
            //// OfType<T>() vs Cast<T>():
            //// OfType skips elements that are not T,
            //// Cast throws InvalidCastException on the first element that is not T.
            #endregion

            #endregion#region var vs dynamic
        }
    }
}
