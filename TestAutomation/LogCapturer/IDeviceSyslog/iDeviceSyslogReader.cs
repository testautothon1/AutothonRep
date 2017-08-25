using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ranorex;

namespace LogCapturer.IDeviceSyslog
{
	public class iDeviceSyslogReader : ILogReader
	{
		private List<LogEntry> logDictionary = new List<LogEntry>();
		private string udid=string.Empty;
		Process iDeviceSyslogProcess = null;
		//System.DateTime datetime;
		public  iDeviceSyslogReader(string udid)
		{
			this.udid=udid;
		}
//		public AdbLogcatReader(string udid)
//		{
//			this.udid=udid;
//		}
		public void startReading()
		{
			String iOS_HOME = System.Environment.GetEnvironmentVariable("iOS_HOME");

			String iOSPath = System.IO.Path.Combine(iOS_HOME, "iMobileDevice", "idevicesyslog.exe");
			iOSPath = "\"" + iOSPath + "\"";
			
			
			string argument= @"/C ""idevicesyslog | grep -w ""changed""";
			//string argument= @"/C ""adb -s udid logcat";
			//argument=argument.Replace("udid",udid);

			var processStartInfo = new ProcessStartInfo("cmd.exe", argument)
			{
				UseShellExecute = false,
				RedirectStandardOutput = true
			};
			iDeviceSyslogProcess = Process.Start(processStartInfo);
			iDeviceSyslogProcess.EnableRaisingEvents=true;
			iDeviceSyslogProcess.StartInfo.CreateNoWindow=true;
			iDeviceSyslogProcess.BeginOutputReadLine();
			iDeviceSyslogProcess.OutputDataReceived += this.p_OutputDataReceived;
			iDeviceSyslogProcess.WaitForExit();
		}

		private void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
		{
			//Report.Info(e.Data);
			//Regex rgx = new Regex(@"\d{1,2}/\d{1,2}/\d{4} \d{1,2}:\d{1,2}:\d{1,2} \w{2}");
			
			if(!String.IsNullOrEmpty(e.Data) && (e.Data.Contains("Program changed") || e.Data.Contains("Volume changed")))
			{
				//Report.Info(e.Data);
				//Match match=rgx.Match(e.Data);
				//System.DateTime dt = Convert.ToDateTime(match.ToString());
				this.logDictionary.Add(new LogEntry(System.DateTime.Now, e.Data));
			}
			
		}
		
		public List<LogEntry> getLogLines()
		{
			List<LogEntry> tempDict=new List<LogEntry>();
			tempDict.Add(logDictionary[logDictionary.Count-1]);
			return tempDict;
			//return logDictionary;
		}
		
		public void clear()
		{
			logDictionary.Clear();
		}
		
		public void Dispose()
		{
			if(iDeviceSyslogProcess!=null)
				iDeviceSyslogProcess.Kill();
//			foreach(Process p in Process.GetProcessesByName("Grep.exe"))
//			{
//				p.Kill();
//			}
		}
	}
}
