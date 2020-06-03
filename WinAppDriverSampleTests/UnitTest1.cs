using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace WinAppDriverSampleTests
{
    [TestClass]
    public class UnitTest1 : TestSession
    {
        [TestMethod]
        public void クリックする()
        {
            Thread.Sleep(3000);

            var panel = _session.FindElementByAccessibilityId("panel1");
            var actions = new Actions(_session);
            actions.Click(panel);       // panel1をクリック
            actions.Perform();      // 実行するために必要

            Thread.Sleep(3000);
        }

        [TestMethod]
        public void ダブルクリックする()
        {
            var panel = _session.FindElementByAccessibilityId("panel1");
            var actions = new Actions(_session);
            actions.DoubleClick(panel);     // panel1をダブルクリック
            actions.Perform();

            Thread.Sleep(3000);
        }

        [TestMethod]
        public void 右クリックする()
        {
            var panel = _session.FindElementByAccessibilityId("panel1");
            var actions = new Actions(_session);
            actions.ContextClick(panel);    // panel1を右クリック
            actions.Perform();

            actions.Click(_session.FindElementByName("Clear"));
            actions.Perform();      // コンテキストメニューのClearをクリック

            Thread.Sleep(3000);
        }

        [TestMethod]
        public void マウスを移動する()
        {
            var panel = _session.FindElementByAccessibilityId("panel2");

            for (int i = 0; i < 10; i++)
            {
                var actions = new Actions(_session);
                actions.MoveToElement(panel, i * 10, i * 10);       // マウスカーソルを移動
                actions.Click();        // クリック
                actions.Perform();

                Thread.Sleep(3000);
            }
        }

        [TestMethod]
        public void キーボードで入力する()
        {
            var textBox = _session.FindElementByAccessibilityId("textBox1");

            var actions = new Actions(_session);
            actions.MoveToElement(textBox);     // textBox1にマウスカーソルを移動
            actions.SendKeys("test");       // キーボードで"test"を入力
            actions.Perform();

            Thread.Sleep(3000);
        }

        [TestMethod]
        public void スクリーンショットをとる()
        {
            var form = _session.FindElementByAccessibilityId("Form1");

            // 起動するまで待つ
            Thread.Sleep(1000);

            var shot = form.GetScreenshot();
            shot.SaveAsFile("form.png", ScreenshotImageFormat.Png);     // スクリーンショットを保存

            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Root");
            var deskSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);

            shot = deskSession.GetScreenshot();
            shot.SaveAsFile("desktop.png", ScreenshotImageFormat.Png);     // デスクトップ全体のスクリーンショットを保存
        }

        [TestMethod]
        public void 起動済みのアプリにアタッチ()
        {
            // テスト実行前にClassInitializeをコメントアウトする

            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Root");
            var deskSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);     // デスクトップセッション

            var window = deskSession.FindElementByName("Form1");
            var windowHandle = window.GetAttribute("NativeWindowHandle");
            windowHandle = (int.Parse(windowHandle)).ToString("x"); // Convert to Hex

            var winOptions = new AppiumOptions();
            winOptions.AddAdditionalCapability("appTopLevelWindow", windowHandle);
            var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), winOptions);      // Form1のセッション

            var panel = session.FindElementByAccessibilityId("panel1");     // panel1
            
            var actions = new Actions(session);
            actions.Click(panel);       // panel1をクリック
            actions.Perform();

            Thread.Sleep(3000);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
