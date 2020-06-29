using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Creatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests
{
    public static class ListFactory// : IPexClassFactory<List<DateTimeOffset>>
    {
        //[PexFactoryMethod(typeof(List<DateTimeOffset>))]
        //public static List<DateTimeOffset> Create(List<long> ticks)
        //{
        //    PexAssume.IsNotNull(ticks);
        //    PexAssume.IsTrue(ticks.Count > 0);
        //    var dates = new List<DateTimeOffset>();
        //    foreach (var i in ticks)
        //    {
        //        dates.Add(new DateTimeOffset(i, new TimeSpan(0)));
        //    }
        //    return dates;
        //}

        [PexFactoryMethod(typeof(List<DateTimeOffset>))]
        public static List<DateTimeOffset> Create(long[] ticks)
        {
            PexAssume.IsNotNull(ticks);
            PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
            var dates = new List<DateTimeOffset>();
            foreach (var i in ticks)
            {
                dates.Add(new DateTimeOffset(i, new TimeSpan(0)));
            }
            return dates;
        }

        //[PexFactoryMethod(typeof(DateTimeOffset[]))]
        //public static DateTimeOffset[] Create(long[] ticks)
        //{
        //    PexAssume.IsNotNull(ticks);
        //    //PexAssume.IsTrue(ticks.Length > 0);
        //    PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
        //    var dates = new DateTimeOffset[ticks.Length];
        //    for (int i = 0; i < ticks.Length; i++)
        //    {
        //        dates[i] = new DateTimeOffset(ticks[i], new TimeSpan(0));
        //    }
        //    return dates;
        //}

        //[PexFactoryMethod(typeof(DateTimeOffset))]
        //public static DateTimeOffset Create(long ticks)
        //{
        //    PexAssume.IsNotNull(ticks);
        //    PexAssume.IsTrue(ticks > 0);
        //    return new DateTimeOffset(ticks, new TimeSpan(0));
        //}

        //[PexFactoryMethod(typeof(List<DateTimeOffset>))]
        //public static List<DateTimeOffset> Create(List<long> ticks)
        //{
        //    PexAssume.IsNotNull(ticks);
        //    PexAssume.IsTrue(ticks.Count > 0);
        //    var dates = new List<DateTimeOffset>();
        //    foreach (var tick in ticks)
        //    {
        //        dates.Add(new DateTimeOffset(tick, new TimeSpan(0)));
        //    }
        //    return dates;
        //}
    }
}
