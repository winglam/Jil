using Microsoft.QualityTools.Testing.Fakes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesStuff
{
    public class DateTimeFixture : IDisposable
    {
        bool logDateTimes = true;

        IDisposable context;
        public DateTimeFixture()
        {
            context = ShimsContext.Create();

            if (!logDateTimes)
            {
                return;
            }
            // unit test code
            System.Fakes.ShimDateTime.NowGet = () =>
            {
                File.AppendAllText(@"D:\sym-flaky\stacktrace.txt", ParseStack(Environment.StackTrace) + "\n========\n");
                DateTime ret = new DateTime(1L);
                ShimsContext.ExecuteWithoutShims(() =>
                {
                    ret = DateTime.Now;
                });
                return ret;
            };

            System.Fakes.ShimDateTime.UtcNowGet = () =>
            {
                File.AppendAllText(@"D:\sym-flaky\stacktrace.txt", ParseStack(Environment.StackTrace) + "\n========\n");
                DateTime ret = new DateTime(1L);
                ShimsContext.ExecuteWithoutShims(() =>
                {
                    ret = DateTime.UtcNow;
                });
                return ret;
            };
        }

        private string ParseStack(string stacktrace)
        {
            string[] stack = stacktrace.Split('\n');
            var sb = new StringBuilder();
            foreach (var s in stack)
            {
                var sl = s.ToLower();
                if ((sl.Contains("jil") || sl.Contains("date") || sl.Contains("time")) && (!s.Contains("System.Runtime") && !s.Contains("Xunit.Sdk") && !s.Contains("System.Reflection") && !s.Contains("System.RuntimeMethodHandle")))
                {
                    sb.Append(s);
                }
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
