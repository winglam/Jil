﻿using System;
using Jil.Common;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Jil.Deserialize;
using Xunit;

namespace JilTests
{
    public class UtilsTests
    {
#pragma warning disable 0649
        class _FieldOffsetsInMemory
        {
            public int Foo;
            public string Bar;
            public double Fizz;
            public decimal Buzz;
            public char Hello;
            public object[] World;
        }
#pragma warning restore 0649

        [Fact]
        public void FieldOffsetsInMemory()
        {
            Func<string, FieldInfo> get = str => typeof(_FieldOffsetsInMemory).GetField(str);

            var offset = Utils.FieldOffsetsInMemory(typeof(_FieldOffsetsInMemory));

            Assert.NotNull(offset);
            Assert.True(offset.ContainsKey(get("Foo")));
            Assert.True(offset.ContainsKey(get("Bar")));
            Assert.True(offset.ContainsKey(get("Fizz")));
            Assert.True(offset.ContainsKey(get("Buzz")));
            Assert.True(offset.ContainsKey(get("Hello")));
            Assert.True(offset.ContainsKey(get("World")));
        }

#pragma warning disable 0649
        class _PropertyFieldUsage
        {
            private string _Foo;
            public string Foo
            {
                get
                {
                    return _Foo;
                }
            }

            private int _Scaler;
            public int SomeProp
            {
                get
                {
                    var x = int.Parse(_Foo);

                    var y = Console.ReadLine();

                    var sum = x + int.Parse(y);

                    return sum * _Scaler;
                }
            }
        }
#pragma warning restore 0649

#if !NETCOREAPP1_0
        // dot net core doesn't give us the reflection bits we need to implement this
        //   so disable the test until it does
        [Fact]
        public void PropertyFieldUsage()
        {
            var use = Utils.PropertyFieldUsage(typeof(_PropertyFieldUsage));

            Assert.NotNull(use);
            Assert.Single(use[typeof(_PropertyFieldUsage).GetProperty("Foo")]);
            Assert.Equal(typeof(_PropertyFieldUsage).GetField("_Foo", BindingFlags.NonPublic | BindingFlags.Instance), use[typeof(_PropertyFieldUsage).GetProperty("Foo")][0]);

            Assert.Equal(2, use[typeof(_PropertyFieldUsage).GetProperty("SomeProp")].Count);
            Assert.Equal(typeof(_PropertyFieldUsage).GetField("_Foo", BindingFlags.NonPublic | BindingFlags.Instance), use[typeof(_PropertyFieldUsage).GetProperty("SomeProp")][0]);
            Assert.Equal(typeof(_PropertyFieldUsage).GetField("_Scaler", BindingFlags.NonPublic | BindingFlags.Instance), use[typeof(_PropertyFieldUsage).GetProperty("SomeProp")][1]);
        }
#endif

        private static string CapacityEstimatorToString<T>(Action<TextWriter, T, int> act, T data)
        {
            using (var str = new StringWriter())
            {
                act(str, data, 0);

                return str.ToString();
            }
        }

#if !NETCOREAPP1_0
        class _ConstantProperties
        {
            public enum ByteEnum : byte
            {
                A = 127
            }

            public enum SByteEnum : sbyte
            {
                A = -3
            }

            public enum ShortEnum : short
            {
                A = 1891
            }

            public enum UShortEnum : ushort
            {
                A = 12381
            }

            public enum IntEnum : int
            {
                A = 1238123
            }

            public enum UIntEnum : uint
            {
                A = 9128123
            }

            public enum LongEnum : long
            {
                A = 1381261112332
            }

            public enum ULongEnum : ulong
            {
                A = 128971891
            }

            public char C1 { get { return ' '; } }
            public char C2 { get { return '"'; } }

            public string STR1 { get { return null; } }
            public string STR2 { get { return "hello world"; } }
            public string STR3 { get { return "\r\n\f"; } }

            public bool BOOL1 { get { return true; } }
            public bool BOOL2 { get { return false; } }

            public byte B1 { get { return 0; } }
            public byte B2 { get { return 127; } }
            public byte B3 { get { return 255; } }

            public sbyte SB1 { get { return -128; } }
            public sbyte SB2 { get { return 0; } }
            public sbyte SB3 { get { return 127; } }

            public short S1 { get { return short.MinValue; } }
            public short S2 { get { return 0; } }
            public short S3 { get { return short.MaxValue; } }

            public ushort US1 { get { return 0; } }
            public ushort US2 { get { return ushort.MaxValue / 2; } }
            public ushort US3 { get { return ushort.MaxValue; } }

            public int I1 { get { return int.MinValue; } }
            public int I2 { get { return 0; } }
            public int I3 { get { return int.MaxValue; } }

            public uint UI1 { get { return 0; } }
            public uint UI2 { get { return uint.MaxValue / 2; } }
            public uint UI3 { get { return uint.MaxValue; } }

            public long L1 { get { return long.MinValue; } }
            public long L2 { get { return 0; } }
            public long L3 { get { return long.MaxValue; } }

            public ulong UL1 { get { return 0; } }
            public ulong UL2 { get { return ulong.MaxValue / 2; } }
            public ulong UL3 { get { return ulong.MaxValue; } }
            public ulong UL4 { get { return ulong.MaxValue - 1; } }

            public float F1 { get { return -1234.56f; } }
            public float F2 { get { return 0; } }
            public float F3 { get { return 1234.56f; } }

            public double D1 { get { return -1234.56; } }
            public double D2 { get { return 0; } }
            public double D3 { get { return 1234.56; } }

            public ByteEnum BE { get { return ByteEnum.A; } }
            public SByteEnum SBE { get { return SByteEnum.A; } }
            public ShortEnum SE { get { return ShortEnum.A; } }
            public UShortEnum USE { get { return UShortEnum.A; } }
            public IntEnum IE { get { return IntEnum.A; } }
            public UIntEnum UIE { get { return UIntEnum.A; } }
            public LongEnum LE { get { return LongEnum.A; } }
            public ULongEnum ULE { get { return ULongEnum.A; } }
        }

        [Fact]
        public void ConstantProperties()
        {
            Assert.Equal("\" \"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("C1"), false));
            Assert.Equal("\"\\\"\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("C2"), false));

            Assert.Equal("null", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("STR1"), false));
            Assert.Equal("\"hello world\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("STR2"), false));
            Assert.Equal(@"""\r\n\f""", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("STR3"), false));

            Assert.Equal("true", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("BOOL1"), false));
            Assert.Equal("false", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("BOOL2"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("B1"), false));
            Assert.Equal("127", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("B2"), false));
            Assert.Equal("255", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("B3"), false));

            Assert.Equal("-128", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("SB1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("SB2"), false));
            Assert.Equal("127", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("SB3"), false));

            Assert.Equal("-32768", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("S1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("S2"), false));
            Assert.Equal("32767", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("S3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("US1"), false));
            Assert.Equal("32767", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("US2"), false));
            Assert.Equal("65535", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("US3"), false));

            Assert.Equal("-2147483648", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("I1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("I2"), false));
            Assert.Equal("2147483647", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("I3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UI1"), false));
            Assert.Equal("2147483647", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UI2"), false));
            Assert.Equal("4294967295", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UI3"), false));

            Assert.Equal("-9223372036854775808", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("L1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("L2"), false));
            Assert.Equal("9223372036854775807", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("L3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UL1"), false));
            Assert.Equal("9223372036854775807", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UL2"), false));
            Assert.Equal("18446744073709551615", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UL3"), false));
            Assert.Equal("18446744073709551614", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UL4"), false));

            Assert.Equal("-1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("F1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("F2"), false));
            Assert.Equal("1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("F3"), false));

            Assert.Equal("-1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("D1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("D2"), false));
            Assert.Equal("1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("D3"), false));

            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("BE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("SBE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("SE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("USE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("IE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("UIE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("LE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantProperties).GetProperty("ULE"), false));
        }
#endif

        class _ConstantFields
        {
            public enum ByteEnum : byte
            {
                A = 127
            }

            public enum SByteEnum : sbyte
            {
                A = -3
            }

            public enum ShortEnum : short
            {
                A = 1891
            }

            public enum UShortEnum : ushort
            {
                A = 12381
            }

            public enum IntEnum : int
            {
                A = 1238123
            }

            public enum UIntEnum : uint
            {
                A = 9128123
            }

            public enum LongEnum : long
            {
                A = 1381261112332
            }

            public enum ULongEnum : ulong
            {
                A = 128971891
            }

            public const char C1 = ' ';
            public const char C2 = '"';

            public const string STR1 = null;
            public const string STR2 = "hello world";
            public const string STR3 = "\r\n\f";

            public const bool BOOL1 = true;
            public const bool BOOL2 = false;

            public const byte B1 = 0;
            public const byte B2 = 127;
            public const byte B3 = 255;

            public const sbyte SB1 = -128;
            public const sbyte SB2 = 0;
            public const sbyte SB3 = 127;

            public const short S1 = short.MinValue;
            public const short S2 = 0;
            public const short S3 = short.MaxValue;

            public const ushort US1 = 0;
            public const ushort US2 = ushort.MaxValue / 2;
            public const ushort US3 = ushort.MaxValue;

            public const int I1 = int.MinValue;
            public const int I2 = 0;
            public const int I3 = int.MaxValue;

            public const uint UI1 = 0;
            public const uint UI2 = uint.MaxValue / 2;
            public const uint UI3 = uint.MaxValue;

            public const long L1 = long.MinValue;
            public const long L2 = 0;
            public const long L3 = long.MaxValue;

            public const ulong UL1 = 0;
            public const ulong UL2 = ulong.MaxValue / 2;
            public const ulong UL3 = ulong.MaxValue;

            public const float F1 = -1234.56f;
            public const float F2 = 0;
            public const float F3 = 1234.56f;

            public const double D1 = -1234.56;
            public const double D2 = 0;
            public const double D3 = 1234.56;

            public const ByteEnum BE = ByteEnum.A;
            public const SByteEnum SBE = SByteEnum.A;
            public const ShortEnum SE = ShortEnum.A;
            public const UShortEnum USE = UShortEnum.A;
            public const IntEnum IE = IntEnum.A;
            public const UIntEnum UIE = UIntEnum.A;
            public const LongEnum LE = LongEnum.A;
            public const ULongEnum ULE = ULongEnum.A;
        }

        [Fact]
        public void ConstantFields()
        {
            Assert.Equal("\" \"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("C1"), false));
            Assert.Equal("\"\\\"\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("C2"), false));

            Assert.Equal("null", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("STR1"), false));
            Assert.Equal("\"hello world\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("STR2"), false));
            Assert.Equal(@"""\r\n\f""", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("STR3"), false));

            Assert.Equal("true", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("BOOL1"), false));
            Assert.Equal("false", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("BOOL2"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("B1"), false));
            Assert.Equal("127", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("B2"), false));
            Assert.Equal("255", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("B3"), false));

            Assert.Equal("-128", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("SB1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("SB2"), false));
            Assert.Equal("127", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("SB3"), false));

            Assert.Equal("-32768", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("S1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("S2"), false));
            Assert.Equal("32767", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("S3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("US1"), false));
            Assert.Equal("32767", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("US2"), false));
            Assert.Equal("65535", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("US3"), false));

            Assert.Equal("-2147483648", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("I1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("I2"), false));
            Assert.Equal("2147483647", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("I3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UI1"), false));
            Assert.Equal("2147483647", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UI2"), false));
            Assert.Equal("4294967295", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UI3"), false));

            Assert.Equal("-9223372036854775808", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("L1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("L2"), false));
            Assert.Equal("9223372036854775807", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("L3"), false));

            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UL1"), false));
            Assert.Equal("9223372036854775807", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UL2"), false));
            Assert.Equal("18446744073709551615", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UL3"), false));

            Assert.Equal("-1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("F1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("F2"), false));
            Assert.Equal("1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("F3"), false));

            Assert.Equal("-1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("D1"), false));
            Assert.Equal("0", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("D2"), false));
            Assert.Equal("1234.56", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("D3"), false));

            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("BE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("SBE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("SE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("USE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("IE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("UIE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("LE"), false));
            Assert.Equal("\"A\"", Jil.Common.ExtensionMethods.GetConstantJSONStringEquivalent(typeof(_ConstantFields).GetField("ULE"), false));
        }

#if !NETCOREAPP1_0 && !DEBUG

#pragma warning disable 0649
        class _IsConstant
        {
            public bool ConstBoolProp { get { return true; } }
            public bool NonConstBoolProp { get { return bool.Parse("False"); } }
            public const bool ConstBoolField = true;
            public bool NonConstBoolField;

            public char ConstCharProp { get { return 'c'; } }
            public char NonConstCharProp { get { return char.Parse("c"); } }
            public const char ConstCharField = 'c';
            public char NonConstCharField;

            public string ConstStringProp { get { return "hello world"; } }
            public string NonConstStringProp { get { return 3.ToString(); } }
            public const string ConstStringField = "fizz buzz";
            public string NonConstStringField;

            public byte ConstByteProp { get { return 10; } }
            public byte NonConstByteProp { get { return byte.Parse("2"); } }
            public const byte ConstByteField = 255;
            public byte NonConstByteField;

            public sbyte ConstSByteProp { get { return 10; } }
            public sbyte NonConstSByteProp { get { return sbyte.Parse("2"); } }
            public const sbyte ConstSByteField = 127;
            public sbyte NonConstSByteField;

            public short ConstShortProp { get { return 10; } }
            public short NonConstShortProp { get { return short.Parse("2"); } }
            public const short ConstShortField = 10000;
            public short NonConstShortField;

            public ushort ConstUShortProp { get { return 10; } }
            public ushort NonConstUShortProp { get { return ushort.Parse("2"); } }
            public const ushort ConstUShortField = 10000;
            public ushort NonConstUShortField;

            public int ConstIntProp { get { return 10; } }
            public int NonConstIntProp { get { return int.Parse("2"); } }
            public const int ConstIntField = 456;
            public int NonConstIntField;

            public uint ConstUIntProp { get { return 10; } }
            public uint NonConstUIntProp { get { return uint.Parse("2"); } }
            public const uint ConstUIntField = 456;
            public uint NonConstUIntField;

            public long ConstLongProp { get { return 10L; } }
            public long NonConstLongProp { get { return long.Parse("2"); } }
            public const long ConstLongField = 456;
            public long NonConstLongField;

            public ulong ConstULongProp { get { return 10UL; } }
            public ulong NonConstULongProp { get { return ulong.Parse("2"); } }
            public const ulong ConstULongField = 456;
            public ulong NonConstULongField;

            public float ConstFloatProp { get { return 123; } }
            public float NonConstFloatProp { get { return float.Parse("2"); } }
            public const float ConstFloatField = 456;
            public float NonConstFloatField;

            public double ConstDoubleProp { get { return 123; } }
            public double NonConstDoubleProp { get { return double.Parse("2"); } }
            public const double ConstDoubleField = 456;
            public double NonConstDoubleField;
        }
#pragma warning restore 0649

        [Fact]
        public void IsConstant()
        {
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstBoolProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstBoolProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstBoolField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstBoolField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstCharProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstCharProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstCharField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstCharField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstStringProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstStringProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstStringField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstStringField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstByteProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstByteProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstByteField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstByteField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstSByteProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstSByteProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstSByteField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstSByteField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstShortProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstShortProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstShortField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstShortField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstUShortProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstUShortProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstUShortField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstUShortField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstIntProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstIntProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstIntField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstIntField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstUIntProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstUIntProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstUIntField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstUIntField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstLongProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstLongProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstLongField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstLongField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstULongProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstULongProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstULongField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstULongField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstFloatProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstFloatProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstFloatField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstFloatField").Single()));

            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstDoubleProp").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstDoubleProp").Single()));
            Assert.True(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("ConstDoubleField").Single()));
            Assert.False(Jil.Common.ExtensionMethods.IsConstant(typeof(_IsConstant).GetMember("NonConstDoubleField").Single()));
        }
#endif

        class _FindRecursiveTypes1
        {
            public _FindRecursiveTypes1 A { get; set; }
            public string B { get; set; }
        }

        class _FindRecursiveTypes2
        {
            public class _FindRecursiveTypes3
            {
                public _FindRecursiveTypes3 A { get; set; }
                public string B { get; set; }
            }

            public _FindRecursiveTypes3 A { get; set; }
            public string B { get; set; }
        }

        class _FindRecursiveTypes4
        {
            public class _FindRecursiveTypes5
            {
                public _FindRecursiveTypes4 A { get; set; }
                public int B { get; set; }
            }

            public class _FindRecursiveTypes6
            {
                public _FindRecursiveTypes4 A { get; set; }
                public _FindRecursiveTypes5 B { get; set; }
                public _FindRecursiveTypes6 C { get; set; }
            }

            public _FindRecursiveTypes5 A { get; set; }
            public int B { get; set; }
            public _FindRecursiveTypes6 C { get; set; }
        }

        [Fact]
        public void FindRecursiveTypes()
        {
            var t1 = Utils.FindRecursiveTypes(typeof(_FindRecursiveTypes1)).ToList();
            Assert.Single(t1);
            Assert.Equal(typeof(_FindRecursiveTypes1), t1[0]);

            var t2 = Utils.FindRecursiveTypes(typeof(_FindRecursiveTypes2)).ToList();
            Assert.Single(t2);
            Assert.Equal(typeof(_FindRecursiveTypes2._FindRecursiveTypes3), t2[0]);

            var t3 = Utils.FindRecursiveTypes(typeof(_FindRecursiveTypes4)).ToList();
            Assert.Equal(2, t3.Count);
            Assert.Contains(t3, t => t == typeof(_FindRecursiveTypes4));
            Assert.Contains(t3, t => t == typeof(_FindRecursiveTypes4._FindRecursiveTypes6));
        }

        public class _Outer<T>
        {
            public List<T> items { get; internal set; }
        }

        class _Inner
        {
            public List<_Inner> sub_options { get; set; }
        }

        [Fact]
        public void FindRecursiveTypes_ListList()
        {
            var recurs = Utils.FindRecursiveTypes(typeof(_Outer<_Inner>)).ToList();

            Assert.Single(recurs);
            Assert.Equal(typeof(_Inner), recurs[0]);
        }

        public class _Outer2<T>
        {
            public Dictionary<string, T> items { get; internal set; }
        }

        class _Inner2
        {
            public Dictionary<string, _Inner2> sub_options { get; set; }
        }

        [Fact]
        public void FindRecursiveTypes_DictDict()
        {
            var recurs = Utils.FindRecursiveTypes(typeof(_Outer2<_Inner2>)).ToList();

            Assert.Single(recurs);
            Assert.Equal(typeof(_Inner2), recurs[0]);
        }

        [Fact]
        public void FindReusedTypes_ListList()
        {
            var reused = Utils.FindReusedTypes(typeof(_Outer<_Inner>));
            Assert.Single(reused);
            Assert.Contains(reused, r => r == typeof(_Inner));
        }

        [Fact]
        public void FindReusedTypes_DictDict()
        {
            var reused = Utils.FindReusedTypes(typeof(_Outer2<_Inner2>));
            Assert.Single(reused);
            Assert.Contains(reused, r => r == typeof(_Inner2));
        }

        class _Issue119B
        {
            public _Issue119A A { get; set; }
            public _Issue119B B { get; set; }
        }

        class _Issue119A
        {
            public _Issue119A Recurse { get; set; }
            
            [Jil.JilDirective(Ignore = true)]
            public _Issue119B IgnoredRecurse { get; set; }
        }

        [Fact]
        public void Issue119()
        {
            var typesToPrime = Utils.FindRecursiveTypes(typeof(_Issue119A));
            Assert.Single(typesToPrime);
            Assert.Equal(typeof(_Issue119A), typesToPrime.Single());
        }

        [Fact]
        public void PublicInterfaces()
        {
            // test that'll fail if I break things that tend to go sideways when linking in two different version fo Jil
            //   ie. if A depends on Jil#1 and B, and B depends on Jil#2
            //   we want B not to break if Jil#1 is a higher version than Jil#2; unless Jil#1 is a major version upgrade

            // DeserializationException
            Assert.NotNull(typeof(Jil.DeserializationException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(Exception), typeof(TextReader), typeof(bool) } ));
            Assert.NotNull(typeof(Jil.DeserializationException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(string), typeof(TextReader), typeof(bool) } ));
            Assert.NotNull(typeof(Jil.DeserializationException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(string), typeof(TextReader), typeof(Exception), typeof(bool) } ));
            Assert.NotNull(typeof(Jil.DeserializationException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(string), typeof(Exception), typeof(bool) } ));

            // InfiniteRecursionException
            Assert.NotNull(typeof(Jil.InfiniteRecursionException).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, Type.EmptyTypes));

            // JSON
            Assert.NotNull(typeof(Jil.JSON).GetMethod("SetDefaultOptions", BindingFlags.Public | BindingFlags.Static, new [] { typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("GetDefaultOptions", BindingFlags.Public | BindingFlags.Static, Type.EmptyTypes ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("SerializeDynamic", BindingFlags.Public | BindingFlags.Static, new[] { typeof(object), typeof(TextWriter), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("SerializeDynamic", BindingFlags.Public | BindingFlags.Static, new[] { typeof(object), typeof(Jil.Options) } ));
            Assert.NotNull(
                typeof(Jil.JSON).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .SingleOrDefault(m => m.Name == "Serialize" && m.GetParameters().Length == 3 && m.GetParameters()[0].ParameterType.IsGenericParameter && m.GetParameters()[1].ParameterType == typeof(TextWriter) &&  m.GetParameters()[2].ParameterType == typeof(Jil.Options))
            );
            Assert.NotNull(
                typeof(Jil.JSON).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .SingleOrDefault(m => m.Name == "Serialize" && m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType.IsGenericParameter && m.GetParameters()[1].ParameterType == typeof(Jil.Options))
            );
            Assert.NotNull(typeof(Jil.JSON).GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, new[] { typeof(TextReader), typeof(Type), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string), typeof(Type), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, new[] { typeof(TextReader), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("DeserializeDynamic", BindingFlags.Public | BindingFlags.Static, new[] { typeof(TextReader), typeof(Jil.Options) } ));
            Assert.NotNull(typeof(Jil.JSON).GetMethod("DeserializeDynamic", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string), typeof(Jil.Options) } ));

            // JilDirectiveAttribute
            Assert.NotNull(typeof(Jil.JilDirectiveAttribute).GetConstructor(Type.EmptyTypes));
            Assert.NotNull(typeof(Jil.JilDirectiveAttribute).GetConstructor(new[] { typeof(string) }));
            Assert.NotNull(typeof(Jil.JilDirectiveAttribute).GetConstructor(new [] { typeof(bool) }));
            Assert.NotNull(typeof(Jil.JilDirectiveAttribute).GetConstructor(new[] { typeof(Type) }));

            // Options
            Assert.NotNull(typeof(Jil.Options).GetConstructor(new[] { typeof(bool), typeof(bool), typeof(bool), typeof(Jil.DateTimeFormat), typeof(bool), typeof(Jil.UnspecifiedDateTimeKindBehavior) }));
        }

        [Fact]
        public void UnionConfigs()
        {
            var all = new[] { 
                UnionCharsets.None,
                UnionCharsets.Signed,
                UnionCharsets.Number,
                UnionCharsets.Stringy,
                UnionCharsets.Bool,
                UnionCharsets.Object,
                UnionCharsets.Listy
            };

            for (var a = 0; a < all.Length; a++)
                for (var b = 0; b < all.Length; b++)
                    for (var c = 0; c < all.Length; c++)
                        for (var d = 0; d < all.Length; d++)
                            for (var e = 0; e < all.Length; e++)
                                for (var f = 0; f < all.Length; f++)
                                {
                                    var set = all[a] | all[b] | all[c] | all[d] | all[e] | all[f];

                                    var t1 = UnionConfigLookup.Get(set, false);
                                    var t2 = UnionConfigLookup.Get(set, true);
                                    var i1 = (UnionLookupConfigBase)Activator.CreateInstance(t1);
                                    var i2 = (UnionLookupConfigBase)Activator.CreateInstance(t2);

                                    Assert.Equal(set, i1.Charsets);
                                    Assert.Equal(set, i2.Charsets);

                                    Assert.False(i1.AllowsNull);
                                    Assert.True(i2.AllowsNull);
                                }
            Assert.NotNull(typeof(Jil.Options).GetConstructor(new[] { typeof(bool), typeof(bool), typeof(bool), typeof(Jil.DateTimeFormat), typeof(bool), typeof(Jil.UnspecifiedDateTimeKindBehavior), typeof(Jil.SerializationNameFormat) }));
        }
    }
}

