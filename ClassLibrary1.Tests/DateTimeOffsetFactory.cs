using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;

namespace ClassLibrary1.Tests
{
    public static class DateTimeOffsetFactory
    {

        //[PexFactoryMethod(typeof(DateTimeOffset[]))]
        //public static DateTimeOffset[] CreateArray(long[] ticks)
        //{

        //    PexAssume.IsNotNull(ticks);
        //    PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
        //    var dates = new DateTimeOffset[ticks.Length+1];
        //    foreach (var i in ticks)
        //    {
        //        dates[i] = (new DateTimeOffset(i, new TimeSpan(0)));
        //    }
        //    return dates;
        //}
    }
}
