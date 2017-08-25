/*
 * Created by Ranorex
 * User: IC019092
 * Date: 8/10/2016
 * Time: 2:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using Ranorex;
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
using System.Collections.Generic;


namespace AppLayer
{
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public enum LogLevel
	{
		Always,
		Debug,
		Trace,
		Warn,
		Info,
		Success,
		Failure,
		Fatal,
		Error
	}
	
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public class Logger
	{
		public Logger()
		{
		}
		
		private static  ReportLevel ConvertToRanorexLevel(LogLevel logLevel)
		{
			// Don't make this dictionary static. It should only be generated if ranorex is installed on the system
			Dictionary<LogLevel, ReportLevel> dict = new Dictionary<LogLevel, ReportLevel>
			{
				{LogLevel.Always, ReportLevel.Always},
				{LogLevel.Warn, ReportLevel.Warn},
				{LogLevel.Fatal, ReportLevel.Failure},
				{LogLevel.Error, ReportLevel.Error},
				{LogLevel.Debug, ReportLevel.Debug},
				{LogLevel.Info, ReportLevel.Info},
				{LogLevel.Success, ReportLevel.Success},
				{LogLevel.Failure, ReportLevel.Failure},
				{LogLevel.Trace, ReportLevel.None}
			};
			ReportLevel reportLevel;

			if (!dict.TryGetValue(logLevel, out reportLevel))
			{
				return ReportLevel.None;
			}
			return reportLevel;
		}
		
		
		
		private static FileInfo getSnapshotFileInfo(IWebDriver driver)
		{
			Delay.Milliseconds(500);
			FileInfo outFile = new FileInfo(Path.GetTempFileName().Replace(".tmp", ".bmp"));
			((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(outFile.FullName, System.Drawing.Imaging.ImageFormat.Jpeg);
			return outFile;
		}
		
		public static void logSnapshot()
		{
			logSnapshot(null);
		}
		
		private static Bitmap CropImage(Bitmap source,Rectangle section)
		{
			Bitmap bmp=new Bitmap(section.Width,section.Height);
			Graphics g=Graphics.FromImage(bmp);
			g.DrawImage(source,0,0,section,GraphicsUnit.Pixel);
			return bmp;
		}
		
		public static Image CaptureSnapshot(By by)
		{
			Bitmap savedImage=null;
			Rectangle section=Rectangle.Empty;

			if(by!=null)
			{
				var size=KeywordImplementation.UIObject(by).Size;
				var x=KeywordImplementation.UIObject(by).Location.X;
				var y= KeywordImplementation.UIObject(by).Location.Y;
				
				//((IOSDriver<IOSElement>)Accessor.getDriver()).Rotate(ScreenOrientation.Landscape);
				Bitmap source=new Bitmap(getSnapshotFileInfo(Accessor.getDriver()).ToString());
				if(!Accessor.driverIsForIOS())
					section=new Rectangle(x,y,size.Width,size.Height);
				else
				{
					var width=Accessor.getDriver().Manage().Window.Size.Width;
					if(width==414)
						section=new Rectangle(x*3,y*3,size.Width*3,size.Height*3);
					else
						section=new Rectangle(x*2,y*2,size.Width*2,size.Height*2);
					
				}
				savedImage=CropImage(source,section);
				
			}
			else
			{
				savedImage=new Bitmap(getSnapshotFileInfo(Accessor.getDriver()).FullName);
				
			}
			return savedImage;
			
		}
		
		public static void logSnapshot(By by)
		{
			Image image = CaptureSnapshot(by);
			Ranorex.Report.LogData(ReportLevel.Info,"",image);			
		}
		
		public static void ValidateImagesChanged(Bitmap before,Bitmap after, bool isImageChanged)
		{
			ConditionalValidation(Ranorex.Imaging.Compare( before, after)!= 1,isImageChanged,"Before and after images are different","Before and after images are same");
			Report.LogData(ReportLevel.Info,"Image Before",before);
			Report.LogData(ReportLevel.Info,"Image After",after);
		}
		
		public static void log(LogLevel logLevel, string message)
		{
			
			log(logLevel,message,false);
		}
		
		public static void log(LogLevel logLevel, string message, bool isSnapshot)
		{
			ReportLevel rLevel = ConvertToRanorexLevel(logLevel);
			Report.Log(rLevel,message);
			if(isSnapshot)
				logSnapshot();
		}
		
		public static void logInfo(string message)
		{
			logInfo(message,false);
		}
		
		public static void logInfo(string message, bool isSnapshot)
		{
			Report.Info(message);
			if(isSnapshot)
				logSnapshot();
		}
		
		public static void logSuccess(string message)
		{
			logSuccess(message,false);
		}
		
		public static void logSuccess(string message, bool isSnapshot)
		{
			Report.Success(message);
			if(isSnapshot)
				logSnapshot();
		}
		
		public static void logFailure(string message)
		{
			logFailure(message,false);
		}
		
		public static void logFailure(string message, bool isSnapshot)
		{
			Report.Failure(message);
			if(isSnapshot)
				logSnapshot();
		}
		
		public static void validateFailure(string message)
		{
			validateFailure(message,false);
		}
		
		public static void validateFailure(string message, bool isSnapshot)
		{
			Validate.Fail(message);
			if(isSnapshot)
				logSnapshot();
		}
		
		public static void ConditionalValidation(bool validationLogic, bool condition,string positiveMessage,string negativeMessage)
		{
			ConditionalValidation(validationLogic,condition,positiveMessage,negativeMessage,false);
		}
		
		public static void ConditionalValidation(bool validationLogic, bool condition,string positiveMessage,string negativeMessage,bool isSnapshot)
		{
			string message=null;
			if(validationLogic==condition)
			{
				message= condition? positiveMessage:negativeMessage;
				logSuccess(message,isSnapshot);
			}
			else
			{
				message=condition? negativeMessage:positiveMessage;
				logFailure(message,isSnapshot);
			}
		}
		
	}
}
