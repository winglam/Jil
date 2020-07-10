using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Settings;
using Jil;
using System.Collections.Generic;
using System.IO;
//using JilTests;

namespace Jill.Tests.Ang
{
    [TestClass]
    [PexClass(typeof(JSON))]
    public class JillTestAng
    {
        [TestMethod]
        public void TestMethod1Failing()
        {
            PUT_ISO8601WithOffset(1473077944704472100L);
        }

        [TestMethod]
        public void TestMethod1Passing()
        {
            PUT_ISO8601WithOffset(1473077944704472101L);
        }

        [TestMethod]
        public void TestMethod1Passing1()
        {
            PUT_ISO8601WithOffset(1473077944704472110L);
        }
        [TestMethod]
        public void TestMethod1Passing2()
        {
            PUT_ISO8601WithOffset(1473077944704470001L);
        }
        [TestMethod]
        public void TestMethod1Passing3()
        {
            PUT_ISO8601WithOffset(1473077944704471110L);
        }
        //Modified 
        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void PUT_ISO8601WithOffset(long ticks)
        {
            var toTest = new List<DateTimeOffset>();
            var tdo = new DateTimeOffset(ticks, new TimeSpan(0, 0, 0, 0));
            //toTest.Add(tdo);
            //toTest.Add(DateTimeOffset.Now); // original code
            toTest.Add(tdo);
            /*for (var h = 0; h <= 14; h++)
            {
                for (var m = 0; m < 60; m++)
                {
                    if (h == 0 && m == 0) continue;
                    if (h == 14 && m > 0) continue;

                    var offsetPos = new TimeSpan(h, m, 0);
                    var offsetNeg = offsetPos.Negate();

                    var now = new DateTime(636639847357871686);
                    now = DateTime.SpecifyKind(now, DateTimeKind.Unspecified);

                    toTest.Add(new DateTimeOffset(now, offsetPos));
                    toTest.Add(new DateTimeOffset(now, offsetNeg));
                }
            }*/

            foreach (var testDto in toTest)
            {
                string shouldMatch;
                if (testDto.Offset == TimeSpan.Zero)
                {
                    shouldMatch = "\"" + testDto.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fffffff\Z") + "\"";
                }
                else
                {
                    shouldMatch = "\"" + testDto.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fffffffzzz") + "\"";
                }
                var strStr = JSON.Serialize(testDto, Options.ISO8601);
                string streamStr;
                using (var str = new StringWriter())
                {
                    JSON.Serialize(testDto, str, Options.ISO8601);
                    streamStr = str.ToString();
                }

                Assert.AreEqual(shouldMatch, strStr);
                Assert.AreEqual(shouldMatch, streamStr);
            }
        }
    }

}
