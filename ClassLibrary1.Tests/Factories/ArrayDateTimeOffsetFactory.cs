using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Creatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests.Factories
{
    /*public static class ArrayDateTimeOffsetFactory// : IPexClassFactory<List<DateTimeOffset>>
    {
        [PexFactoryMethod(typeof(DateTimeOffset[]))]
        public static DateTimeOffset[] CreateArr([PexAssumeNotNull]long[] ticks)
        {
            
            PexAssumeEx.TrueForAll(ticks, t => DateTime.MinValue.Ticks <= t && DateTime.MaxValue.Ticks >= t);
            //PexAssume.IsTrue();
            //PexAssume.IsTrue(ticks.Length > 0);
            //PexAssume.IsTrue(ticks.Length > 2 || ticks.Length <= 2);
            DateTimeOffset[] dates = new DateTimeOffset[ticks.Length];
            for (int i = 0; i < ticks.Length; i++)
            {


                dates[i] = new DateTimeOffset(ticks[i], new TimeSpan(0));
            }
            return dates;
        }
    }*/

}
