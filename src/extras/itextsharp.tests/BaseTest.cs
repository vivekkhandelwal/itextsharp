using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public abstract class BaseTest
{
#if NETCOREAPP
    [OneTimeSetUp]
#else
    [TestFixtureSetUp]
#endif
    public void RunBeforeAnyTests()
    {
        RunBeforeEachTests();
    }

    public static void RunBeforeEachTests()
    {
        Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory + "/../");
    }
}