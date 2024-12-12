using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace SystemTests
{
    [TestClass]
    public class UnitTest1
        //tests if a student can enter their status
    {
        [TestMethod]
        public void TestStudentStatusEntry()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("1\nJohn Doe\n3\n2\nHad a good day\n1\n2024-12-15\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                var result = sw.ToString();
                Assert.IsTrue(result.Contains("Your status has been saved, hopeful yo see you really soon, goodbye and have a nive day :))."));
            }
        }

        [TestMethod]
        public void TestPSAccess()
        {
            //test if the PT can acess the system
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("2\n12345\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                var result = sw.ToString();
                Assert.IsTrue(result.Contains("Access granted."));
            }
        }

        [TestMethod]
        public void TestSTAccess()
        {
            //test if the senior tutor can acess the system
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("3\n67890\n"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                var result = sw.ToString();
                Assert.IsTrue(result.Contains("Access granted."));
            }
        }

        [TestMethod]
        public void TestDataPersistence()
            // test if data has been saved
        {
            string testData = "John Doe\nok\nHad a good day\nyes\n2024-12-15 at 12pm to 1pm with Nia Lee\n";
            File.WriteAllText("info.txt", testData);

            string data = File.ReadAllText("info.txt");
            Assert.IsTrue(data.Contains("John Doe"));
            Assert.IsTrue(data.Contains("ok"));
            Assert.IsTrue(data.Contains("Had a good day"));
            Assert.IsTrue(data.Contains("2024-12-15 at 12pm to 1pm with Nia Lee"));
        }

        [TestMethod]
        public void TestPSReviewStudentStatuses()
        {
            string testData = "John Doe\nok\nHad a good day\nyes\n2024-12-15 at 12pm to 1pm with Nia Lee\n";
            File.WriteAllText("info.txt", testData);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("2\n12345\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                var result = sw.ToString();
                Assert.IsTrue(result.Contains("John Doe"));
                Assert.IsTrue(result.Contains("ok"));
                Assert.IsTrue(result.Contains("Had a good day"));
                Assert.IsTrue(result.Contains("2024-12-15 at 12pm to 1pm with Nia Lee"));
            }
        }

        [TestMethod]
        public void TestPSBookMeetingWithStudent()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader("2\n12345\n2\nJohn Doe\n2024-12-15\n1\n"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                var result = sw.ToString();
                Assert.IsTrue(result.Contains("Meeting has been booked.thanking for booking with us. "));
            }
        }
    }
}