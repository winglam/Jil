using Jil;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary1.Tests
{
    class TimeSpanTests
    {
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

        [PexMethod(TestEmissionFilter = PexTestEmissionFilter.All, MaxBranches = 10000000, MaxRuns = 10000000, Timeout = 2400, MaxConstraintSolverTime = 30)]
        public void ISO8601TimeSpans()
        {
            File.AppendAllText(@"D:\sym - flaky\timePex.txt", "starting method: ISO8601TimeSpans time: " + DateTime.Now + "\n");
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
            File.AppendAllText(@"D:\sym-flaky\timePex.txt", "ending method: ISO8601TimeSpans time: " + DateTime.Now + "\n");
        }
    }
}
