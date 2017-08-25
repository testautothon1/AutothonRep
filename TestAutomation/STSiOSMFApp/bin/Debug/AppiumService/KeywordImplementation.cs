/*
 * Created by Ranorex
 * User: ic014157
 * Date: 4/7/2016
 * Time: 3:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Remoting;
using Ranorex.Core.Testing;

namespace AppLayer.AppiumService
{
	/// <summary>
	/// Description of KeywordImplementation.
	/// </summary>
	public class KeywordImplementation
	{
		static AppiumLocalService service = null;
		public static void ServiceStart()
		{
			service = OpenQA.Selenium.Appium.Service.AppiumLocalService.BuildDefaultService();
			service.Start();
		}
		
		public static void LaunchAndroidApp(String udid, String appPackage, String appActivity)
		{
			ServiceStart();
			AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement> driver = null;
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("device", "Android");
			capabilities.SetCapability(CapabilityType.Platform, "Windows");
			capabilities.SetCapability("deviceName", "ABCD");
			capabilities.SetCapability("udid",udid);
			capabilities.SetCapability("platformName", "Android");
			
			capabilities.SetCapability("appPackage", appPackage);
			capabilities.SetCapability("appActivity", appActivity);

			capabilities.SetCapability("noReset", true);
			try
			{
				driver = new AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>(service.ServiceUrl, capabilities, TimeSpan.FromSeconds(180));
				Accessor.setDriver(driver);
				Ranorex.Report.Info("Launch Android App", "Connected to Appium Server at " + service.ServiceUrl.ToString());
				
			}
			catch(Exception ex){
				Ranorex.Report.Log(ReportLevel.Failure, ex.Message);
			}
			
		}
		
		public static void LaunchiOSApp(Uri appiumServer, String udid, String bundleId,string appPath)
		{
			IOSDriver<OpenQA.Selenium.Appium.iOS.IOSElement> driver = null;
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("device", "iOS");
			
			capabilities.SetCapability(CapabilityType.Platform, "Mac");
			capabilities.SetCapability("deviceName", "iPhone");
			capabilities.SetCapability("udid", udid);
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("app", appPath);
			capabilities.SetCapability("bundleId", bundleId);
			if(TestSuite.Current.Parameters["Version"].Equals("10",StringComparison.CurrentCultureIgnoreCase))
			{
				capabilities.SetCapability("automationName", "XCUITest");
				capabilities.SetCapability("xcodeConfigFile", "/Users/audiology/xcodeBuild.xcconfig");
			}
			else
				capabilities.SetCapability("automationName", "appium");
			
			capabilities.SetCapability("showIOSLog", "false");
			capabilities.SetCapability("noReset", true);
			try
			{
				driver = new IOSDriver<OpenQA.Selenium.Appium.iOS.IOSElement>(appiumServer, capabilities, TimeSpan.FromSeconds(180));
				Accessor.setDriver(driver);
				Ranorex.Report.Info("Launch iOS App", "Connected to Appium Server at " + appiumServer.ToString());
				
				if(TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
					if((!TestSuite.Current.Parameters["Version"].Equals("10",StringComparison.CurrentCultureIgnoreCase)))
						((IOSDriver<IOSElement>)Accessor.getDriver()).Orientation=ScreenOrientation.Landscape;
			}catch(Exception ex){
				Ranorex.Report.Log(ReportLevel.Failure, ex.Message);
			}
			
		}
		
		public static void LaunchAndroidWebView(String udid, String webURL)
		{
			ServiceStart();
			AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement> driver = null;
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("device", "Android");
			capabilities.SetCapability("deviceName", "ABCD");
			capabilities.SetCapability("udid",udid);
			capabilities.SetCapability("browserName", "Chrome");
			capabilities.SetCapability("platformName", "Android");
			try
			{
				driver = new AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>(service.ServiceUrl, capabilities, TimeSpan.FromSeconds(180));
				driver.Navigate().GoToUrl(webURL);
				Accessor.setDriver(driver);
				KeywordImplementation.TypeText(By.Name("q"),"executeautomation");
				//Find the Search text box UI Element
//				IWebElement element = driver.FindElement(By.Name("q"));
//
//				//Perform Ops
//				element.SendKeys("executeautomation");
				Ranorex.Report.Info("Launch Android web browser" + service.ServiceUrl.ToString());
			}
			catch(Exception ex){
				Ranorex.Report.Log(ReportLevel.Failure, ex.Message);
			}
			
		}
		
		public static void LaunchWeb(String browser, String webURL)
		{
			try
			{
				//System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Framework\TestAutomation\External\Drivers\chromedriver.exe");

				IWebDriver driver= null;
				string p=Path.GetFullPath(@"..\..\..\External\Drivers\");
				if(browser.Equals("chrome",StringComparison.CurrentCultureIgnoreCase))
					driver= new ChromeDriver(Path.GetFullPath(@"..\..\..\External\Drivers\"));
				Accessor.setDriver(driver);
				driver.Navigate().GoToUrl(webURL);
				//Find the Search text box UI Element
				//IWebElement element = driver.FindElement(By.Name("q"));
				
				//Perform Ops
				//element.SendKeys("executeautomation");
				//KeywordImplementation.TypeText(By.Name("q"),"executeautomation");
				Logger.logInfo("Launch web browser" + service.ServiceUrl.ToString());
			}
			catch(Exception ex){
				Ranorex.Report.Log(ReportLevel.Info, ex.Message);
			}
			
		}
		
		
		
		public static void RemoveApp()
		{
			string applicationName=TestSuite.Current.Parameters["AppName"];
			try
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).RemoveApp(applicationName);
			}
			catch(Exception e)
			{
				Logger.logInfo("App has removed wih message: "+e.Message);
			}
		}
		
		public static void clickVolumeUp()
		{
			if(Accessor.driverIsForIOS()){
				((IOSDriver<OpenQA.Selenium.Appium.iOS.IOSElement>)Accessor.getDriver()).ExecuteScript("UIATarget.localTarget().clickVolumeUp()");
			}
			else
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Keycode_VOLUME_UP);
			}
		}
		
		public static void waitForObject(By by, TimeSpan timeSpan)
		{
			by=translateBy(by);
			WebDriverWait wait = new  WebDriverWait(Accessor.getDriver(), timeSpan);
			
			wait.Until(drv => drv.FindElement(by));
		}
		
		public static bool waitForObjectExist(By by, TimeSpan timeSpan)
		{
			by=translateBy(by);
			try
			{
				WebDriverWait wait = new  WebDriverWait(Accessor.getDriver(), timeSpan);
				var webElement=wait.Until(drv => drv.FindElement(by));
				return webElement.Displayed;
			}
			catch(Exception e)
			{
				Report.Warn(e.Message);
				return false;
			}
			
		}
		
		public static IWebElement UIObject(By by)
		{
			return(UIObject(by, TimeSpan.FromSeconds(2)));
		}
		
		public static IWebElement UIObject(By by, TimeSpan timeSpan)
		{
			by=translateBy(by);
			WebDriverWait wait = new  WebDriverWait(Accessor.getDriver(), timeSpan);
			
			var webElement=wait.Until(drv => drv.FindElement(by));
			return webElement;
		}
		
		public static void Click(By by){
			Click(by, String.Empty);
		}
		
		public static void Click(By by, string messageForReport){
			Click(by, messageForReport, false);
		}
		
		public static void Click(By by, string messageForReport, Boolean takeScreenshot)
		{
			by=translateBy(by);
			
			try{
				waitForObject(by,TimeSpan.FromSeconds(10));
				
				IWebElement element = Accessor.getDriver().FindElement(by);
				
				if(Accessor.driverIsForIOS()){
					((IOSDriver<IOSElement>) Accessor.getDriver()).Tap(1, element, 300);
				}else{
					element.Click();
				}
				
				if(messageForReport != String.Empty)
					Ranorex.Report.Info("Click", messageForReport);
				
				if(takeScreenshot)
					Logger.logSnapshot();//getSnapshot(Accessor.getDriver());
				
			}catch(Exception ex){
				string a = ex.Message;
				if(ex.Message.Contains("point is not within the bounds of the screen"))
				{
					IWebElement element = Accessor.getDriver().FindElement(by);
					var size=Accessor.getDriver().Manage().Window.Size;
					
					int starty = (int) (size.Height * 0.50);
					int endy = (int) (size.Height*0.20);
					int startx = size.Width / 2;
					
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					((IOSDriver<IOSElement>) Accessor.getDriver()).Tap(1, element, 300);
				}
				else
				{
					Ranorex.Report.Log(ReportLevel.Failure, ex.Message);
					Logger.logSnapshot();//getSnapshot(Accessor.getDriver());
				}
			}
			
		}
		
		public static void ClearText(IWebElement element)
		{
			int stringLength = element.Text.Length;
			element.Click();
			for (int i = 0; i <= stringLength; i++)
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Keycode_DPAD_RIGHT);
			}
			element.Click();
			for (int i = 0; i <= stringLength; i++)
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Keycode_DEL);
			}
		}
		
		public static void TypeText(By by, String text2Type)
		{
			TypeText(by, text2Type, String.Empty);
		}
		
		public static void TypeText(By by, String text2Type, String messageForReport)
		{
			if(Accessor.driverIsForIOS() || Accessor.driverIsForAndroid())
				by=translateBy(by);
			try{
				IWebElement element = Accessor.getDriver().FindElement(by);
				
				if(Accessor.driverIsForAndroid())
				{
					int length=element.Text.Length;
					while(element.Text.Length!=0 && element.Text!="Search")
					{
						ClearText(element);
						element = Accessor.getDriver().FindElement(by);
						if(length==element.Text.Length)
							break;
						length=element.Text.Length;
					}
				}
				else
				{
					element.Clear();
				}
				
				if(!Accessor.driverIsForIOS())
				{
					element.SendKeys(text2Type);
				}
				else
				{
					element.Click();
					((IOSDriver<IOSElement>)Accessor.getDriver()).Keyboard.SendKeys(text2Type);
				}
				
				if(messageForReport != String.Empty)
					Ranorex.Report.Info("TypeText", messageForReport);
				
			}
			catch(Exception ex){
				Ranorex.Report.Log(ReportLevel.Failure, ex.ToString());
				Logger.logSnapshot();
			}
			
		}
		
		private static By translateBy(By by){
			String byString = by.ToString();
			if( (Accessor.driverIsForAndroid()) && byString.StartsWith("By.Name: "))
			{
				String name = byString.Substring(9);
				String translatedXPath = "//*[@text='"+ name +"']";
				return By.XPath(translatedXPath);
			}
			if( (Accessor.driverIsForIOS()) && byString.StartsWith("By.XPath: ") && TestSuite.Current.Parameters["Version"].Equals("10",StringComparison.CurrentCultureIgnoreCase))
			{
				String name = byString.Substring(10);
				String translatedXPath = name.Replace("UIATableView","XCUIElementTypeTable").Replace("UIA","XCUIElementType");
				return By.XPath(translatedXPath);
			}
			else
			{
				return by;
			}
		}

		public static void ValidateContents(string contents)
		{
			string[] contentsArray= contents.Split(';');
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				SwipeVerticle(3,0.70,0.20,By.Name(str));
				if(waitForObjectExist(By.Name(str),TimeSpan.FromSeconds(1)))
				{
					check=true;
					Logger.logSuccess(string.Format("Information '{0}' text has found",str));
				}
				
				if(!check)
				{
					Logger.logFailure(string.Format("Information '{0}' text has not found",str));
				}
			}
			Logger.logSnapshot();
			
		}
		
		public static void ValidateContentsContains(string contents,bool isFound )
		{
			
			string[] contentsArray= contents.Split(';');
			
			var size=Accessor.getDriver().Manage().Window.Size;
			
			/*****************************/
			//Swipe from Bottom to Top
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				var textViewArray=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementsByXPath("//android.widget.TextView");
				int swipeCount=0;
				while(swipeCount!=3)
				{
					foreach(var textView in textViewArray)
					{
						if(textView.Text.Contains(str))
						{
							check=true;
							break;
						}
					}
					if(!check)
						((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
					else
						break;
					textViewArray=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementsByXPath("//android.widget.TextView");
					swipeCount++;
				}
				
				string message=null;
				if(check==isFound)
				{
					message=isFound?string.Format("Information '{0}' text is found",str):string.Format("Information '{0}' text is not found",str);
					Logger.logSuccess(message);
				}
				else
				{
					message=isFound?string.Format("Information '{0}' text is not found",str):string.Format("Information '{0}' text is found",str);
					Logger.logFailure(message);
				}
				
			}
			Logger.logSnapshot();
		}
		
		public static void PressBack()
		{
			Logger.logInfo("Press the back key");
			try
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Back);
			}
			catch(Exception e)
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Back);
			}
			Delay.Seconds(2);
		}
		
		public static void Tap(string objectName)
		{
			Logger.logInfo(string.Format("Tap on '{0}'",objectName));
			KeywordImplementation.Click(By.Name(objectName));
		}
		
		public static void SwipeVerticle(int count,double startY, double endY,By by)
		{
			SwipeVerticle(count,startY,endY,by,false);
		}
		
		public static void SwipeVerticle(int count,double startY, double endY,By by, Boolean takeScreenshot)
		{
			int cnt=0;
			var size=Accessor.getDriver().Manage().Window.Size;
			int starty = (int) (size.Height *startY);
			int endy = (int) (size.Height*endY);
			int startx = size.Width / 2;
			while(cnt!=count && (!KeywordImplementation.waitForObjectExist(by,TimeSpan.FromSeconds(1))))
			{
				if(Accessor.driverIsForIOS())
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
				else if(Accessor.driverIsForAndroid())
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
				cnt++;
			}
			
			if(takeScreenshot)
				Logger.logSnapshot();
		}
		
		public static void ClickOnAlertMessage(string option)
		{
			if(waitForObjectExist(By.Name(option),TimeSpan.FromSeconds(2)))
			{
				Click(By.Name(option));
			}
		}
		

	}
}
