using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDriverSampleTests
{
    public class TestSession
    {
        protected static WindowsDriver<WindowsElement> _session;

        public static void Setup(TestContext context)
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Ysk\WinAppDriverSample\WinAppDriverSample\bin\Debug\WinAppDriverSample.exe");
            _session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        }

        public static void TearDown()
        {
            if (_session != null)
            {
                _session.Quit();
                _session = null;
            }
        }
    }
}
