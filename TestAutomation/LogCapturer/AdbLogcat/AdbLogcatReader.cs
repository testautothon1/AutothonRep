using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LogCapturer.AdbLogcat
{
	public class AdbLogcatReader : ILogReader
	{
		private List<LogEntry> logDictionary = new List<LogEntry>();
		private string udid=string.Empty;
		Process adbLogcatProcess = null;
		public AdbLogcatReader()
		{
			
		}
		public AdbLogcatReader(string udid)
		{
			this.udid=udid;
		}
		
//		public void startReading()
//		{
//			String ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
//
//			String adbPath = System.IO.Path.Combine(ANDROID_HOME, "platform-tools", "adb.exe");
//			adbPath = "\"" + adbPath + "\"";
//
//			//first clear all logs
//			 Process.Start(adbPath, "-s "+udid+" logcat -c");
//
//			//Process p = Process.Start(adbPath, "logcat");
//			var processStartInfo = new ProcessStartInfo(adbPath, "-s "+udid+" logcat")
//			{
//				UseShellExecute = false,
//				RedirectStandardOutput = true
//
//			};
//
//			adbLogcatProcess = Process.Start(processStartInfo);
//
//			//Manish Code
//			adbLogcatProcess.EnableRaisingEvents=true;
//			adbLogcatProcess.StartInfo.CreateNoWindow=true;
//
//			//Manish Commented
//			adbLogcatProcess.BeginOutputReadLine();
//			adbLogcatProcess.OutputDataReceived += this.p_OutputDataReceived;
//			adbLogcatProcess.WaitForExit();
//		}

		
		public void startReading()
		{
			String ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");

			String adbPath = System.IO.Path.Combine(ANDROID_HOME, "platform-tools", "adb.exe");
			adbPath = "\"" + adbPath + "\"";

			//first clear all logs
			Process.Start(adbPath, "-s "+udid+" logcat -c");

			//string argument= @"/C ""adb -s udid logcat | grep ""Program changed""";
			string argument= @"/C ""adb -s udid logcat | findstr /C:""Program changed""";
			//string argument= @"/C ""adb -s udid logcat";
			argument=argument.Replace("udid",udid);

			var processStartInfo = new ProcessStartInfo("cmd.exe", argument)
			{
				UseShellExecute = false,
				RedirectStandardOutput = true
			};
			
			adbLogcatProcess = Process.Start(processStartInfo);
			adbLogcatProcess.EnableRaisingEvents=true;
			adbLogcatProcess.StartInfo.CreateNoWindow=true;
			adbLogcatProcess.BeginOutputReadLine();
			adbLogcatProcess.OutputDataReceived += this.p_OutputDataReceived;
			adbLogcatProcess.WaitForExit();
		}
		
		private void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
		{
			//Ranorex.Report.Info(e.Data);
			//if(!String.IsNullOrEmpty(e.Data))
			this.logDictionary.Add(new LogEntry(DateTime.Now, e.Data));
			
		}
		
		public List<LogEntry> getLogLines()
		{
			return logDictionary;
		}
		
		public void clear()
		{
			logDictionary.Clear();
		}
		
		public void Dispose()
		{
			foreach(Process process in Process.GetProcessesByName("adb"))
			{
				process.Kill();
			}
//			if(adbLogcatProcess!=null)
//				adbLogcatProcess.Kill();
//
			
		}
	}
}
