/*
 * Created by Ranorex
 * User: manishk
 * Date: 6/27/2016
 * Time: 1:29 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

//extern alias WebDriver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.Diagnostics;
using Castle.Components.DictionaryAdapter.Xml;
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

namespace STSAndBasic
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
		
		public static void BrandBasedHINames()
		{
			if(TestSuite.Current.Parameters["AppName"].Equals("com.signia.mobilefitting",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Run P";
				TestSuite.Current.Parameters["H2"] = "Run SP";
				TestSuite.Current.Parameters["H3"] = "Run Click ITC";
				TestSuite.Current.Parameters["H4"] = "Run Click CIC";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.connexx.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Targa P 5A";
				TestSuite.Current.Parameters["H2"] = "Targa HP 5A";
				TestSuite.Current.Parameters["H3"] = "inoX ITC 5A";
				TestSuite.Current.Parameters["H4"] = "inoX CIC 5A";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.aandm.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "XTM P A4";
				TestSuite.Current.Parameters["H2"] = "XTM XP A4";
				TestSuite.Current.Parameters["H3"] = "XTM IF ITC A4";
				TestSuite.Current.Parameters["H4"] = "XTM IF CIC A4";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.audioservice.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Volta P M";
				TestSuite.Current.Parameters["H2"] = "Volta HP M";
				TestSuite.Current.Parameters["H3"] = "Volta quiX P M";
				TestSuite.Current.Parameters["H4"] = "Volta quiX M";
			}
			else
			{
				Logger.logFailure("AppName not found");
			}
		}
		
		public string HISelection(string HI)
		{
			if(HI.Equals("HA1"))
			{
				HI = TestSuite.Current.Parameters["H1"];
			}
			else if(HI.Equals("HA2"))
			{
				HI = TestSuite.Current.Parameters["H2"];
			}
			else if(HI.Equals("HA3"))
			{
				HI = TestSuite.Current.Parameters["H3"];
			}
			else if(HI.Equals("HA4"))
			{
				HI = TestSuite.Current.Parameters["H4"];
			}
			return HI;
		}
		
//		public void SwitchToChooseHI()
//		{
		////			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'Run Click CIC')]"));
		////			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'Run Click ITC')]"));
		////			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'Run P')]"));
		////			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'Run SP')]"));
//			Logger.logSnapshot();
//			KeywordImplementation.Tap("Continue");
//
//			if(KeywordImplementation.waitForObjectExist(By.Id("Txt_CommonAlert_Title"),TimeSpan.FromSeconds(3)))
//			{
//				Logger.logInfo("Security alert has popped up",true);
//				KeywordImplementation.Click(By.Name("OK"));
//			}
//		}
		
		public void SwitchSettingUseMold(bool YesNo)
		{
			var switchUseMold=KeywordImplementation.UIObject(By.Id("Switch_Settings_UseMolds"),TimeSpan.FromSeconds(2));
			if((YesNo.Equals(true) && switchUseMold.Text.Equals("No")) ||  (YesNo.Equals(false) && switchUseMold.Text.Equals("Yes")))
			{
				switchUseMold.Click();
				Logger.logInfo(string.Format("Switch custom ear mold is '{0}' ", switchUseMold.Text));
			}
			Logger.logSnapshot();
		}
		
		public void ClickInfoButton()
		{
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Btn_ActionBar_Left"));
			else
				KeywordImplementation.Click(By.Id("Tab_Cutomer_Info"));
			Logger.logInfo("Clicked on Info",true);
		}

		public void ValidateInfoPage()
		{
			if(KeywordImplementation.waitForObjectExist(By.Name("Info"),TimeSpan.FromSeconds(2)))
			{
				//Logger.logSuccess("Info page has opened");
				Logger.logSuccess("Info page has opened");
			}
			else
			{
				//	Logger.validateFailure("Info page has not opened");
				Logger.validateFailure("Info page has not opened");
			}
		}
		
		public void SelectPage(string pageName)
		{
			KeywordImplementation.Click(By.Name(pageName));
			Logger.logInfo(string.Format("Selected page '{0}'",pageName),true);
		}
		
		public void TapOn(string objectName)
		{
			KeywordImplementation.Tap(objectName);
		}

		public void TapOnWithSwipe(string objectName)
		{
			string[] objectaArr = objectName.Split('@');
			if(objectaArr.Count()>1)
			{
				if(TestSuite.Current.Parameters["Brand"].Equals("Signia",StringComparison.CurrentCultureIgnoreCase))
				{
					KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name(objectaArr[0]));
					KeywordImplementation.Tap(objectaArr[0]);
				}
				if(TestSuite.Current.Parameters["Brand"].Equals("Connex",StringComparison.CurrentCultureIgnoreCase))
				{
					KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name(objectaArr[1]));
					KeywordImplementation.Tap(objectaArr[1]);
				}
			}
			else
				KeywordImplementation.Tap(objectName);
		}

		public void SelectPageFromInfo(string pageName)
		{
			Logger.logInfo(string.Format("Select page '{0}' from info",pageName));
			if(pageName.Equals("Supported Hearing Aids",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Info_Msg1"));
			else if(pageName.Equals("Medical Info",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Info_Msg2"));
			else if(pageName.Equals("App Info",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Info_Msg"));
			else if(pageName.Equals("FAQs",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Info_Msg4"));
			else if(pageName.Equals("Legal Notice",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Info_Msg7"));
			else if(pageName.Equals("Privacy Policy",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("Txt_Info_Msg9"));
				KeywordImplementation.Click(By.Id("Txt_Info_Msg8"));
			}
			else if(pageName.Equals("Terms & Conditions",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("Txt_Info_Msg9"));
				KeywordImplementation.Click(By.Id("Txt_Info_Msg9"));
			}
			
			Logger.logSnapshot();
		}
		
		public void ValidateInfoPage(string pageName)
		{
			if(KeywordImplementation.waitForObjectExist(By.Name(pageName),TimeSpan.FromSeconds(2)))
				Logger.logSuccess(string.Format("'{0}' page has opened",pageName));
			else
				Logger.validateFailure(string.Format("'{0}' page has not opened",pageName));
			Logger.logSnapshot();
		}
		
		public void ValidateContents(string contents)
		{
			KeywordImplementation.ValidateContents(contents);
		}
		
		public void LogSnapshot()
		{
			Logger.logSnapshot();
		}

		public void ValidateContentsContains(string contents,bool isFound )
		{
			KeywordImplementation.ValidateContentsContains(contents,isFound);
		}
		
		public void ValidateCustomerContinueEnabled(bool enabled)
		{

			KeywordImplementation.SwipeVerticle(3,0.50,0.30,By.Id("Btn_Cust_Continue"));
			string message=null;
			if(KeywordImplementation.UIObject(By.Id("Btn_Cust_Continue")).Enabled==enabled)
			{
				message=enabled?"Continue button is enabled":"Continue button is disabled";
				Logger.logSuccess(message);
			}
			else
			{
				message=enabled?"Continue button is disabled":"Continue button is enabled";
				Logger.logFailure(message);
			}
			Logger.logSnapshot();
		}
		
		public void BackAction()
		{
			//Delay.Seconds(2);
			KeywordImplementation.PressBack();
		}

		public void CreatePatient(string lastName, string firstName)
		{
			CreatePatient(lastName,firstName,string.Empty);
		}
		
		public void CreatePatient(string lastName, string firstName, string Mmm_dd_yyyy)
		{
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("RdBtn_Cust_New"));
			else
			{
				KeywordImplementation.Click(By.Id("Tab_Cutomer_ClientList"));
				Delay.Seconds(2);
			}
			
			if(KeywordImplementation.waitForObjectExist(By.Id("EdTxt_Cust_Comment"),TimeSpan.FromSeconds(2)))
			{
				KeywordImplementation.Click(By.Id("Txt_Cust_Comment"));
				Delay.Seconds(2);
			}
			
			KeywordImplementation.SwipeVerticle(3,0.50,0.80,By.Id("EdTxt_Cust_LastName"));
			
			if(lastName!=String.Empty)
			{
				KeywordImplementation.TypeText(By.Id("EdTxt_Cust_LastName"),lastName);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
			}
			Delay.Seconds(2);
			if(firstName!=String.Empty)
			{
				KeywordImplementation.TypeText(By.Id("EdTxt_Cust_FirstName"),firstName);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
			}
			
			if(Mmm_dd_yyyy != String.Empty)
			{
				SwipeForDOB(Mmm_dd_yyyy.Split('_')[0],Mmm_dd_yyyy.Split('_')[1],Mmm_dd_yyyy.Split('_')[2]);
			}
			
			
			
//			/*****************************/
//			//Swipe from Bottom to Top and Top to bottom
//			//Find swipe start and end point from screen's width and height.
//			int starty1 = (int) (size.Height * 0.50);
//			int endy1 = (int) (size.Height*0.20);
//			int startx1 = size.Width / 2;
//
//			//Swipe from bottom to top
//			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx1, starty1, startx1, endy1, 2000);
//			Delay.Seconds(1);
//			Logger.logInfo("Entered Last name and First name",true);
//
		}
		
		public void SwitchAudiogramAvailableinEditScreen(bool on, bool alert)
		{
			string message=null;
			var switchAudiogram=KeywordImplementation.UIObject(By.Id("Switch_Cust_ExisitingAudiogram"),TimeSpan.FromSeconds(1));
			if((on.Equals(true) && switchAudiogram.Text.Equals("No")) ||  (on.Equals(false) && switchAudiogram.Text.Equals("Yes")))
			{
				switchAudiogram.Click();
				message=on?"Switch on the audiogram":"Switch off the audiogram";
				Logger.logInfo(message);
			}
			Logger.logSnapshot();
			if(KeywordImplementation.waitForObjectExist(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(2)))
			{
				if(alert.Equals(true))
				{
					KeywordImplementation.Click(By.Name("OK"));
					Logger.logInfo("OK the alert");
				}
				else
				{
					KeywordImplementation.Click(By.Name("Cancel"));
					Logger.logInfo("Cancel the alert");
				}
			}
		}

		public void SelectCustomerContinue()
		{
			KeywordImplementation.SwipeVerticle(3,0.80,0.50,By.Id("Btn_Cust_Continue"));
			KeywordImplementation.Click(By.Id("Btn_Cust_Continue"));
			Delay.Seconds(2);
		}
		
		public void TabVerifyActionBarDetailTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("Txt_ActionBar_DetailTitle"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}
		
		public void VerifyActionBarTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}
		
		public void SwipeForDOB(string Mmm,string dd, string yyyy)
		{
			string custDOBFormat = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id("EdTxt_Cust_Dob")).Text;
			string[] dobFormatArray=null;
			if(custDOBFormat.Contains('/'))
				dobFormatArray=custDOBFormat.Split('/');
			else if(custDOBFormat.Contains('-'))
				dobFormatArray=custDOBFormat.Split('-');
			KeywordImplementation.Click(By.Id("EdTxt_Cust_Dob"));
			IWebElement monthPicker=null;
			IWebElement monthValue=null;
			IWebElement datePicker=null;
			IWebElement dateValue=null;
			IWebElement yearPicker=null;
			IWebElement yearValue=null;
			
			if(dobFormatArray[0].Contains("d"))
			{
				datePicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementByXPath("//android.widget.NumberPicker[@index='0']");
				dateValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='0']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[0].Contains("M"))
			{
				monthPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='0']");
				monthValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='0']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[0].Contains("y"))
			{
				yearPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='0']");
				yearValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='0']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			
			if(dobFormatArray[1].Contains("d"))
			{
				datePicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']");
				dateValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[1].Contains("M"))
			{
				monthPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']");
				monthValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[1].Contains("y"))
			{
				yearPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']");
				yearValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='1']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			if(dobFormatArray[2].Contains("d"))
			{
				datePicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']");
				dateValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[2].Contains("M"))
			{
				monthPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']");
				monthValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			else if(dobFormatArray[2].Contains("y"))
			{
				yearPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']");
				yearValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElementByXPath("//android.widget.NumberPicker[@index='2']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			}
			
			int currDate=int.Parse(dateValue.Text.Split('.')[0]);
			int changeDate=int.Parse(dd);
			bool swipeDateDown=false;
			if(currDate>changeDate)
				swipeDateDown=true;
			
			int currYear=int.Parse(yearValue.Text.Split('.')[0]);
			int changeYear=int.Parse(yyyy);
			bool swipeYearDown=false;
			if(currYear>changeYear)
				swipeYearDown=true;
			
			var size= monthPicker.Size;
			
			/*****************************/
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height * 0.20);
			int startx = size.Width / 2;
			
			//Swipe from bottom to top
			while(!monthValue.Text.Contains(Mmm))
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()))).Swipe(monthPicker.Location.X+startx, monthPicker.Location.Y+starty, monthPicker.Location.X+startx, monthPicker.Location.Y+endy, 300);
				Delay.Ms(500);
			}
			
			Delay.Seconds(1);
			
			while(!dateValue.Text.Contains(dd))
			{
				if(!swipeDateDown)
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()))).Swipe(datePicker.Location.X+startx, datePicker.Location.Y+starty, datePicker.Location.X+startx, datePicker.Location.Y+endy, 300);
				else
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()))).Swipe(datePicker.Location.X+startx, datePicker.Location.Y+endy, datePicker.Location.X+startx, datePicker.Location.Y+starty, 300);
				
				Delay.Ms(500);
			}
			
			Delay.Seconds(1);
			
			while(!yearValue.Text.Contains(yyyy))
			{
				if(!swipeYearDown)
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()))).Swipe(yearPicker.Location.X+startx, yearPicker.Location.Y+starty, yearPicker.Location.X+startx, yearPicker.Location.Y+endy, 300);
				else
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()))).Swipe(yearPicker.Location.X+startx, yearPicker.Location.Y+endy, yearPicker.Location.X+startx, yearPicker.Location.Y+starty, 300);
				Delay.Ms(500);
			}
			
			Delay.Seconds(1);
			
			Logger.logSnapshot();
			KeywordImplementation.Click(By.Id("Btn_DatePicker_Done"));
		}

		public void ValidateDOB(string dd_mm_yyyy)
		{
			string custDOBFormat = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElement(By.Id("EdTxt_Cust_Dob")).Text;
			string[] custDobFormatArray=null;
			if(custDOBFormat.Contains('/'))
				custDobFormatArray=custDOBFormat.Split('/');
			else if(custDOBFormat.Contains('-'))
				custDobFormatArray=custDOBFormat.Split('-');
			
			string[] dobFormatArray=dd_mm_yyyy.Split('_');
			bool check=false;
			foreach(string dob in dobFormatArray)
			{
				check=false;
				foreach(string custDob in custDobFormatArray)
				{
					if(dob.Contains(custDob))
					{
						check=true;
						break;
					}
					
				}
				if(!check)
				{
					Logger.logFailure("DOB has not entered correctly",true);
					break;
				}
				
			}
			if(check)
			{
				Logger.logSuccess("DOB has entered correctly",true);
			}
		}

		public void TapEnterAudiogram()
		{
			Logger.logInfo("Tap 'Enter Audiogram'");
			KeywordImplementation.Click(By.Id("Txt_Fitting_EnterAudiogram"));
		}
		
		public void TapNew()
		{
			Logger.logInfo("Select New patient");
			KeywordImplementation.Click(By.Id("RdBtn_Cust_New"));
		}
		
		public void	DrawAudiogramPoints(string side, string points)
		{
			Logger.logInfo(string.Format("Audiogram points \"{0}\"",points));
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Right"));
			}
			if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Left"));
			}
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
				KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
			}
			string[] aPoints=points.Split(';');
			Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));

			
			IWebElement elementImgbtnAudiNext =null;
			IWebElement elementImgbtnAudiPrev = null;
			int check=0;
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				elementImgbtnAudiNext = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElement(By.Id("ImgBtn_Audi_Next"));
				elementImgbtnAudiPrev = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).FindElement(By.Id("ImgBtn_Audi_Prev"));
			}
			double heightFraction=20.0;
			if(ValidateYAxisContains("120"))
				heightFraction=26.0;
			else if(ValidateYAxisContains("110"))
				heightFraction=24.0;
			
			foreach(KeyValuePair<int, int> pair in auPoints)
			{
				string slider=null;
				if(pair.Key.ToString().Contains("250"))
				{
					slider="Seek_Audi_250Hz";
				}
				else if(pair.Key.ToString().Contains("500"))
				{
					slider="Seek_Audi_500Hz";
				}
				else if(pair.Key.ToString().Contains("1000"))
				{
					slider="Seek_Audi_1KHz";
				}
				else if(pair.Key.ToString().Contains("2000"))
				{
					slider="Seek_Audi_2KHz";
				}
				else if(pair.Key.ToString().Contains("4000"))
				{
					slider="Seek_Audi_4KHz";
				}
				else if(pair.Key.ToString().Contains("8000"))
				{
					slider="Seek_Audi_8KHz";
				}
				
				if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				{
					if((pair.Key.ToString().Contains("4000")|| pair.Key.ToString().Contains("8000")) && check==0)
					{
						elementImgbtnAudiNext.Click();
						elementImgbtnAudiNext.Click();
						elementImgbtnAudiNext.Click();
						check++;

					}
					else if((pair.Key.ToString().Contains("250")|| pair.Key.ToString().Contains("500")) && check==1)//||pair.Key.ToString().Contains("1000")|| pair.Key.ToString().Contains("2000")) &&check==1)
					{
						elementImgbtnAudiPrev.Click();
						elementImgbtnAudiPrev.Click();
						elementImgbtnAudiPrev.Click();
						check--;
					}
				}
				double temp=pair.Value/5.0;
				temp++;
				Delay.Seconds(1);
				string p=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PageSource;
				
				IWebElement element = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id(slider));
				double heightValuefraction=element.Size.Height/heightFraction;
				double yCoordinate=heightValuefraction*temp;
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Tap(1, (int)element.Location.X, (int)(element.Location.Y + yCoordinate), 500);
				//Dictionary<string, double> coords= new Dictionary<string, double>();
				//coords.Add("x",element.Location.X);
				//coords.Add("y",element.Location.Y+yCoordinate);
				//Delay.Seconds(1);
				//((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).ExecuteScript("mobile: tap",coords);
			}
		}
		
		public void	DrawAudiogramPointsWithPicker(string side, string points)
		{
			Logger.logInfo(string.Format("Draw audiogram points \"{0}\"",points));
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Right"));
			}
			if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Left"));
			}
			
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
				KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
			}
			
			string[] aPoints=points.Split(';');
			Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));
			
			IWebElement elementImgbtnAudiNext =null;
			IWebElement elementImgbtnAudiPrev = null;
			
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				elementImgbtnAudiNext = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id("ImgBtn_Audi_Next"));
				elementImgbtnAudiPrev = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id("ImgBtn_Audi_Prev"));
			}
			
			int check = 0;
			
			foreach(KeyValuePair<int, int> pair in auPoints)
			{
				string dbTxtBox=null;
				if(pair.Key.ToString().Contains("250"))
				{
					dbTxtBox="Txt_Audi_250HzVal";
				}
				else if(pair.Key.ToString().Contains("500"))
				{
					dbTxtBox="Txt_Audi_500HzVal";
				}
				else if(pair.Key.ToString().Contains("1000"))
				{
					dbTxtBox="Txt_Audi_1KHzVal";
				}
				else if(pair.Key.ToString().Contains("2000"))
				{
					dbTxtBox="Txt_Audi_2KHzVal";
				}
				else if(pair.Key.ToString().Contains("4000"))
				{
					dbTxtBox="Txt_Audi_4KHzVal";
				}
				else if(pair.Key.ToString().Contains("8000"))
				{
					dbTxtBox="Txt_Audi_8KHzVal";
				}

				if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				{
					if ((pair.Key.ToString().Contains("4000") || pair.Key.ToString().Contains("8000")) && check == 0)
					{
						elementImgbtnAudiNext.Click();
						elementImgbtnAudiNext.Click();
						elementImgbtnAudiNext.Click();
						check++;
					}
					else if ((pair.Key.ToString().Contains("250") || pair.Key.ToString().Contains("500")) && check == 1)//||pair.Key.ToString().Contains("1000")|| pair.Key.ToString().Contains("2000")) &&check==1)
					{
						elementImgbtnAudiPrev.Click();
						elementImgbtnAudiPrev.Click();
						elementImgbtnAudiPrev.Click();
						check--;
					}
				}
				Delay.Milliseconds(300);
				IWebElement element = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id(dbTxtBox));
				element.Click();
				SwipeForAudiDB(pair.Value.ToString());
			}
		}
		
		public void SwipeForAudiDB(string audiDB)
		{
			IWebElement audiPicker=null;
			IWebElement audiValue=null;
			
			audiPicker=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementByXPath("//android.widget.NumberPicker[@index='0']");
			audiValue=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementByXPath("//android.widget.NumberPicker[@index='0']/android.widget.EditText[@resource-id='android:id/numberpicker_input']");
			
			
			var size= audiPicker.Size;
			
			/*****************************/
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height * 0.20);
			int startx = size.Width / 2;
			
			//Swipe from bottom to top
			while(!audiValue.Text.Contains(audiDB))
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(audiPicker.Location.X+startx, audiPicker.Location.Y+starty, audiPicker.Location.X+startx, audiPicker.Location.Y+endy, 300);
				Delay.Ms(500);
			}
			
			Delay.Seconds(1);
			KeywordImplementation.Click(By.Id("Btn_NumberPicker_Done"));
		}
		
		public bool ValidateYAxisContains(string content)
		{
			var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
			bool check=false;
			var textViewArray=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementsByXPath("//android.widget.LinearLayout[contains(@resource-id,'Layout_Audi_YAxis')]//android.widget.TextView");
			
			foreach(var textView in textViewArray)
			{
				if(textView.Text.Contains(content))
				{
					check=true;
					break;
				}
			}
			return check;
		}
		
		public void TapOnRightButton(string message)
		{
			KeywordImplementation.Click(By.Id("Btn_ActionBar_Right"),message);
		}
		
		public void TapOnLeftButton(string message)
		{
			KeywordImplementation.Click(By.Id("Btn_ActionBar_Left"),message);
		}
		
		public void TapOnSettings(string message)
		{
			KeywordImplementation.Click(By.Id("Tab_Cutomer_Settings"),message);
		}
		
		public void TabTapOnRightButton(string message)
		{
			KeywordImplementation.Click(By.Id("Btn_ActionBar_DetailRight"),message);
		}
		
		public void EditAudiogram()
		{
			Logger.logInfo("Select the Audiogram Tab");
			KeywordImplementation.Click(By.Id("Layout_Monaural_Audiogram"));
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				TapOnRightButton("Tap on 'Edit' ");
			else
				TabTapOnRightButton("Tap on 'Edit' ");
		}
		
//		public void EditAudiogram(string side)
//		{
//			Logger.logInfo("Select the Audiogram");
//			TapOnOverviewScreen(side);
//			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
//				TapOnRightButton("Tap on 'Edit' ");
//			else
//				TabTapOnRightButton("Tap on 'Edit' ");
//		}
		
		public void ClickOnAlertMessage(string option)
		{
			KeywordImplementation.ClickOnAlertMessage(option);
		}
		
		public void	ValidateALertMessage( string message)
		{
			if(KeywordImplementation.waitForObjectExist(By.Name("OK"),TimeSpan.FromSeconds(2)))
			{
				if(KeywordImplementation.UIObject(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(1)).Text.Contains(message))
					Logger.logSuccess(string.Format("Popup displayed the message '{0}'",message));
				else
					Logger.logFailure(string.Format("Popup displayed the message '{0}'",KeywordImplementation.UIObject(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(1)).Text));
				Logger.logSnapshot();
			}
			
		}
		
		public void	ValidateALertMessage( string message, bool isAlert)
		{
			
			if(isAlert)
			{
				var alertMessage=KeywordImplementation.UIObject(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(3));
				if(alertMessage.Text.Contains(message))
				{
					Logger.logSuccess(string.Format("Popup displayed the message '{0}'",message));
					ClickOnAlertMessage("OK");
				}
				else
					Logger.logFailure(string.Format("Popup displayed the message '{0}'",alertMessage.Text));
			}
			else if(KeywordImplementation.waitForObjectExist(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(2)))
			{
				var alertMessage=KeywordImplementation.UIObject(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(3));
				if(alertMessage.Text.Contains(message))
				{
					Logger.logFailure(string.Format("Unexpected Popup displayed with the message '{0}'",message));
					ClickOnAlertMessage("OK");
				}
			}
			Logger.logSnapshot();
		}
		
		public void ValidateAudiogramPointsOnCustomerOverviewScreen(string side, string points, bool isExist)
		{
			if(isExist)
			{
				var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
				int starty = (int) (size.Height * 0.50);
				int endy = (int) (size.Height*0.80);
				int startx = size.Width / 2;
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx, starty, startx, endy, 1000);
				
				IWebElement audiogramGrid=null;
				if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
					audiogramGrid=KeywordImplementation.UIObject(By.Id("AudiogramGrid_RightEar"));
				else if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
					audiogramGrid=KeywordImplementation.UIObject(By.Id("AudiogramGrid_LeftEar"));
				
				if(audiogramGrid.GetAttribute("name").Contains(points))
					Logger.logSuccess("Audiogram point validation passed on customer overview screen");
				else
					Logger.logFailure("Audiogram point validation failed on customer overview scxreen",true);
			}
			else
			{
				Logger.logInfo(string.Format("'{0}' audiology do not exist on customer overview screen",side));
			}
		}
		
		public void TapOnOverviewScreen(string side)
		{
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("AudiogramGrid_RightEar"));
			else if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("AudiogramGrid_LeftEar"));
		}
		
		public void ValidateAudiogramPointsOnOverviewScreen(string side, string points)
		{
			Logger.logInfo(string.Format("Validate audiogram points \"{0}\"",points));
			//KeywordImplementation.Click(By.Id("Layout_Monaural_Audiogram"));
			
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("AudiogramGrid_RightEar"));
				KeywordImplementation.Click(By.Id("RdBtn_AudiReentry_Right"));
			}
			if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("AudiogramGrid_LeftEar"));
				KeywordImplementation.Click(By.Id("RdBtn_AudiReentry_Left"));
			}
			
			string[] aPoints=points.Split(';');
			Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));

			bool check=false;
			
			foreach(KeyValuePair<int, int> pair in auPoints)
			{
				check=false;
				if(pair.Key.ToString().Contains("250") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_250HzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("500") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_500HzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("1000") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_1KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("2000") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_2KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("4000") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_4KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("8000") && KeywordImplementation.UIObject(By.Id("Txt_AudiReentry_8KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}

				if(!check)
				{
					Logger.logFailure("Audiogram point validation failed",true);
					break;
				}
			}
			if(check)
				Logger.logSuccess("Audiogram point validation passed",true);
			
		}
		
		public void ValidateAudiogramPoints(string side, string points)
		{
			//Ranorex.Logger.logInfo(string.Format("Validate audiogram points \"{0}\"",points));
			
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Right"));
			}
			if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("RdBtn_Audi_Left"));
			}
			
			string[] aPoints=points.Split(';');
			//	Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));
			Dictionary<string, string> auPoints=aPoints.ToDictionary(sKey=>sKey.Split(',')[0],sValue=>sValue.Split(',')[1]);
			IWebElement elementImgbtnAudiNext =null;
			IWebElement elementImgbtnAudiPrev = null;
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				elementImgbtnAudiNext = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id("ImgBtn_Audi_Next"));
				elementImgbtnAudiPrev = ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElement(By.Id("ImgBtn_Audi_Prev"));
				
				elementImgbtnAudiPrev.Click();
				elementImgbtnAudiPrev.Click();
			}
			bool check=false;
			int count=0;
			
			foreach(KeyValuePair<string, string> pair in auPoints)
			{
				check=false;
				if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				{
					if((pair.Key.ToString().Contains("4000")|| pair.Key.ToString().Contains("8000")) && count==0)
					{
						elementImgbtnAudiNext.Click();
						elementImgbtnAudiNext.Click();
						count++;
						
					}
					else if((pair.Key.ToString().Contains("250")|| pair.Key.ToString().Contains("500")||pair.Key.ToString().Contains("1000")|| pair.Key.ToString().Contains("2000")) && count==1)
					{
						elementImgbtnAudiPrev.Click();
						elementImgbtnAudiPrev.Click();
						count--;
					}
					
				}
				if(pair.Key.ToString().Contains("250") && KeywordImplementation.UIObject(By.Id("Txt_Audi_250HzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("500") && KeywordImplementation.UIObject(By.Id("Txt_Audi_500HzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("1000") && KeywordImplementation.UIObject(By.Id("Txt_Audi_1KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("2000") && KeywordImplementation.UIObject(By.Id("Txt_Audi_2KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("4000") && KeywordImplementation.UIObject(By.Id("Txt_Audi_4KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}
				else if(pair.Key.ToString().Contains("8000") && KeywordImplementation.UIObject(By.Id("Txt_Audi_8KHzVal"),TimeSpan.FromSeconds(1)).Text.Contains(pair.Value.ToString()))
				{
					check=true;
				}

				if(!check)
				{
					Logger.logFailure("Audiogram point validation failed",true);
					break;
				}
			}
			if(check)
				Logger.logSuccess("Audiogram point validation passed",true);
		}
		
		public void ValidateCustomerEditableFields()
		{
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_LastName")).GetAttribute("focusable").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess("'Last Name' field is editable");
			else
				Logger.logFailure("'Last Name' field is not editable");
			
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_FirstName")).GetAttribute("focusable").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess("'First Name' field is editable");
			else
				Logger.logFailure("'First Name' field is not editable");
			
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_Dob")).GetAttribute("focusable").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess("'DOB' field is editable");
			else
				Logger.logFailure("'DOB' field is not editable");
			
			if(KeywordImplementation.UIObject(By.Id("Switch_Cust_ExisitingAudiogram")).GetAttribute("focusable").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess("'Audiogram available' field is editable");
			else
				Logger.logFailure("'Audiogram available' field is not editable");
			
			KeywordImplementation.Click(By.Id("Txt_Cust_Comment"));
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
			if(KeywordImplementation.waitForObjectExist(By.Id("EdTxt_Cust_Comment"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("'Notes' field is expanded");
			else
				Logger.logFailure("'Notes' field is not expanded");
		}

		public void VerifySwitchAudiogramAvailable(bool on)
		{
			var switchAudiogram=KeywordImplementation.UIObject(By.Id("Switch_Cust_ExisitingAudiogram"),TimeSpan.FromSeconds(1));
			if((on.Equals(true) && switchAudiogram.Text.Equals("Yes")) ||  (on.Equals(false) && switchAudiogram.Text.Equals("No")))
			{
				Logger.logSuccess("Switch Audiogram is " + switchAudiogram.Text);
			}
			else
			{
				Logger.logFailure("Switch Audiogram is " + switchAudiogram.Text);
			}
		}
		
		public void ValidateButtonExist(string buttonName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name(buttonName));
			if(KeywordImplementation.waitForObjectExist(By.Name(buttonName),TimeSpan.FromSeconds(2)))
				Logger.logSuccess(string.Format("'{0}' is exist",buttonName));
			else
				Logger.logFailure(string.Format("'{0}' does not exist",buttonName));
		}
		
		public void ValidateButtonEnabled(string buttonName, bool isEnabled)
		{
			Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Name(buttonName)).Enabled,isEnabled,string.Format("'{0}' is enabled",buttonName),string.Format("'{0}' is disabled",buttonName));
			
		}

		public void ValidateWorkflowEnabled(string workflow, bool isEnabled)
		{
			Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Name(workflow)).Enabled,isEnabled,string.Format("{0} is enabled",workflow),string.Format("{0} is disabled",workflow));
		}
		
//		public void ValidateContentsEnabled(string contents, bool isEnabled)
//		{
//			string[] ContentsArray = contents.Split(';');
//			
//			foreach(string cont in ContentsArray)
//			{
//				Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Name(cont)).Enabled,isEnabled,string.Format("{0} is enabled",cont),string.Format("{0} is disabled",cont));
//			}
//			
//		}
		
		public void ValidateRecommendationEnabled(string side, bool isEnabled)
		{
			if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Id("Layout_SideSelection_Left")).Enabled,isEnabled,string.Format("Recommendation is enabled for {0} side",side),string.Format("Recommendation is disabled for {0} side",side));
			else if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Id("Layout_SideSelection_Right")).Enabled,isEnabled,string.Format("Recommendation is disabled for {0} side",side),string.Format("Recommendation is enabled for {0} side",side));
		}
		
		public void ValidateSideRecommendation()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("Txt_SideSelection_RecommendedSideL"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Left side is Recommended");
			else if (KeywordImplementation.waitForObjectExist(By.Id("Txt_SideSelection_RecommendedSideR"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess(" Right side is Recommended");
		}
		
		public void SelectLanguage(string language)
		{
			//Logger.logInfo("Select the language "+language);
			if(language.Equals("English",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("English"));
			else if(language.Equals("French",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Français"));
			else if(language.Equals("Spanish",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Español"));
			else if(language.Equals("Hindi",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Hindi"));
			else if(language.Equals("Chinese",StringComparison.InvariantCultureIgnoreCase))
				//KeywordImplementation.Click(By.Name("中国"));
				KeywordImplementation.Click(By.Name("中国 (Simplified Chinese)"));
			Logger.logInfo("Select the language "+language,true);
			//Logger.logSnapshot();
		}
		
		public void ValidateCustomerScreen()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("RdBtn_Cust_New"),TimeSpan.FromSeconds(2)) && KeywordImplementation.waitForObjectExist(By.Id("RdBtn_Cust_List"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("Customer screen is opened with new and list segmented control",true);
			else if(KeywordImplementation.waitForObjectExist(By.Id("Tab_Cutomer_ClientList"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("Customer screen is opened with ClientList segmented control",true);
			else
				Logger.logFailure("Customer screen is not opened",true);
			
		}
		
		public void ValidateLastName(string lastName)
		{
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_LastName")).Text.Contains(lastName))
				Logger.logSuccess(string.Format("Last name '{0}' has found",lastName));
			else
				Logger.logFailure(string.Format("Last name '{0}' has not found",lastName));
		}
		
		public void ValidateFirstName(string firstName)
		{
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_FirstName")).Text.Contains(firstName))
				Logger.logSuccess(string.Format("First name '{0}' has found",firstName));
			else
				Logger.logFailure(string.Format("First name '{0}' has not found",firstName));
		}
		
		public void SearchCustomer(string firstName)
		{
			KeywordImplementation.TypeText(By.Id("EdTxt_Cust_Search"),firstName);
		}
		
		public void SelectCustomer(string lastName,string firstName)
		{
			Delay.Seconds(1);
			KeywordImplementation.Click(By.Name(lastName+", "+firstName));
			
			if(!KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title"),TimeSpan.FromSeconds(3)).Text.Equals(lastName+", "+firstName))
				KeywordImplementation.Click(By.Name(lastName+", "+firstName));
		}
		
		public void CloseApp()
		{
			TearDown.CloseApplication();
		}
		
		public void StartApp()
		{
			TestModuleRunner.Run(new STSAndBasic.AndroidSetUp());
		}
		
		public void AcceptAndContinueWelcomeScreen()
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("Btn_AppInfo_Accept"));
			Logger.logInfo("Welcome Page screen is available",true);
			KeywordImplementation.Click(By.Id("CheckBox_AcceptTAndC"));
			KeywordImplementation.Click(By.Id("Btn_AppInfo_Accept"));
		}
		
		public void EnterPartialCode()
		{
			Logger.logInfo("Enter partial code");
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code1"),"9457");
		}
		
		public void TapTryAgain()
		{
			Logger.logInfo("Tap on 'Try again'");
			KeywordImplementation.Click(By.Name("Try again"));
		}
		
		public void EnterCorrectCode()
		{
			Delay.Seconds(1);
			Logger.logInfo("Enter correct code");
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code1"),"8981");
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code2"),"9313");
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code3"),"9666");
		}
		
		public void EnterCorrectCode(string p1,string p2,string p3)
		{
			Logger.logInfo("Enter correct code");
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code1"),p1);
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code2"),p2);
			KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code3"),p3);
		}
		
		public void DoneAuthenticationScreen()
		{
			Logger.logInfo("Tap on Done");
			KeywordImplementation.Click(By.Name("Done"));
		}
		
		public void ValidateMonauralClusterSelected(string cluster,string side)
		{
			IWebElement clusterElement=null;
			clusterElement=KeywordImplementation.UIObject(By.Id("Txt_Monaural_ComfortProfile"));
			
			if(clusterElement.Text.Contains(cluster))
				Logger.logSuccess("Monaural cluster "+cluster+" is selected");
			else
				Logger.logFailure(string.Format("Cluster '{0}' is selected instead of '{1}'",clusterElement.Text,cluster));
		}
		
		public void ValidateBinauralClusterSelected(string cluster,string side)
		{
			IWebElement clusterElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				clusterElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_SoundProfileRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				clusterElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_SoundProfileLeft"));
			
			if(clusterElement.Text.Contains(cluster))
				Logger.logSuccess(string.Format("Cluster '{0}' is selcted for '{1}' side",cluster,side));
			else
				Logger.logFailure(string.Format("Cluster '{0}' is selected instead of '{1}' for '{2}' side",clusterElement.Text,cluster,side));
		}

		public void ValidateBinauralClusterNotSelected(string side)
		{
			bool clusterElementExist=false;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				clusterElementExist=KeywordImplementation.waitForObjectExist(By.Id("Txt_Binaural_SelectSoundProfileRIght"),TimeSpan.FromSeconds(2));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				clusterElementExist=KeywordImplementation.waitForObjectExist(By.Id("Txt_Binaural_SelectSoundProfileLeft"),TimeSpan.FromSeconds(2));
			
			if(clusterElementExist)
				Logger.logSuccess(string.Format("Profile is not selcted for '{0}' side",side));
			else
				Logger.logFailure(string.Format("Profile is selcted for '{0}' side",side));
		}

		public void ValidateMonauralCouplingSelected(string coupling, string side)
		{
			if(KeywordImplementation.UIObject(By.Id("Txt_Monaural_MoldSelected")).Text.Contains(coupling))
				Logger.logSuccess("Monaural coupling "+coupling+" is selected");
			else
				Logger.logFailure("Monaural coupling "+coupling+" is not selected");
			
			if(KeywordImplementation.UIObject(By.Id("Txt_Monaural_MoldSelected")).GetAttribute("name").Contains(side))
				Logger.logSuccess("Monaural coupling recommendation has given for "+side+" ear");
			else
				Logger.logFailure("Monaural coupling recommendation has not given for "+side+" ear");
		}
		
		public void ValidateBinauralCouplingSelected(string coupling,string side)
		{
			IWebElement couplingElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				couplingElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_MoldSelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				couplingElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_MoldSelectedLeft"));
			
			if(couplingElement.Text.Contains(coupling))
				Logger.logSuccess(string.Format("Coupling '{0}' is selcted for '{1}' side",coupling,side));
			else
				Logger.logFailure(string.Format("Coupling '{0}' is not selcted for '{1}' side",coupling,side));
		}

		public void ValidateMonauralHISelected(string HI,string side)
		{
			HI= HISelection(HI);
			if(HI.Contains(KeywordImplementation.UIObject(By.Id("Txt_Monaural_HISelected")).Text))
				Logger.logSuccess("Monaural HI "+KeywordImplementation.UIObject(By.Id("Txt_Monaural_HISelected")).Text+" is selected");
			else
				Logger.logFailure((string.Format("HI '{0}' is selected instead of '{1}'",KeywordImplementation.UIObject(By.Id("Txt_Monaural_HISelected")).Text,HI)),true);
			
			if(KeywordImplementation.UIObject(By.Id("Txt_Monaural_HISelected")).GetAttribute("name").Contains(side))
				Logger.logSuccess("Monaural HI recommendation has given for "+side+" ear");
			else
				Logger.logFailure("Monaural HI recommendation has not given for "+side+" ear",true);
		}
		
		public void ValidateBinauralHISelected(string HI,string side)
		{
			HI= HISelection(HI);
			IWebElement hiElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				hiElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_HISelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				hiElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_HISelectedLeft"));
			
			if(HI.Contains(hiElement.Text))
				Logger.logSuccess(string.Format("HI '{0}' is selected for '{1}' side",hiElement.Text,side));
			else
				Logger.logFailure((string.Format("HI '{0}' is selected for '{1}' side instead of '{2}'",hiElement.Text,side,HI)),true);
		}
		
		public void ValidateBinauralHINotSelected(string side)
		{
			bool hiElementExist=false;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				hiElementExist=KeywordImplementation.waitForObjectExist(By.Id("Txt_Binaural_SelectHearingSystemRight"),TimeSpan.FromSeconds(2));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				hiElementExist=KeywordImplementation.waitForObjectExist(By.Id("Txt_Binaural_SelectHearingSystemLeft"),TimeSpan.FromSeconds(2));
			
			if(hiElementExist)
				Logger.logSuccess(string.Format("HI is not selcted for side '{0}' ",side));
			else
				Logger.logFailure(string.Format("HI is already selcted for side '{0}' ",side));
		}
		
		public void ValidateSuitableHISelected(string HI,string side)
		{
			HI= HISelection(HI);
			if(KeywordImplementation.UIObject(By.Id("Txt_HIRecommended")).GetAttribute("name").Contains(side))
				Logger.logSuccess("Suitable HI has given for "+side+" ear");
			else
				Logger.logFailure("Suitable HI has not given for "+side+" ear");
			
			if(KeywordImplementation.UIObject(By.Id("Txt_HIRecommended")).GetAttribute("name").Contains(HI))
				Logger.logSuccess(string.Format("Suitable HI is '{0}'",HI));
			else
				Logger.logFailure(string.Format("Suitable HI is not '{0}'",HI));
		}
		
		public void SendDataToHearingAid(string side)
		{
			Logger.logInfo(string.Format("Tap on 'Send Data to Hearing Aid' for {0} side",side));
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_ConnectHearingSystemRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_ConnectHearingSystemLeft"));
		}
		
		public void ConnectHI()
		{
			Logger.logInfo("Tap on Connect HI");
			KeywordImplementation.Click(By.Id("Btn_HiPairing_Connect"));
		}
		
		public void ConfirmationToneYes()
		{
			ClickOnAlertMessage("OK");
			Logger.logInfo("Tap on Yes for the - hear the confirmation tone from the hearing aid");
			KeywordImplementation.Click(By.Id("Btn_HiPairing_Yes"));
			//		ValidateALertMessage("Pairing and initial programming complete.");
			ClickOnAlertMessage("OK");
		}

		public void TabConfirmationToneYes()
		{
			//ClickOnAlertMessage("OK");
			Logger.logInfo("Tap on Yes for the - hear the confirmation tone from the hearing aid");
			KeywordImplementation.Click(By.Id("Btn_HiPairing_Yes"));
			//		ValidateALertMessage("Pairing and initial programming complete.");
			ClickOnAlertMessage("OK");
		}
		
		public void SoundComfortOk()
		{
			//	ClickOnAlertMessage("OK");
			Logger.logInfo("Tap on ok");
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("Btn_SoundComfort_Testok"));
			KeywordImplementation.Click(By.Id("Btn_SoundComfort_Testok"));
			
		}
		
		public void SelectSide(string side)
		{
			Logger.logInfo("Tap on side "+side);
			KeywordImplementation.Click(By.Name(side));
		}

		public void VolumeIncrease()
		{
			var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
			/*****************************/
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.90);
			int startx = size.Width / 2;
			
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx, starty, startx, endy, 1000);
			
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Volume")));
			KeywordImplementation.Click(By.Id("ImgBtn_SoundComfort_VolumeIncrease"));
			KeywordImplementation.Click(By.Id("ImgBtn_SoundComfort_VolumeIncrease"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Volume")));
			Logger.logInfo("Validating Volume has changed");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}
		
		public void SoundSharper()
		{
			Bitmap imageBeforeSoundChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Seekbar_SoundComfort_Sharp")));
			KeywordImplementation.Click(By.Id("ImgBtn_SoundComfort_SharpIncrease"));
			KeywordImplementation.Click(By.Id("ImgBtn_SoundComfort_SharpIncrease"));
			Bitmap imageAfterSoundChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Seekbar_SoundComfort_Sharp")));
			Logger.logInfo("Validating Sound has changed");
			Logger.ValidateImagesChanged(imageBeforeSoundChange,imageAfterSoundChange,true);
		}
		
		public void ContinueWithLeftEar()
		{
			Delay.Seconds(2);
			Logger.logInfo("Continue With Left Ear");
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name("Continue With Left Ear"));
			KeywordImplementation.Click(By.Name("Continue With Left Ear"));
		}

		public void CloseSession()
		{
			Logger.logInfo("Close session");
			KeywordImplementation.Click(By.Id("Btn_SoundComfort_Close"));
			
			//Handling Rate the App pop-up
			if(KeywordImplementation.waitForObjectExist(By.Name("How do you like the app"),TimeSpan.FromSeconds(2)))
			{
				Logger.logInfo("Tap on 'Please remind me later'",true);
				KeywordImplementation.Click(By.Name("Please remind me later"));
			}
		}
		
		public void DeleteClient()
		{
			KeywordImplementation.Click(By.Id("Btn_Cust_Delete"));
			ClickOnAlertMessage("OK");
		}
		
		public void EditNotes(string notes)
		{
			Logger.logInfo("Edit the notes field");
			KeywordImplementation.Click(By.Id("Txt_Cust_Comment"));
			//((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
			KeywordImplementation.TypeText(By.Id("EdTxt_Cust_Comment"),notes);
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
		}
		
		public void EditPatient(string lastName,string firstName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.80,By.Id("EdTxt_Cust_LastName"));
			Logger.logInfo(string.Format("Edit the patient with info '{0}, {1}'",lastName,firstName));
			KeywordImplementation.TypeText(By.Id("EdTxt_Cust_LastName"),lastName);
			KeywordImplementation.TypeText(By.Id("EdTxt_Cust_FirstName"),firstName);
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
		}
		
		public void ValidatePatientExist(string lastName,string firstName)
		{
			Logger.logSnapshot();
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_LastName")).Text.Contains(lastName) && KeywordImplementation.UIObject(By.Id("EdTxt_Cust_FirstName")).Text.Contains(firstName))
				Logger.logSuccess(string.Format("Patient details '{0}, {1}' has found",lastName,firstName));
			else
				Logger.logFailure(string.Format("Patient details '{0}, {1}' has not found",lastName,firstName));
		}
		
		public void ValidateDateEmpty(bool empty)
		{
			string message=null;
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_Dob")).Text.Contains("yy")==empty)
			{
				message=empty?string.Format("DOB details has not found"):string.Format("DOB details '{0}' has found",KeywordImplementation.UIObject(By.Id("EdTxt_Cust_Dob")).Text);
				Logger.logSuccess(message);
			}
			else
			{
				message=empty?string.Format("DOB details '{0}' has found",KeywordImplementation.UIObject(By.Id("EdTxt_Cust_Dob")).Text):string.Format("DOB details has not found");
				Logger.logFailure(message);
			}
		}
		
		public void ValidateNotes(string  notes)
		{
			KeywordImplementation.Click(By.Id("Txt_Cust_Comment"));
			KeywordImplementation.Click(By.Id("EdTxt_Cust_Comment"));
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PressKeyCode(AndroidKeyCode.Back);
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Cust_Comment")).Text.Contains(notes))
				Logger.logSuccess(string.Format("Notes '{0}' has found",notes));
			else
				Logger.logFailure(string.Format("Notes '{0}' has not found",notes));
			
			//	BackAction();
		}
		
		public void ValidateDeleteClientOption()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("Btn_Cust_Delete"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("Delete client option exist");
			else
				Logger.logFailure("Delete client option does not exist");
		}
		
		public void ClearDateField()
		{
			Logger.logInfo("Clear the date field");
			KeywordImplementation.Click(By.Id("EdTxt_Cust_Dob"));
			KeywordImplementation.Click(By.Id("Btn_DatePicker_Clear"));
		}
		
		public void ValidateAudiogramToggle(bool toggle)
		{
			string message=null;
			var switchAudiogram=KeywordImplementation.UIObject(By.Id("Switch_Cust_ExisitingAudiogram"),TimeSpan.FromSeconds(1));
			if((toggle.Equals(true) && switchAudiogram.Text.Equals("Yes"))||(toggle.Equals(false) && switchAudiogram.Text.Equals("No")))
			{
				message=toggle?"Audiogram toggle is On":"Audiogram toggle is Off";
				Logger.logSuccess(message);
			}
			else
			{
				message=toggle?"Audiogram toggle is Off":"Audiogram toggle is On";
				Logger.logFailure(message);
			}
		}
		
		public void ValidateAppVersion()
		{
			string versionInfo=KeywordImplementation.UIObject(By.Id("Txt_Info_Msg6"),TimeSpan.FromSeconds(2)).Text;
			if((versionInfo.Split('.').Length-1)==3)
				Logger.logSuccess(string.Format("Application version {0} displayed is correct",versionInfo));
			else
				Logger.logFailure(string.Format("Application version {0} displayed is incorrect",versionInfo));
		}
		
//		public void TapThriceOn AppVersion()
//		{
//			IWebElement versionInfo=KeywordImplementation.UIObject(By.Id("Txt_Info_Msg6"),TimeSpan.FromSeconds(2));
//			((IOSDriver<IOSElement>) ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Tap(1, versionInfo, 0);
//			((IOSDriver<IOSElement>) ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Tap(1, versionInfo, 0);
//			((IOSDriver<IOSElement>) ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Tap(1, versionInfo, 0);
//		}
		
		public void  ContinueValidationScreen()
		{
			Logger.logSuccess("Continue with Validation screen");
			KeywordImplementation.Click(By.Name("Continue"));
		}
		
		public void  ContinueCustomerEarMoldScreen()
		{
			
			if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
			{
				Logger.logInfo("Continue with brand selection screen",true);
				KeywordImplementation.Click(By.Name("Done"));
			}
			
			Logger.logInfo("Continue with Customer Ear Molds screen",true);
			
			if(TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
				
				//Swipe from Bottom to Top and Top to bottom
				//Find swipe start and end point from screen's width and height.
				int starty=0,endy=0,startx=0;
				bool isTablet= size.Width>=600;
				starty = (int) (size.Height * 0.50);
				endy = (int) (size.Height*0.20);
				startx = size.Width / 2;
				int count=0;
				while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Id("Btn_CustomMolds_Continue"),TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
			}
			KeywordImplementation.Click(By.Id("Btn_CustomMolds_Continue"));			
		}
		
		public void SwitchUsageStatistics(bool on)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("Switch_Settings_Omniture"),TimeSpan.FromSeconds(5));
			if((on.Equals(true) && switchUsageStatistics.Text.Equals("Off")) ||  (on.Equals(false) && switchUsageStatistics.Text.Equals("On")))
			{
				switchUsageStatistics.Click();
				Logger.logSuccess("Usage statistics is " + switchUsageStatistics.Text);
			}
			Logger.logSnapshot();
		}
		
		public void ValidateUsageStatistics(bool isON)
		{
			var switchUsageStatistics=KeywordImplementation.UIObject(By.Id("Switch_Settings_Omniture"),TimeSpan.FromSeconds(1));
			if((isON.Equals(true) && switchUsageStatistics.Text.Equals("On")) ||  (isON.Equals(false) && switchUsageStatistics.Text.Equals("Off")))
				Logger.logSuccess("Usage statistics is "+switchUsageStatistics.Text);
			else
				Logger.logFailure("Usage statistics is "+switchUsageStatistics.Text);
			
			Logger.logSnapshot();
			
		}
		
		public void ValidateFAQInfoPage()
		{
			
			if(KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title"),TimeSpan.FromSeconds(2)).Text.Equals("FAQs"))
				Logger.logSuccess("FAQ page has opened");
			else
				Logger.logFailure("FAQ page has not opened");
		}
		
		public void SelectPageFromFAQ(string pageName)
		{
			Logger.logInfo(string.Format("Select FAQ {0}",pageName));
			if(pageName.Equals("Why do I need an access code?",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Why do I need an access code?"));
			Delay.Seconds(1);
		}
		
		public void ValidateOptionExist(string optionName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name(optionName));
			Logger.ConditionalValidation(KeywordImplementation.waitForObjectExist(By.Name(optionName),TimeSpan.FromSeconds(2)),true,string.Format("Option {0} exist",optionName),string.Format("Option {0} does not exist",optionName));
		}
		
		public void TapMoreInformation()
		{
			Logger.logInfo("Tap 'More information'");
			KeywordImplementation.Click(By.Id("Txt_AppInfo_ReadImpInfo"));
		}

		public void ValidateCESign()
		{
			
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
				
				//Swipe from Bottom to Top and Top to bottom
				//Find swipe start and end point from screen's width and height.
				int starty = (int) (size.Height * 0.50);
				int endy = (int) (size.Height*0.20);
				int startx = size.Width / 2;
				
				int count=0;
				while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.ClassName("android.widget.ImageView"),TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
			}
			if(KeywordImplementation.waitForObjectExist(By.ClassName("android.widget.ImageView"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("CE sign exists");
			else
				Logger.logFailure("CE sign does not exist");
			Logger.logSnapshot();
		}
		
		public void ValidateBrandSpecificDetails()
		{
			if(TestSuite.Current.Parameters["Brand"].Equals("Signia",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.ValidateContents("Siemens BASIC App;Signia GmbH");
			}
			else if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.ValidateContents("Fit2Go App;Sivantos GmbH");
			}
		}
		
		public void SelectLanguageFromSettings()
		{
			KeywordImplementation.Click(By.Id("Txt_Settings_Msg1"),"Open the Language screen");
		}
		
		public void CancelAuthenticationScreen()
		{
			Logger.logInfo("Authentication  screen is Cancelled");
			KeywordImplementation.Click(By.Name("Cancel"));
			Logger.logSnapshot();
		}
		
		public void ValidateAppRunning(bool isRunning)
		{
			string applicationName=string.Empty;
			string message=null;
			
			if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
				applicationName="com.connexx.fit2go";
			else if(TestSuite.Current.Parameters["Brand"].Equals("Signia",StringComparison.CurrentCultureIgnoreCase))
				applicationName="com.signia.mobilefitting";
			
			if(((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).PageSource.Contains(applicationName)==isRunning)
			{
				message=isRunning?"App is running":"App has closed";
				Logger.logSuccess(message);
				if(!isRunning)
					CloseApp();
			}
			else
			{
				message=isRunning?"App has closed":"App is running";
				Logger.logFailure(message);
			}
		}

		public void SelectCountry(string country)
		{
			KeywordImplementation.SwipeVerticle(20,0.50,0.20,By.Name(country));
			KeywordImplementation.Click(By.Name(country));
		}
		
		public void TapContinue()
		{
			Logger.logInfo("Tap on 'Continue'");
			
			if(TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				var size=((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Manage().Window.Size;
				
				//Swipe from Bottom to Top and Top to bottom
				//Find swipe start and end point from screen's width and height.
				int starty = (int) (size.Height * 0.50);
				int endy = (int) (size.Height*0.20);
				int startx = size.Width / 2;
				
				int count=0;
				while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Name("Continue"),TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
			}
			KeywordImplementation.Click(By.Name("Continue"));
			if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Name("Done"));
			}
			
		}
		
		public void TapOnWithContentDesc(string objectName)
		{
			string contentDisc=null;
			objectName = HISelection(objectName);
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				contentDisc=objectName+"_"+KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title")).Text;
			else
				contentDisc=objectName+"_"+KeywordImplementation.UIObject(By.Id("Txt_ActionBar_DetailTitle")).Text;

			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.XPath("//*[contains(@content-desc,'"+contentDisc+"')]"));
			KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'"+contentDisc+"')]"));
		}
		
		public void TapNeedAnAccessCode()
		{
			Logger.logInfo("Tap on 'Need An Access Code'");
			KeywordImplementation.Click(By.Id("Txt_Legitimation_NeedCode"));
		}
		
		public void ValidateTryAgainExists()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("Btn_Legitimation_TryAgain"),TimeSpan.FromSeconds(1)))
				Logger.logSuccess("Try again exists");
			else
				Logger.logFailure("Try again does not exists");
		}

		public void ValidatePrePopulatedEmail()
		{
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_To")).Text.Contains("@") && KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_To")).Text.Contains("."))
				Logger.logSuccess(string.Format("Pre-populated mail id is'{0}'",KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_To")).Text));
			else
				Logger.logFailure(string.Format("Pre-populated e-mail id '{0}' is not correct",KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_To")).Text));
			
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_Subject")).Text.Length!=0)
				Logger.logSuccess(string.Format("Pre-populated mail subject is '{0}'",KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_Subject")).Text));
			else
				Logger.logFailure("Pre-populated subject is empty");
			
			if(KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_MailContent")).Text.Length!=0)
				Logger.logSuccess(string.Format("Pre-populated mail contents is'{0}'",KeywordImplementation.UIObject(By.Id("EdTxt_Legitimation_MailContent")).Text));
			else
				Logger.logFailure("Pre-populated mail content is empty");
			Logger.logSnapshot();
			
		}

		public void SwipePatientLeft(string lastName,string firstName)
		{
			var client=	KeywordImplementation.UIObject(By.Name(lastName+", "+firstName));
			
			var size= client.Size;
			
			int startx = (int) (size.Width * 0.70);
			int endx = (int) (size.Width * 0.30);
			int starty = size.Height / 2;
			
			//Swipe from right to left
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver())).Swipe(client.Location.X+(startx-1), client.Location.Y+(starty-1), client.Location.X+endx, client.Location.Y+starty, 300);
			Delay.Ms(500);
			
		}
		
		public void TapOnRecommendedHearingAid(string objectName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Id("Txt_HIRecommended"));
			KeywordImplementation.Click(By.Name(objectName));
		}
		
		public void ValidateTrashToolTipExist(bool isExist)
		{
			Delay.Seconds(2);
			Logger.ConditionalValidation(KeywordImplementation.waitForObjectExist(By.Id("Layout_Audi_Delete"),TimeSpan.FromSeconds(1)),isExist,"Trash icon is exist","Trash icon does not exist");
		}
		
		public void ValidateTrashToolTipExist(string side,bool isExist)
		{
			//'TrashCan_'+'"+side+"'
			var contDesc= "TrashCan_"+side;
			Logger.ConditionalValidation(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@content-desc,'"+side+"')]"),TimeSpan.FromSeconds(1)),isExist,"Trash icon is exist in "+side+" side","Trash icon does not exist in "+side+" side");
		}
		
		public void TapOnTrashToolTip()
		{
			KeywordImplementation.Click(By.Id("Layout_Audi_Delete"));
		}
		
		public void VerifyTextHighlighted(string objectName)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name(objectName));
			if(KeywordImplementation.UIObject(By.Name(objectName)).GetAttribute("selected").Equals("true",StringComparison.CurrentCultureIgnoreCase))
				Logger.logSuccess(string.Format("'{0}' is highlighted",objectName));
			else
				Logger.logFailure(string.Format("'{0}'is not highlighted",objectName));
		}
		
		public void TapOnMonauralHI(string message)
		{
			Logger.logInfo(message);
			KeywordImplementation.Click(By.Id("Txt_Monaural_HISelected"));
		}
		
		public void TapOnBinauralHI(string message, string side)
		{
			Logger.logInfo(message);
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_HISelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_HISelectedLeft"));
		}
		
		public void SelectCouplingIfAvailable(bool isCouplingAvailable,string coupling)
		{
			if(isCouplingAvailable)
			{
				TapOnWithContentDesc(coupling);
			}
		}

		public void ValidateRecommendedHI(string HI,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.80,By.Name("Recommended"));
			
			string recommendedHI=KeywordImplementation.UIObject(By.Id("Txt_HIRecommended")).GetAttribute("name");
			
			if(recommendedHI.Contains(side))
				Logger.logSuccess("Recommended HI has given for "+side+" ear");
			else
				Logger.logFailure("Recommended HI has not given for "+side+" ear");
			
			if(recommendedHI.Contains(HI))
				Logger.logSuccess(string.Format("Recommended HI is '{0}'",HI));
			else
				Logger.logFailure(string.Format("Recommended HI is '{0}' instead of '{1}'",recommendedHI,HI));
		}

		public void ValidateRecommendedCoupling(string coupling,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Name("Recommended"));
			
			string recommendedCoupling=KeywordImplementation.UIObject(By.Id("Txt_CouplingRecommended")).GetAttribute("name");
			
			if(recommendedCoupling.Contains(side))
				Logger.logSuccess("Recommended coupling has given for "+side+" ear");
			else
				Logger.logFailure("Recommended coupling has not given for "+side+" ear");
			
			if(recommendedCoupling.Contains(coupling))
				Logger.logSuccess(string.Format("Recommended coupling is '{0}'",coupling));
			else
				Logger.logFailure(string.Format("Recommended coupling is '{0}' instead of '{1}'",recommendedCoupling,coupling));
		}

		public void ValidateSuitableCluster(string cluster,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Name("Suitable"));
			
			string suitableCluster=KeywordImplementation.UIObject(By.Id("Txt_ClusterRecommended")).GetAttribute("name");
			
			if(suitableCluster.Contains(side))
				Logger.logSuccess("Suitable cluster has given for "+side+" ear");
			else
				Logger.logFailure("Suitable cluster has not given for "+side+" ear");
			
			if(suitableCluster.Contains(cluster))
				Logger.logSuccess(string.Format("Suitable cluster is '{0}'",cluster));
			else
				Logger.logFailure(string.Format("Suitable cluster is '{0}' instead of '{1}'",suitableCluster,cluster));
		}
		
	}
}
