/*
 * Created by Ranorex
 * User: ic014157
 * Date: 4/7/2016
 * Time: 4:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium;

namespace AppLayer.AppiumService
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Accessor
	{
		
		static IWebDriver driver = null;
		
		public static IWebDriver getDriver(){
			if(driver == null)
				throw new Exception("Webdriver object not set");
			return driver;
		}
		
		public static void setDriver(IWebDriver driver){
			Accessor.driver = driver;
		}
		
		public static bool driverIsForIOS(){
			return Accessor.driver is IOSDriver<IOSElement>;
		}
		
		public static bool driverIsForAndroid(){
			return Accessor.driver is AndroidDriver<AndroidElement>;
		}
			
		
	}
}
