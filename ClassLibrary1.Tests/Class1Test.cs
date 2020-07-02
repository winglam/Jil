// <copyright file="Class1Test.cs">Copyright ©  2020</copyright>
using System;
using System.Collections.Generic;
using System.IO;
using Jil;
using JilTests;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Microsoft.QualityTools.Testing.Fakes;


namespace ClassLibrary1.Tests
{
    /// <summary>This class contains parameterized unit tests for Class1</summary>
    //[PexClass(typeof(DateTimeOffset))]
    [TestClass]
    [PexClass(typeof(JSON))]
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


        //[TestMethod]
        //public void testISO8601TimeSpansFixed()
        //{
        //    //P7541099DT23H34M0S
        //    //SerializeTests s = new SerializeTests();
        //    //s.ISO8601TimeSpans(7541099, 23, 34, 0, 0, 1, 10);
        //    ISO8601TimeSpans(7541099, 23, 34, 0, 0, 1);
        //}

        //[PexMethod]
        //public void testISO8601WithOffset2(long i)
        //{
        //    PexAssume.IsTrue(i >= DateTimeOffset.MinValue.Ticks && i <= DateTimeOffset.MaxValue.Ticks);
        //    SerializeTests s = new SerializeTests();
        //    s.ISO8601WithOffset(i);
        //    ISO8601WithOffset(i);
        //}

        //[TestMethod]
        //public void testISO8601WithOffsetFixed()
        //{
        //    //SerializeTests s = new SerializeTests();
        //    //s.ISO8601WithOffset(1436051802072316100);
        //    ISO8601WithOffset(1436051802072316100);
        //}

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void ISO8601TimeSpans()
        {
            File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "starting method: ISO8601TimeSpans time: " + DateTime.Now + "\n");
            var timeSpans = new List<TimeSpan>();

            //for (var i = 0; i < 1000; i++)
            //{
            //    var d = PexChoose.Value<int>("d");
            //    var h = PexChoose.Value<int>("h");
            //    var m = PexChoose.Value<int>("m");
            //    var s = PexChoose.Value<int>("s");
            //    var ms = PexChoose.Value<int>("ms");
            //    var negate = PexChoose.Value<int>("negate");

            //    PexAssume.IsTrue(d >= 0 && d < 10675199 - 1);
            //    PexAssume.IsTrue(h >= 0 && h < 24);
            //    PexAssume.IsTrue(m >= 0 && m < 60);
            //    PexAssume.IsTrue(s >= 0 && s < 60);
            //    PexAssume.IsTrue(ms >= 0 && ms < 1000);
            //    PexAssume.IsTrue(negate >= 0 && negate < 2);

            //    var ts = new TimeSpan(d, h, m, s, ms);
            //    if (negate == 0)
            //    {
            //        ts = ts.Negate();
            //    }

            //    timeSpans.Add(ts);
            //}
            var d = PexChoose.Value<int>("d");
            var h = PexChoose.Value<int>("h");
            var m = PexChoose.Value<int>("m");
            var s = PexChoose.Value<int>("s");
            var ms = PexChoose.Value<int>("ms");
            var negate = PexChoose.Value<int>("negate");

            PexAssume.IsTrue(d >= 0 && d < 10675199 - 1);
            PexAssume.IsTrue(h >= 0 && h < 24);
            PexAssume.IsTrue(m >= 0 && m < 60);
            PexAssume.IsTrue(s >= 0 && s < 60);
            PexAssume.IsTrue(ms >= 0 && ms < 1000);
            PexAssume.IsTrue(negate >= 0 && negate < 2);

            var tsa = new TimeSpan(d, h, m, s, ms);
            if (negate == 0)
            {
                tsa = tsa.Negate();
            }

            timeSpans.Add(tsa);
            timeSpans.Add(TimeSpan.MaxValue);
            timeSpans.Add(TimeSpan.MinValue);
            timeSpans.Add(default(TimeSpan));

            foreach (var ts in timeSpans)
            {
                string streamJson, stringJson;
                using (var str = new StringWriter())
                {
                    JSON.Serialize(ts, str, Options.ISO8601);
                    streamJson = str.ToString();
                }

                {
                    stringJson = JSON.Serialize(ts, Options.ISO8601);
                }


                var dotNetStr = XmlConvert.ToString(ts);

                streamJson = streamJson.Trim('"');
                stringJson = stringJson.Trim('"');

                if (streamJson.IndexOf('.') != -1)
                {
                    var lastChar = streamJson[streamJson.Length - 1];
                    streamJson = streamJson.Substring(0, streamJson.Length - 1).TrimEnd('0') + lastChar;
                }

                if (stringJson.IndexOf('.') != -1)
                {
                    var lastChar = stringJson[stringJson.Length - 1];
                    stringJson = stringJson.Substring(0, stringJson.Length - 1).TrimEnd('0') + lastChar;
                }

                Assert.AreEqual(dotNetStr, streamJson);
                Assert.AreEqual(dotNetStr, stringJson);
            }
            File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "ending method: ISO8601TimeSpans time: " + DateTime.Now + "\n");
        }



        static long T;
        static long MyTime()
        {
            long v = PexChoose.Value<long>("x");
            PexAssume.IsTrue(v >= T);
            T = v;
            return v;
        }

        //[TestMethod]
        //public void testLoop()
        //{
        //    var starting = 1473077944704472100L;
        //    for (var t = starting; t < starting + 1000; t++)
        //    {
        //        try
        //        {
        //            ISO8601WithOffset(t);
        //        } catch (Exception e )
        //        {
        //            Console.Out.WriteLine("t: " + t);
        //        }
        //    }
        //}


        [PexMethod]
        public void test()
        {
            long v1 = PexChoose.Value<long>("x");
          
            long v2 = PexChoose.Value<long>("y");
          
            Assert.AreEqual(v1, v2);
        }

        [PexMethod]
        public void test2()
        {
            long[] a = new long[2];
            for (int i = 0; i < 2; i++)
            {
                a[i] = PexChoose.Value<long>("x"); // or maybe "x"+i ??
            }
            Console.WriteLine(a[0] + " : " + a[1]);
            Assert.AreEqual(a[0], a[1]);
        }

        //[TestMethod]
        //public void testFixedValue()
        //{
        //    ISO8601WithOffset(1436051802073161000L);
        //}

        //[TestMethod]
        //public void TestCurrentYear()
        //{
        //    // Shims can be used only in a ShimsContext:
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange:
        //        // Shim DateTime.Now to return a fixed date:
        //        System.Fakes.ShimDateTime.NowGet =
        //        () =>
        //        { return new DateTime(2000, 02, 02); };
        //        System.Fakes.ShimDateTime.UtcNowGet =
        //            () => { return new DateTime(2000, 02,02); };

        //        // Assert:
        //        // This will always be true if the component is working:
        //        Assert.AreEqual(DateTimeOffset.UtcNow, 2000);
        //    }
        //}
        //[PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        //public void TestDateTimeTimeSpan()
        //{
        //    File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "starting method: TestDateTimeTimeSpan time: " + DateTime.Now + "\n");
        //    try
        //    {
        //        using (ShimsContext.Create())
        //        {
        //            // Arrange:
        //            var year = PexChoose.Value<int>("year");
        //            var month = PexChoose.Value<int>("month");
        //            var day = PexChoose.Value<int>("day");
        //            var hour = PexChoose.Value<int>("hour");
        //            var min = PexChoose.Value<int>("min");
        //            var sec = PexChoose.Value<int>("sec");
        //            var msec = PexChoose.Value<int>("msec");

        //PexAssume.IsTrue(year >= 2020 && year <= 2025);
        //            PexAssume.IsTrue(month >= 1 && month <= 12);
        //            PexAssume.IsTrue(min >= 0 && min< 60);
        //            PexAssume.IsTrue(hour >= 0 && hour< 24);
        //            PexAssume.IsTrue(sec >= 0 && sec< 60);
        //            PexAssume.IsTrue(msec >= 0 && msec< 1000);
        //            if (month == 2)
        //            {
        //                PexAssume.IsTrue(day >= 1 && day <= 28);
        //            }
        //            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        //            {
        //                PexAssume.IsTrue(day >= 1 && day <= 31);
        //            }
        //            else
        //            {
        //                PexAssume.IsTrue(day >= 1 && day <= 30);
        //            }

        //            System.Fakes.ShimDateTime.NowGet = () =>
        //            { return new TimeSpan(day, hour, min, sec, msec); };
        //            System.Fakes.ShimDateTime.UtcNowGet = () =>
        //            { return new DateTime(year, month, day, hour, min, sec, msec); };

        //            // Assert:
        //            SerializeTests s = new SerializeTests();
        //            s.ISO8601WithOffset();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "done method: TestDateTimeTimeSpan time: " + DateTime.Now + "\n");
        //        throw e;
        //    }
        //}       

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void TestTicks()
        {
            File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "starting method: TestTicks time: " + DateTime.Now + "\n");
            try
            {
                using (ShimsContext.Create())
                {
                    // Arrange:
                    var ticks = PexChoose.Value<long>("ticks");
                    // from 07/01/2020 00:00:00 to 07/01/2025 00:00:00
                    PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 637292448000000000L);
                    //PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 638718048000000000L);

                    System.Fakes.ShimDateTime.NowGet = () =>
                        { return new DateTime(ticks); };
                    System.Fakes.ShimDateTime.UtcNowGet = () =>
                        { return new DateTime(ticks); };

                    // Assert:
                    SerializeTests s = new SerializeTests();
                    s.ISO8601WithOffset();
                }
            } catch (Exception e)
            {
                File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "done method: TestTicks time: " + DateTime.Now + "\n");
                throw e;
            }
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void TestDateTime()
        {
            File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "starting method: TestDateTime time: " + DateTime.Now + "\n");
            try
            {
                using (ShimsContext.Create())
                {
                    // Arrange:
                    var year = PexChoose.Value<int>("year");
                    var month = PexChoose.Value<int>("month");
                    var day = PexChoose.Value<int>("day");
                    var hour = PexChoose.Value<int>("hour");
                    var min = PexChoose.Value<int>("min");
                    var sec = PexChoose.Value<int>("sec");
                    var msec = PexChoose.Value<int>("msec");

                    PexAssume.IsTrue(year >= 2020 && year <= 2025);
                    PexAssume.IsTrue(month >= 1 && month <= 12);
                    PexAssume.IsTrue(min >= 0 && min < 60);
                    PexAssume.IsTrue(hour >= 0 && hour < 24);
                    PexAssume.IsTrue(sec >= 0 && sec < 60);
                    PexAssume.IsTrue(msec >= 0 && msec < 1000);
                    if (month == 2)
                    {
                        PexAssume.IsTrue(day >= 1 && day <= 28);
                    }
                    else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    {
                        PexAssume.IsTrue(day >= 1 && day <= 31);
                    }
                    else
                    {
                        PexAssume.IsTrue(day >= 1 && day <= 30);
                    }

                    System.Fakes.ShimDateTime.NowGet = () =>
                    { return new DateTime(year, month, day, hour, min, sec, msec); };
                    System.Fakes.ShimDateTime.UtcNowGet = () =>
                    { return new DateTime(year, month, day, hour, min, sec, msec); };

                    // Assert:
                    SerializeTests s = new SerializeTests();
                    s.ISO8601WithOffset();
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(@"C:\Users\winglam2\timePex.txt", "done method: TestDateTime time: " + DateTime.Now + "\n");
                throw e;
            }
        }

        [TestMethod]
        public void TestDateTimeFixed()
        {
            try
            {
                using (ShimsContext.Create())
                {
                    // Arrange:
                    var year = PexChoose.Value<int>("year");
                    var month = PexChoose.Value<int>("month");
                    var day = PexChoose.Value<int>("day");
                    var hour = PexChoose.Value<int>("hour");
                    var min = PexChoose.Value<int>("min");
                    var sec = PexChoose.Value<int>("sec");
                    var msec = PexChoose.Value<int>("msec");
                   

                    System.Fakes.ShimDateTime.NowGet = () =>
                    { return new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Utc); };
                    System.Fakes.ShimDateTime.UtcNowGet = () =>
                    { return new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Utc); };


                    // Assert:
                    SerializeTests s = new SerializeTests();
                    s.ISO8601WithOffset();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // Timeout are in seconds. 2400sec = 40min
        //[PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        //public void ISO8601WithOffset(long ticks)
        //{
        //    //long ticks = PexChoose.Value<long>("x");
        //    var toTest = new List<DateTimeOffset>();
        //    // TODO try different constructor
        //    toTest.Add(new DateTimeOffset(ticks, new TimeSpan(0, 0, 0, 0)));

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

        //    foreach (var testDto in toTest)
        //    {
        //        string shouldMatch;
        //        if (testDto.Offset == TimeSpan.Zero)
        //        {
        //            shouldMatch = "\"" + testDto.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fffffff\Z") + "\"";
        //        }
        //        else
        //        {
        //            shouldMatch = "\"" + testDto.ToString(@"yyyy-MM-ddTHH\:mm\:ss.fffffffzzz") + "\"";
        //        }
        //        var strStr = JSON.Serialize(testDto, Options.ISO8601);
        //        string streamStr;
        //        using (var str = new StringWriter())
        //        {
        //            JSON.Serialize(testDto, str, Options.ISO8601);
        //            streamStr = str.ToString();
        //        }
        //        //PexObserve.ValueForViewing("pc", PexSymbolicValue.GetPathConditionString());
        //        //Console.Out.WriteLine(PexSymbolicValue.GetPathConditionString());
        //        try
        //        {
        //            Assert.AreEqual(shouldMatch, strStr);
        //            File.AppendAllText(@"C:\Users\winglam2\pass.txt", "pass: " +PexSymbolicValue.GetPathConditionString() + "\n");
        //        }
        //        catch (Exception e)
        //        {
        //            File.AppendAllText(@"C:\Users\winglam2\fail.txt", "fail: " + PexSymbolicValue.GetPathConditionString() + "\n");
        //            throw e;
        //        }
        //        //PexObserve.ValueForViewing("pc", PexSymbolicValue.GetPathConditionString());
        //        //Console.Out.WriteLine(PexSymbolicValue.GetPathConditionString());
        //        Assert.AreEqual(shouldMatch, streamStr);
        //    }
        //}

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000)]
        public void test([PexAssumeUnderTest] long d)
        {
            var s = d.ToString();
            int l = s.Length;
            Assert.AreEqual("00", s.Substring(l - 2, 2));
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000)]
        public void testISO8601WithOffsetFactory([PexAssumeNotNull]List<DateTimeOffset> dates)
        {
            ISO8601WithOffsetFactoryList(dates);
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
                if ( testDto.Ticks != 1436051802072316100)
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
