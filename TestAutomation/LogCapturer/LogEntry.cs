/*
 * Created by Ranorex
 * User: ic014157
 * Date: 4/11/2016
 * Time: 11:19 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

/// <summary>
/// Description of Log.
/// </summary>
namespace LogCapturer{
	public class LogEntry
	{
		public LogEntry(DateTime time, String message){
			this.time = time;
			this.message = message;
			
		}
		
		public LogEntry(DateTime time,String logLevel, String message){
			this.time = time;
			this.message = message;
			this.logLevel = logLevel;
		}
		
		public System.DateTime time;
		public String logLevel;
		public String message;
		
		public override string ToString()
		{
			return base.ToString() + " " + this.message;
		}
	}
}
