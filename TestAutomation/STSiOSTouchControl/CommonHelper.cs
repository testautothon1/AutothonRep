/*
 * Created by Ranorex
 * User: IC014157
 * Date: 6/9/2017
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using System.Diagnostics;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using AppLayer;
using AppLayer.AppiumService;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Service;
using System.Linq;
using AndroidRepLayer;

namespace STSiOSTouchControl
{
	/// <summary>
	/// Description of CommonHelper.
	/// </summary>
	public class CommonHelper
	{

		public CommonHelper()
		{
		}
		
		private void Init()
		{
			// Your recording specific initialization code goes here.
		}
		
		public void StartApp()
		{
			StartApp(true);
		}
		
		public void StartApp(bool isRateTheApp)
		{
			TestModuleRunner.Run(new iOSSetup(isRateTheApp));
		}
		
		public void CloseApp()
		{
			TearDown.CloseApplication();
		}
		
		public void TapOn(string objectName)
		{
			KeywordImplementation.SwipeVerticle(3,0.80,0.20,By.Name(objectName));
			KeywordImplementation.Tap(objectName);
		}
		
		public void TapOnWithSwipeDown(string objectName)
		{
			KeywordImplementation.SwipeVerticle(3,0.20,0.50,By.Name(objectName));
			KeywordImplementation.Tap(objectName);
		}
		
		public void TapOnSettings()
		{
			Logger.logInfo("Click on Settings icon");
			KeywordImplementation.Click(By.Id("Main LowerBar more"));
		}
		
		public void TapOnProgram()
		{
			Logger.logInfo("Click on Program on Home Screen");
			KeywordImplementation.Click(By.Id("buttonProgramSelection"));
		}

		public void BackAction()
		{
			KeywordImplementation.Click(By.Id("Back"));
		}
		
		public void TapOnIdontKnow()
		{
			KeywordImplementation.Click(By.Id("buttonNotKnown"));
		}
		
		public void ClickRateTheAppNow()
		{
//			TestModuleRunner.Run(new AdbClear());
//			TestModuleRunner.Run(new AndroidSuiteSetUp());
//			CloseApp();
//			bool check=false;
//			for(int i=3;i<=20;i++)
//			{
//				StartApp(false);
//				Logger.logInfo(string.Format("{0} Run of app ",i));
//				if(i==20 && KeywordImplementation.waitForObjectExist(By.Name("Rate the App Now"),TimeSpan.FromSeconds(1)))
//				{
//					Logger.logSuccess(string.Format("Rate the app pop-up found at {0}th iteration",i),true);
//					KeywordImplementation.Click(By.Name("Rate the App Now"));
//					check=true;
//					break;
//				}
//				CloseApp();
//			}
//			if(!check)
//			{
//				Logger.logFailure("Rate the app pop-up not found",true);
//			}
		}
		
//		public void TapOnwithContentDisc(string objectName)
//		{
//			TapOnwithContentDisc(objectName, string.Empty);
//		}
//
//		public void TapOnwithContentDisc(string objectName, string message)
//		{
//			if(message!=String.Empty)
//				Logger.logInfo(message);
//			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'"+objectName+"')]"));
//		}
		
		public void TapOnMuteUnmute()
		{
			KeywordImplementation.Click(By.Id("MuteUnmuteButton"));
			
		}
		
		public void SwitchUsageStatistics(bool on)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("switchUsageStatistics"),TimeSpan.FromSeconds(5));
			if((on.Equals(true) && switchUsageStatistics.GetAttribute("value").Equals("false")) ||  (on.Equals(false) && switchUsageStatistics.GetAttribute("value").Equals("true")))
			{
				switchUsageStatistics.Click();
				Logger.logSuccess("Usage statistics is " + switchUsageStatistics.GetAttribute("value"));
			}
			Logger.logSnapshot();
		}
		
		public void SliderVolumeIncrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutVolumeControl")));
			KeywordImplementation.Click(By.Id("imgViewVolumeUp"));
			KeywordImplementation.Click(By.Id("imgViewVolumeUp"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutVolumeControl")));
			Logger.logInfo("Validating Volume has been Increased");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void SliderVolumeDecrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutVolumeControl")));
			KeywordImplementation.Click(By.Id("imgViewVolumeDown"));
			KeywordImplementation.Click(By.Id("imgViewVolumeDown"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutVolumeControl")));
			Logger.logInfo("Validating Volume has been Decreased");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void ValidateTinnitusVolumeIncrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("sliderVolumeControl")));
			KeywordImplementation.Click(By.Id("btnVolumeUp"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("sliderVolumeControl")));
			Logger.logInfo("Validating Tinnitus Volume has been Increased");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void ValidateTinnitusVolumeDecrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("sliderVolumeControl")));
			KeywordImplementation.Click(By.Id("btnVolumeDown"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("sliderVolumeControl")));
			Logger.logInfo("Validating Tinnitus Volume has been Decreased");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void EditProgram(string programName, string editProgramName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.80,By.Name(programName));
			
			Logger.logInfo(string.Format("Edit the patient with info '{0}'",programName));
			KeywordImplementation.TypeText(By.Name(programName),editProgramName);
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Back);
		}
		
		public void ValidateAppVersion()
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("itemTextViewRight"),true);
			string versionInfo=KeywordImplementation.UIObject(By.Id("itemTextViewRight"),TimeSpan.FromSeconds(2)).Text;
			if((versionInfo.Split('.').Length-1)==3)
				Logger.logSuccess(string.Format("Application version {0} displayed is correct",versionInfo));
			else
				Logger.logFailure(string.Format("Application version {0} displayed is incorrect",versionInfo));
		}
		
		public void ValidateContents(string contents)
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
				
				int count=0;
				while(count!=10 && (!KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 50);
					count++;
				}
				
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1)))
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
		
		public void ValidateContentsContains(string contents,bool isFound)
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
				
				int count=0;
				while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 50);
					count++;
				}
				
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1)))
				{
					check=true;
				}
				
				string message=null;
				if(check==isFound)
				{
					message=isFound?string.Format("Information '{0}' text has found",str):string.Format("Information '{0}' text has not found",str);
					Logger.logSuccess(message);
				}
				else
				{
					message=isFound?string.Format("Information '{0}' text has not found",str):string.Format("Information '{0}' text has found",str);
					Logger.logFailure(message);
				}
			}
			Logger.logSnapshot();
		}

		public void ValidateUsageStatistics(bool isON)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("switchUsageStatistics"),TimeSpan.FromSeconds(1));
			if((isON.Equals(true) && switchUsageStatistics.GetAttribute("value").Equals("true")) ||  (isON.Equals(false) && switchUsageStatistics.GetAttribute("value").Equals("false")))
				Logger.logSuccess("Usage statistics is "+switchUsageStatistics.GetAttribute("value"));
			else
				Logger.logFailure("Usage statistics is "+switchUsageStatistics.GetAttribute("value"));
			
			Logger.logSnapshot();
		}
		
		public void ValidateTextSelected(string objectName)
		{
			if(KeywordImplementation.UIObject(By.Name(objectName)).GetAttribute("selected").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess(string.Format("'{0}' is selected",objectName));
			else
				Logger.logFailure(string.Format("'{0}'is not selected",objectName));
		}
		
		public void ValidateMuteUnmute(bool muteUnmute)
		{
			string message=null;
			var buttonMuteUnmute=KeywordImplementation.UIObject(By.Id("MuteUnmuteButton"),TimeSpan.FromSeconds(1));
			if(buttonMuteUnmute.GetAttribute("label").Equals(muteUnmute.ToString(),StringComparison.CurrentCultureIgnoreCase))
			{
				message=muteUnmute?"HI is mute":"HI is unmute";
				Logger.logSuccess(message);
			}
			else
			{
				message=muteUnmute?"HI is unmute":"HI is mute";
				Logger.logFailure(message);
			}
			//	Logger.logFailure("HI is "+muteUnmute);
			
//			if(KeywordImplementation.UIObject(By.Id("MuteUnmuteButton")).GetAttribute("label").Equals(muteUnmute,StringComparison.CurrentCultureIgnoreCase))
//				Logger.logSuccess("HI is "+muteUnmute);
//			else
//				Logger.logFailure("HI is "+muteUnmute);
		}
		
		public void ValidateTinnitusMixedMode()
		{
			int check=0;
			if(KeywordImplementation.waitForObjectExist(By.Id("sliderVolumeControl"),TimeSpan.FromSeconds(1)))
			{
				check++;
				Logger.logSuccess("Slider for volume command is present");
			}
			else
				Logger.logFailure("Slider for volume command is not present");
			
			if(KeywordImplementation.waitForObjectExist(By.Id("sliderTherapyControl"),TimeSpan.FromSeconds(1)))
			{
				check++;
				Logger.logSuccess("Therapy signal is present");
			}
			else
				Logger.logFailure("Therapy signal is not present");
			
			if(check==2)
				Logger.logSuccess("The Tinnitus program is in mixed mode");
			else
				Logger.logFailure("The Tinnitus program is not in mixed mode");
		}
		
		public void ValidateSliderSteps()
		{
			string []sliderSteps = null;
			for(int i=0;i<16;i++)
				KeywordImplementation.Click(By.Id("imgViewVolumeUp"));

			sliderSteps=KeywordImplementation.UIObject(By.Id("seekBarVolume")).GetAttribute("name").Split('_');
			if((sliderSteps[1].Equals(sliderSteps[2])) && (sliderSteps[2].Equals("15")))
				Logger.logInfo("Volume slider has 16 steps",true);
			else
				Logger.logFailure("Volume slider does not have 16 steps",false);
		}
		
		public void ValidateRateOurApp(string AppRate,bool isPopup,int runCount)
		{
			//bool check=false;
			for(int i=1;i<=runCount;i++)
			{
				
				StartApp(false);
				Logger.logInfo(string.Format("{0} Run of App",i));
				if(isPopup.Equals(true))
				{
					if(i==runCount && KeywordImplementation.waitForObjectExist(By.Name("Rate our app"),TimeSpan.FromSeconds(2)))
					{
						Logger.logSuccess(string.Format("Rate the app pop-up found at {0}th iteration",i),true);
						ValidateContents("Not now;No rating;Rate the App Now");
						KeywordImplementation.Click(By.Name(AppRate));
					//	check=true;
						break;
					}
					else if(i==runCount && !KeywordImplementation.waitForObjectExist(By.Name("Rate our app"),TimeSpan.FromSeconds(2)))
					{
						Logger.logFailure("Rate the app - automatic pop-up not found");
						break;
					}
				}
				else if(isPopup.Equals(false))
				{
					if(KeywordImplementation.waitForObjectExist(By.Name("Rate our app"),TimeSpan.FromSeconds(2)))
					{
						Logger.logFailure(string.Format("Rate the app - automatic pop-up found at {0} itteration",i));
						break;
					}
					else if(i==runCount)
						Logger.logSuccess("Rate the app - automatic pop-up not found");
				}
				CloseApp();
			}
			
		}
		
		public void PermissionPopUpHandle()
		{
			if(KeywordImplementation.waitForObjectExist(By.Name("ALLOW"),TimeSpan.FromSeconds(3)))
				KeywordImplementation.Click(By.Name("ALLOW"));
			else if(KeywordImplementation.waitForObjectExist(By.Name("Allow"),TimeSpan.FromSeconds(3)))
				KeywordImplementation.Click(By.Name("Allow"));
		}
		
		public void ValidateControlsOnMFHomePage()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("imgViewSelectedProgram"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Brand image exists");
			else
				Logger.logFailure("Brand image do not exists");
			if(KeywordImplementation.waitForObjectExist(By.Id("seekBarVolume"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Volume slider exists");
			else
				Logger.logFailure("Volume slider do not exists");
			if(KeywordImplementation.waitForObjectExist(By.Id("btnVolumeMute"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Mute button exists");
			else
				Logger.logFailure("Mute button do not exists");
			if(KeywordImplementation.waitForObjectExist(By.Id("imgMoreMenu"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Settings Button exists");
			else
				Logger.logFailure("Settings Button do not exists");
		}
		
		public void ValidateContentsWithContentDesc(string objectName,bool isFound)
		{
			int swipeCnt=0;
			bool check=false;
			string[] validationObjectArray = objectName.Split(';');
			foreach(string obj in  validationObjectArray)
			{
				while(swipeCnt!=3)
				{
					if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@content-desc,'"+obj+"')]"),TimeSpan.FromSeconds(1)))
					{
						check=true;
						break;
					}
					if(!check)
					{
						KeywordImplementation.SwipeVerticle(swipeCnt,0.50,0.20,By.XPath("//*[contains(@content-desc,'"+obj+"')]"));
						swipeCnt++;
					}
					else
						break;
				}
				string message=null;
				if(check==isFound)
				{
					message=isFound?string.Format("'{0}' is found",obj):string.Format("'{0}' is not found",obj);
					Logger.logSuccess(message);
				}
				else
				{
					message=isFound?string.Format("'{0}' is not found",obj):string.Format("'{0}' is found",obj);
					Logger.logFailure(message);
				}
			}
			Logger.logSnapshot();
			
		}
		
	}
}

