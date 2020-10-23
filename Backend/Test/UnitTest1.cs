using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        public class Adam : dumbass { 
            public string name = "adam";
        }

        public class dumbass
        {
            public bool dumbassery = true;
        }

        public class Dont_Do {
            public static dumbass WhatIwantYouTo()
            {
                return new Adam();
            }            
        }
        public class Do
        {
            public static void WhatIWantYouToDo(Adam a)
            {

            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            Do.WhatIWantYouToDo(new Adam());
            dumbass adam = Dont_Do.WhatIwantYouTo();
            if (adam.dumbassery)
                Console.WriteLine("adam is indeed a dumbass");
        } 
    }
}
