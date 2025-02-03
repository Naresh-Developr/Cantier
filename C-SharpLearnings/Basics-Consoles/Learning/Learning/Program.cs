using System;

namespace Learning
{
    class Test
    {
        public int Method1()
        {
            return 2;
        }

        virtual public void Method2()
        {
            Console.WriteLine("hello");
        }

    }
        class Program : Test
        {
            
         public override void Method2()
            {
                Console.WriteLine("helo");
            }

            static void Main(string[] args)
            {
                Test test = new Test();
                Console.WriteLine(test.Method1());
                Program prg = new Program();
            prg.Method2();
                        
                Console.WriteLine("Hello World!");
            }
           
        }

    
}

