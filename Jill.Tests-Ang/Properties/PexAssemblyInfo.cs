using System.Globalization;
using System;
// <copyright file="PexAssemblyInfo.cs">Copyright ©  2020</copyright>
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]
//[assembly: PexAssemblySettings(TestFramework = "XUnit")]

// Microsoft.Pex.Framework.Instrumentation
//[assembly: PexAssemblyUnderTest("ClassLibrary1")]
//[assembly: PexInstrumentAssembly(typeof(JSON))]
//[assembly: PexInstrumentAssembly(typeof(SerializeTests))]
[assembly: PexAssemblyUnderTest("Jil")]
//[assembly: PexAssemblyUnderTest("JilTests")]
//[assembly: PexInstrumentAssembly("System.Core")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
//[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]
[assembly: PexInstrumentAssembly("JilTests")]
[assembly: PexInstrumentAssembly("Jil")]
[assembly: PexInstrumentType(typeof(AppDomain))]
[assembly: PexInstrumentType(typeof(Math))]
[assembly: PexInstrumentType("mscorlib", "System.Signature")]
[assembly: PexInstrumentType("mscorlib", "System.DateTimeFormat")]
[assembly: PexInstrumentType(typeof(DateTimeFormatInfo))]
[assembly: PexInstrumentType(typeof(GregorianCalendar))]
[assembly: PexInstrumentType("mscorlib", "System.Text.StringBuilderCache")]

