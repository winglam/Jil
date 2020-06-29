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

        /*[PexFactoryMethod(typeof(List<DateTimeOffset>))]
        //public static List<DateTimeOffset> Create([PexAssumeNotNull]long[] ticks, int h, int m)
        public static List<DateTimeOffset> Create([PexAssumeNotNull]long[] ticks)
        {
            PexAssumeEx.TrueForAll(ticks, t => DateTime.MinValue.Ticks <= t && DateTime.MaxValue.Ticks >= t);

            //PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
            var dates = new List<DateTimeOffset>();
            foreach (var i in ticks)
            {
                //dates.Add(new DateTimeOffset(i, new TimeSpan(h,m,0)));
                dates.Add(new DateTimeOffset(i, new TimeSpan(0)));
            }
            return dates;
        }*/

        [PexFactoryMethod(typeof(List<DateTimeOffset>))]
        //public static List<DateTimeOffset> Create([PexAssumeNotNull]long[] ticks, int h, int m)
        public static List<DateTimeOffset> CreateUserDefined(long tick)
        {
            PexAssume.IsTrue(DateTime.MinValue.Ticks <= tick && DateTime.MaxValue.Ticks >= tick);
            var tdo = new DateTimeOffset(tick, new TimeSpan(0, 0, 0, 0));
            
            //PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
            var dates = new List<DateTimeOffset>();
            dates.Add(tdo);
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

                    dates.Add(new DateTimeOffset(now, offsetPos));
                    dates.Add(new DateTimeOffset(now, offsetNeg));
                }
            }
            return dates;
        }



        




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

        /*[PexFactoryMethod(typeof(DateTimeOffset))]
        public static DateTimeOffset CreateSingle(long ticks)
        {

            var date = new DateTimeOffset(ticks, new TimeSpan(0));

            return date;
        }*/
    }
}
