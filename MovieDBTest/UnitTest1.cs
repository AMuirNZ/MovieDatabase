using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieDatabase;


namespace MovieDBTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Database myDatabase = new Database();

            int Actual = myDatabase.FeeCalculation(2000, 2017);
            Assert.AreEqual(2, Actual);

        }

        [TestMethod]
        public void TestMethod2()
        {
            Database myDatabase = new Database();

            string Actual = myDatabase.AddMovie("PG", "Cats", "2005", "Cats are good", "Cats" );

            Assert.AreEqual("Movie Entered", Actual);

        }

       


    }
}
