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

        //[TestMethod]
        //public void testFixedValue()
        //{
        //    ISO8601WithOffset(1436051802073161000L);
        //}

        //static DateTime GetTime(bool useUTC)
        //{
        //    var ticks = PexChoose.Value<long>("ticks");
        //    // from 07/01/2020 00:00:00 to 07/01/2025 00:00:00
        //    PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 638718048000000000L);
        //    //PexAssume.IsTrue(ticks > DateTime.MinValue.Ticks && ticks < DateTime.MaxValue.Ticks && ticks >= T);
        //    T = ticks;
        //    DateTimeKind kind;
        //    if (useUTC)
        //    {
        //        kind = DateTimeKind.Utc;
        //    }
        //    else
        //    {
        //        kind = DateTimeKind.Local;
        //    }
        //    return new DateTime(ticks);
        //}

        //static long T;
        //[PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 60, MaxConditions = 10000000)]
        //public void TestTicks()
        //{
        //    File.AppendAllText(@"D:\sym-flaky\timePex.txt", "starting method: TestTicks time: " + DateTime.Now + "\n");
        //    T = -1;
        //    try
        //    {
        //        using (ShimsContext.Create())
        //        {
        //            System.Fakes.ShimDateTime.NowGet = () => GetTime(false);
        //            //System.Fakes.ShimDateTime.UtcNowGet = () => GetTime(true);

        //            // Assert:
        //            var s = new SerializeTests();
        //            s.ISO8601WithOffset();
        //        }
        //    } catch (Exception e)
        //    {
        //        File.AppendAllText(@"D:\sym-flaky\timePex.txt", "done method: TestTicks time: " + DateTime.Now + "\n");
        //        throw e;
        //    }
        //}

        // 2400 = 40m
        // 600 = 10m
        // 3600 = 60m
        // 21600 = 6h
        //long fileTick = DateTime.Now.Ticks;
        ////, MaxWorkingSet = 700
        //[PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 21600, MaxConstraintSolverTime = 60, MaxConditions = 10000000)]
        //public void TestTicks()
        //{
        //    var fileName = $"D:\\sym-flaky\\timePex-{fileTick}-5yr-exc-6h.txt";
        //    File.AppendAllText(fileName, "starting method: TestTicks time: " + DateTime.Now + "\n");
        //    try
        //    {
        //        using (ShimsContext.Create())
        //        {
        //            // Arrange:
        //            var ticks = PexChoose.Value<long>("ticks");

        //            // from 07/01/2020 00:00:00 to 07/01/2025 00:00:00
        //            PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 638718048000000000L);

        //            // from 07/01/2020 00:00:00 to 07/02/2020 00:00:00
        //            //PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 637292448000000000L);

        //            // Min ticks which is 1/1/0001 12:00:00 AM and Max ticks which is 12/31/9999 23:59:59 PM
        //            //PexAssume.IsTrue(ticks >= DateTime.MinValue.Ticks && ticks <= DateTime.MaxValue.Ticks);

        //            System.Fakes.ShimDateTime.NowGet = () =>
        //            { return new DateTime(ticks); };
        //            //System.Fakes.ShimDateTime.UtcNowGet = () =>
        //            //{ return new DateTime(ticks, DateTimeKind.Utc); };

        //            // Assert:
        //            SerializeTests s = new SerializeTests();
        //            s.ISO8601WithOffset();
        //            //s.ISO8601WithOffsetNoLoop();
        //        }
        //        File.AppendAllText(fileName, "test pass time: " + DateTime.Now + "\n");
        //    }
        //    catch (Exception e)
        //    {
        //        File.AppendAllText(fileName, "test fail time: " + DateTime.Now + "\n");
        //        throw e;
        //    }
        //}

        long fileTick = DateTime.Now.Ticks;
        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 600, MaxConstraintSolverTime = 60, MaxConditions = 10000000)]
        public void TestTicks(long ticks)
        {
            var fileName = $"D:\\sym-flaky\\timePex-{fileTick}-5yr-exc-10m-put.txt";
            File.AppendAllText(fileName, "starting method: TestTicks time: " + DateTime.Now + "\n");
            try
            {
                using (ShimsContext.Create())
                {
                    // from 07/01/2020 00:00:00 to 07/01/2025 00:00:00
                    //PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 638718048000000000L);

                    // from 07/01/2020 00:00:00 to 07/02/2020 00:00:00
                    PexAssume.IsTrue(ticks > 637139520000000000L && ticks < 637292448000000000L);

                    // Min ticks which is 1/1/0001 12:00:00 AM and Max ticks which is 12/31/9999 23:59:59 PM
                    //PexAssume.IsTrue(ticks >= DateTime.MinValue.Ticks && ticks <= DateTime.MaxValue.Ticks);

                    System.Fakes.ShimDateTime.NowGet = () =>
                    { return new DateTime(ticks); };
                    //System.Fakes.ShimDateTime.UtcNowGet = () =>
                    //{ return new DateTime(ticks, DateTimeKind.Utc); };

                    // Assert:
                    SerializeTests s = new SerializeTests();
                    //s.ISO8601WithOffsetPUT(ticks);
                    s.ISO8601WithOffsetNoLoop();
                }
                File.AppendAllText(fileName, "test pass time: " + DateTime.Now + "\n");
            }
            catch (Exception e)
            {
                File.AppendAllText(fileName, "test fail time: " + DateTime.Now + "\n");
                throw e;
            }
        }

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void TestDateTime()
        {
            File.AppendAllText(@"D:\sym-flaky\timePex.txt", "starting method: TestDateTime time: " + DateTime.Now + "\n");
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
                    { return new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Local); };
                    System.Fakes.ShimDateTime.UtcNowGet = () =>
                    { return new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Utc); };

                    // Assert:
                    SerializeTests s = new SerializeTests();
                    //s.ISO8601WithOffset();
                    s.ISO8601WithOffset();
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(@"D:\sym-flaky\timePex.txt", "done method: TestDateTime time: " + DateTime.Now + "\n");
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
    }
}
