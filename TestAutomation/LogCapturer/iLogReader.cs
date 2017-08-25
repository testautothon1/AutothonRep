/*
 * Created by Ranorex
 * User: ic014157
 * Date: 4/11/2016
 * Time: 10:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

/// <summary>
/// Description of ILogReader.
/// </summary>
namespace LogCapturer{
	public interface ILogReader : IDisposable
	{
		
		void startReading();
		
		List<LogEntry> getLogLines();
		
		void clear();
	}
}
