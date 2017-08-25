/*
 * Created by Ranorex
 * User: Z003RWZS
 * Date: 4/17/2017
 * Time: 10:40 AM
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

namespace STSAndTouchControl
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
			TestModuleRunner.Run(new AndroidSetUp(isRateTheApp));
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
			KeywordImplementation.Click(By.Id("imgMoreMenu"));
		}
		
		public void TapOnProgram()
		{
			Logger.logInfo("Click on Program on Home Screen");
			KeywordImplementation.Click(By.Id("layoutProgramSelect"));
		}

		public void BackAction()
		{
			KeywordImplementation.PressBack();
		}
		
		public void TapOnIdontKnow()
		{
			KeywordImplementation.Click(By.Id("buttonNotKnown"));
		}
		
		public void ClickRateTheAppNow()
		{
			TestModuleRunner.Run(new AdbClear());
			TestModuleRunner.Run(new AndroidSuiteSetUp());
			CloseApp();
			bool check=false;
			for(int i=3;i<=20;i++)
			{
				StartApp(false);
				Logger.logInfo(string.Format("{0} Run of app ",i));
				if(i==20 && KeywordImplementation.waitForObjectExist(By.Name("Rate the App Now"),TimeSpan.FromSeconds(1)))
				{
					Logger.logSuccess(string.Format("Rate the app pop-up found at {0}th iteration",i),true);
					KeywordImplementation.Click(By.Name("Rate the App Now"));
					check=true;
					break;
				}
				CloseApp();
			}
			if(!check)
			{
				Logger.logFailure("Rate the app pop-up not found",true);
			}
		}
		
		public void TapOnwithContentDisc(string objectName)
		{
			TapOnwithContentDisc(objectName, string.Empty);
		}
		
		public void TapOnwithContentDisc(string objectName, string message)
		{
			if(message!=String.Empty)
				Logger.logInfo(message);
			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'"+objectName+"')]"));
		}
		
		public void SwitchUsageStatistics(bool on)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("toggleButton"),TimeSpan.FromSeconds(5));
			if((on.Equals(true) && switchUsageStatistics.Text.Equals("Off")) ||  (on.Equals(false) && switchUsageStatistics.Text.Equals("On")))
			{
				switchUsageStatistics.Click();
				Logger.logSuccess("Usage statistics is " + switchUsageStatistics.Text);
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
		
		public void TinnitusVolumeIncrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutTinnitus")));
			KeywordImplementation.Click(By.Id("imgViewTinnitusUp"));
			KeywordImplementation.Click(By.Id("imgViewTinnitusUp"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutTinnitus")));
			Logger.logInfo("Validating Tinnitus Volume has been Increased");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void TinnitusVolumeDecrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutTinnitus")));
			KeywordImplementation.Click(By.Id("imgViewTinnitusDown"));
			KeywordImplementation.Click(By.Id("imgViewTinnitusDown"));
			KeywordImplementation.Click(By.Id("imgViewTinnitusDown"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("layoutTinnitus")));
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
			KeywordImplementation.ValidateContents(contents);
		}

		public void ValidateContentsContains(string contents,bool isFound )
		{
//			Logger.logInfo(string.Format("Tap on '{0}'",contents));
			KeywordImplementation.ValidateContentsContains(contents,isFound);
		}

		public void ValidateUsageStatistics(bool isON)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("toggleButton"),TimeSpan.FromSeconds(1));
			if((isON.Equals(true) && switchUsageStatistics.Text.Equals("On")) ||  (isON.Equals(false) && switchUsageStatistics.Text.Equals("Off")))
				Logger.logSuccess("Usage statistics is "+switchUsageStatistics.Text);
			else
				Logger.logFailure("Usage statistics is "+switchUsageStatistics.Text);
			
			Logger.logSnapshot();
		}
		
		public void ValidateTextSelected(string objectName)
		{
			if(KeywordImplementation.UIObject(By.Name(objectName)).GetAttribute("selected").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess(string.Format("'{0}' is selected",objectName));
			else
				Logger.logFailure(string.Format("'{0}'is not selected",objectName));
		}
		
		public void ValidateMuteUnmute(bool on)
		{
			string message=null;
			if(KeywordImplementation.UIObject(By.Id("btnVolumeMute")).GetAttribute("selected").Equals(Convert.ToString(on),StringComparison.CurrentCultureIgnoreCase))
			{
				message=on?"HI is Mute ":"HI is Unmute";
				Logger.logSuccess(message);
			}
			else
			{
				message=on?"HI is Unmute":"HI is Mute ";
				Logger.logFailure(message);
			}
		}
		
		public void ValidateTinnitusMixedMode()
		{
			int mixedModecont = 0;
			if(KeywordImplementation.waitForObjectExist(By.Id("seekBarVolume"),TimeSpan.FromSeconds(1)))
			{
				Logger.logInfo("Slider for volume command is present");
				mixedModecont++;
			}
			else
				Logger.logFailure("The Volume slider is not present");

			if(KeywordImplementation.waitForObjectExist(By.Id("seekBarTinnitusMasker"),TimeSpan.FromSeconds(1)))
			{
				Logger.logInfo("Therapy signal is present");
				mixedModecont++;
			}
			else
				Logger.logFailure("Therapy signal is not present");
			
			if(mixedModecont == 2)
				Logger.logSuccess("Tinnitus mixed mode present");
			else
				Logger.logFailure("Tinnitus mixed mode not present");

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
			for(int i=1;i<=runCount;i++)
			{
				bool check = false;
				StartApp(false);
				Logger.logInfo(string.Format("{0} Run of App",i));
				if(isPopup.Equals(true))
				{
					if(i==runCount && KeywordImplementation.waitForObjectExist(By.Name("Rate our app"),TimeSpan.FromSeconds(2)))
					{
						Logger.logSuccess(string.Format("Rate the app pop-up found at {0}th iteration",i),true);
						ValidateContents("Not now;No rating;Rate the App Now");
						KeywordImplementation.Click(By.Name(AppRate));
						check=true;
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
		
		public void ValidateControlsOnHomescreen()
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
		
		public void ClickOnAlertMessage(string option)
		{
			KeywordImplementation.ClickOnAlertMessage(option);
		}
	}
}
