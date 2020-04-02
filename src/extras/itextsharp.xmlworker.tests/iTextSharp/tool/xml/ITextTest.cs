using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BaseITextTest = iTextSharp.testutils.ITextTest;

namespace itextsharp.xmlworker.tests.iTextSharp.tool.xml
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
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory + "/../");
        }
    }
}
