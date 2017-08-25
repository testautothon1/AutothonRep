
using LogCapturer.AdbLogcat;
using LogCapturer.IDeviceSyslog;
using LogCapturer.HttpWebHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Ranorex;

namespace LogCapturer
{
	public class Capturer : IDisposable
	{

		private static Capturer logCapturer =null;
		private ILogReader logReader;
		private Boolean isAdb;
		
		private Capturer(){
			//to enforce singleton
		}
		
		public static Capturer getInstance()
		{
			if(logCapturer == null)
				logCapturer = new Capturer();
			
			return logCapturer;
		}
		
		
		
		public void init(Boolean useHttpServer, string udid="")
		{
			if (useHttpServer)
			{
				logReader= new MyHttpServer(IPAddress.Any, 8080);

				Thread thread = new Thread(new ThreadStart(logReader.startReading));
				thread.Start();
			}
//			else
//			{
//				logReader = new AdbLogcatReader(udid);
//				Thread thread = new Thread(new ThreadStart(logReader.startReading));
//				thread.Start();
//			}
			else
			{
				logReader = new iDeviceSyslogReader(udid);
				Thread thread = new Thread(new ThreadStart(logReader.startReading));
				thread.Start();
			}
			clear();
		}
		
		
		public void init(Boolean useHttpServer, Boolean isAdb,string udid="")
		{
			this.isAdb=isAdb;
			if (useHttpServer)
			{
				logReader= new MyHttpServer(IPAddress.Any, 8080);

				Thread thread = new Thread(new ThreadStart(logReader.startReading));
				thread.Start();
			}
			else if(isAdb)
			{
				logReader = new AdbLogcatReader(udid);
				Thread thread = new Thread(new ThreadStart(logReader.startReading));
				thread.Start();
			}
			else
			{
				logReader = new iDeviceSyslogReader(udid);
				Thread thread = new Thread(new ThreadStart(logReader.startReading));
				thread.Start();
			}
			
		}
		
		public List<LogEntry> getLogs()
		{
			if(logReader==null)
				throw new Exception("Use the Init method first");
			
			return logReader.getLogLines();
		}
		
		public void VerifyLogs(String expectedLog)
		{
			Delay.Seconds(4);
			if(isAdb)
			{
				foreach(System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("adb"))
				{
					process.Kill();
				}
				
			}
			else
			{
				foreach(System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("idevicesyslog"))
				{
					process.Kill();
				}
			}
			Delay.Seconds(1);
			
			foreach(LogEntry log in Capturer.getInstance().getLogs().ToArray())
			{
				if(log.message.Contains(expectedLog))
				{
					Ranorex.Report.Success("VerifyLogs", log.message);
					return;
				}
			}
			
			Ranorex.Report.Log(ReportLevel.Failure, "'" + expectedLog + "' not found");
			//getSnapshot(Accessor.getDriver());
		}
		
	
		
		
//		public void VerifyLogs(String expectedLog)
//		{
//			Delay.Seconds(1);
//			List<LogEntry> logentries=Capturer.getInstance().getLogs();//.ToArray();
//			foreach(LogEntry log in logentries)
//			{
//				if(log.message.Contains(expectedLog))
//				{
//					Ranorex.Report.Success("VerifyLogs", log.message);
//					return;
//				}
//			}
//
//			Ranorex.Report.Log(ReportLevel.Failure, "'" + expectedLog + "' not found");
//			//getSnapshot(Accessor.getDriver());
//		}
//
		public void clear()
		{
			this.logReader.clear();
		}
		
		public void Dispose()
		{
//			if(this.logReader!= null)
//			{
//				this.logReader.Dispose();
//				this.logReader = null;
//			}
		}
	}
}