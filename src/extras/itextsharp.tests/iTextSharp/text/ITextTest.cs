using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BaseITextTest = iTextSharp.testutils.ITextTest;

namespace itextsharp.tests.iTextSharp.text
{
    public abstract class ITextTest : BaseITextTest
    {
#if NETCOREAPP
        [OneTimeSetUp]
#else
        [TestFixtureSetUp]
#endif
        public void RunBeforeAnyTests()
        {
            BaseTest.RunBeforeEachTests();
        }
    }
}
