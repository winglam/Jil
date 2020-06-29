// <copyright file="Class1Test.cs">Copyright ©  2020</copyright>
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using Jil;
using JilTests;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Using;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: PexInstrumentAssembly(typeof(DateTimeOffset))]
[assembly: PexInstrumentAssembly(typeof(JSON))]
[assembly: PexInstrumentAssembly(typeof(SerializeTests))]

namespace ClassLibrary1.Tests
{
    /// <summary>This class contains parameterized unit tests for Class1</summary>
    [PexClass(typeof(JSON))]
    [TestClass]
    public partial class Class1Test
    {
        //static long lastTick = 1593209090645;
        //[TestMethod]
        //public void testISO8601WithOffset(long i)
        //{
        //    PexAssume.IsTrue(i >= lastTick);
        //    lastTick = i;
        //    SerializeTests s = new SerializeTests();
        //    s.ISO8601WithOffset(i);
        //}

        //[PexMethod(MaxConstraintSolverTime = 10)]
        //public void testISO8601TimeSpans(int d, int h, int m, int sec, int ms, int negate, int num)
        //{
        //    PexAssume.IsTrue(d >= 0 && d < 10675199 - 1);
        //    PexAssume.IsTrue(h >= 0 && h < 24);
        //    PexAssume.IsTrue(m >= 0 && m < 60);
        //    PexAssume.IsTrue(sec >= 0 && sec < 60);
        //    PexAssume.IsTrue(ms >= 0 && ms < 1000);
        //    PexAssume.IsTrue(num >= 0);
        //    PexAssume.IsTrue(negate >= 0 && negate < 2);
        //    SerializeTests s = new SerializeTests();
        //    s.ISO8601TimeSpans(d,h,m,sec,ms,negate, num);
        //}


        [TestMethod]
        public void testISO8601TimeSpansFixed()
        {
            //P7541099DT23H34M0S
            SerializeTests s = new SerializeTests();
            s.ISO8601TimeSpans(7541099, 23, 34, 0, 0, 1, 10);
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 100000000)]
        public void test([PexAssumeUnderTest] long d)
        {
            var s = d.ToString();
            int l = s.Length;
            Assert.AreEqual("00", s.Substring(l - 2, 2));
        }

        [TestMethod]
        public void fixedTest()
        {
            test(12340);
        }

        [PexMethod]
        public void testISO8601WithOffset2(long i)
        {
            //PexAssume.IsTrue(i >= DateTimeOffset.MinValue.Ticks && i <= DateTimeOffset.MaxValue.Ticks);
            //SerializeTests s = new SerializeTests();
            //s.ISO8601WithOffset(i);
            ISO8601WithOffset(i);
        }

        [TestMethod]
        public void testISO8601WithOffsetFixed()
        {
            //SerializeTests s = new SerializeTests();
            //s.ISO8601WithOffset(1436051802072316100);
            ISO8601WithOffset(1436051802072316100);
        }

        public void ISO8601WithOffset(long ticks)
        {
            var toTest = new List<DateTimeOffset>();
            var tdo = new DateTimeOffset(ticks, new TimeSpan(0, 0, 0, 0));
            toTest.Add(tdo);
            //toTest.Add(DateTimeOffset.Now);
            PexAssert.ReachEventually();

            for (var h = 0; h <= 14; h++)
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
            }

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

        public void ISO8601WithOffsetFactory(DateTimeOffset[] toTest)
        {
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
                if (toTest.Length > 0 && toTest[0].Ticks != 0)
                {
                    Console.WriteLine("hi");
                }
                else
                {
                    throw new Exception("bye");
                }
                Assert.AreEqual(shouldMatch, strStr);
                Assert.AreEqual(shouldMatch, streamStr);
            }
        }

        public void ISO8601WithOffsetFactoryList(List<DateTimeOffset> toTest)
        {
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
                if (toTest.Count > 0 && toTest[0].Ticks != 0)
                {
                    Console.WriteLine("hi");
                }
                else
                {
                    throw new Exception("bye");
                }
                Assert.AreEqual(shouldMatch, strStr);
                Assert.AreEqual(shouldMatch, streamStr);
            }
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All)]
        [PexUseType(typeof(DateTimeOffset[]))]
        public void testISO8601WithOffsetFactoryArr([PexAssumeUnderTest] DateTimeOffset[] dates)
        {
            PexAssume.IsNotNull(dates);
            PexAssume.IsTrue(dates.Length > 0);
            if (dates[0].Ticks > 1436051802072316100)
            {
                throw new Exception("foo");
            }
            ISO8601WithOffsetFactory(dates);
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 100000000)]
        [PexUseType(typeof(List<DateTimeOffset>))]
        public void testISO8601WithOffsetFactory([PexAssumeUnderTest] List<DateTimeOffset> dates)
        {
            PexAssume.IsNotNull(dates);
            PexAssume.IsTrue(dates.Count > 0);
            if (dates[0].Ticks > 1436051802072316100)
            {
                throw new Exception("foo");
            }
            ISO8601WithOffsetFactoryList(dates);
        }

        //[PexFactoryMethod(typeof(List<DateTimeOffset>))]
        //public static List<DateTimeOffset> Create(List<long> ticks)
        //{
        //    var dates = new List<DateTimeOffset>();
        //    foreach (var i in ticks)
        //    {
        //        dates.Add(new DateTimeOffset(i, new TimeSpan(0)));
        //    }
        //    return dates;
        //}

        [PexMethod]
        public void Des(int t)
        {
            JSON.TestEven(t);
        }

        //public void factoryMethodISO8601Offset()
        //{
        //    var toTest = new List<DateTimeOffset>();
        //    var tdo = new DateTimeOffset(ticks, new TimeSpan(0, 0, 0, 0));
        //    toTest.Add(tdo);
        //    //toTest.Add(DateTimeOffset.Now);

        //    if (ticks == 0)
        //    {
        //        PexAssert.IsTrue(toTest != null);
        //    }

        //    for (var h = 0; h <= 14; h++)
        //    {
        //        for (var m = 0; m < 60; m++)
        //        {
        //            if (h == 0 && m == 0) continue;
        //            if (h == 14 && m > 0) continue;

        //            var offsetPos = new TimeSpan(h, m, 0);
        //            var offsetNeg = offsetPos.Negate();

        //            var now = new DateTime(636639847357871686);
        //            now = DateTime.SpecifyKind(now, DateTimeKind.Unspecified);

        //            toTest.Add(new DateTimeOffset(now, offsetPos));
        //            toTest.Add(new DateTimeOffset(now, offsetNeg));
        //        }
        //    }
        //}

        //[TestMethod]
        //public void testISO8601WithOffsetRand()
        //{
        //    var r = new Random(1);
        //    for (var j = 0; j < 100; j++)
        //    {
        //        long ra = LongRandom(DateTimeOffset.MinValue.Ticks, DateTimeOffset.MaxValue.Ticks, r);
        //        //PexAssume.IsTrue(i >= DateTimeOffset.MinValue.Ticks && i <= DateTimeOffset.MaxValue.Ticks);
        //        Console.WriteLine(ra);
        //        SerializeTests s = new SerializeTests();
        //        s.ISO8601WithOffset(ra);
        //    }
        //}

        //long LongRandom(long min, long max, Random rand)
        //{
        //    byte[] buf = new byte[8];
        //    rand.NextBytes(buf);
        //    long longRand = BitConverter.ToInt64(buf, 0);
        //    return (Math.Abs(longRand % (max - min)) + min);
        //}
    }
}
