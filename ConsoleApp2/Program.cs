using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class TestClass
    {
        double d, f;

        public TestClass(double d, double f)
        {
            this.d = d;
            this.f = f;
        }

        public double Sum()
        {
            return d + f;
        }

        public void Info()
        {
            //Console.WriteLine($"d={d}, f={f}");
            Console.WriteLine("d={0}, f={1}", d, f);
        }

        public override string ToString()
        {
            return "TestClass";
        }
    }

    public class Reflect
    {
        public static void MethodReflectionInfo<T> (T obj) 
            where T: class
        {
            Type t = typeof(T);

            MethodInfo[] miArr = t.GetMethods();
            Console.WriteLine("Список методов: ");
            foreach (MethodInfo m in miArr)
            {
                Console.WriteLine(" --> "+m.Name+" \t"+m.ReturnType);
                ParameterInfo[] parms = m.GetParameters();
                foreach (ParameterInfo p in parms)
                {
                    Console.WriteLine(p.ParameterType.Name+" "+p.Name);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Assembly armAssembly = Assembly.LoadFrom(@"C:\Users\ГерценЕ\Downloads\Новая папка\armtek\bin\Debug\armtek.dll");

            Type[] ts = armAssembly.GetTypes();


            Console.WriteLine("---------------");
            Type w = ts.FirstOrDefault(f=>f.IsClass && f.Name== "artmteckWork");
            if(w!=null)
            {
                Object obj = Activator.CreateInstance(w);

                MethodInfo getData = obj.GetType().GetMethod("GetData");
                object[] prm = new object[] { };
                object obj_ = new object();
                var result = getData.Invoke(obj, null);
            }


            foreach (Type item in ts)
            {
                if(item.IsClass && item.Name.Equals("artmteckWork"))
                {
                    Console.WriteLine("Name: {0}; FullName: {1}",
                        item.Name, item.FullName);

                    //PropertyInfo[] prop = item.GetProperties();
                    //foreach (PropertyInfo p in prop)
                    //{
                    //    Console.WriteLine("-> "+p.Name);
                    //}

                    Object work = Activator.CreateInstance(item);
                    foreach (var m in work.GetType()
                        .GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    {
                        Console.WriteLine("{0}, {1}", m.Name, m.ReturnType);
                    }

                }
            }
        }
    }
}
