
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

// offered to the public domain for any use with no restriction
// and also with no warranty of any kind, please enjoy. - David Jeske.

// simple HTTP explanation
// http://www.jmarshall.com/easy/http/

namespace LogCapturer.HttpWebHook
{


	public class MyHttpServer : HttpServer, ILogReader
	{
		private List<LogEntry> logDictionary = new List<LogEntry>();
		
		public MyHttpServer(IPAddress localAdr, int port)
			: base(localAdr, port)
		{
			Console.WriteLine("HTTP Server started at  " + this.getHostedLocalAddress().ToString() + ":" + this.getListenerPort());
		}

		public override void handleGETRequest(HttpProcessor p)
		{
			throw new NotImplementedException();

		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
		{
			//https://github.com/appium/appium/issues/1149
			//Content-Length property was not coming in case of Appium Log WebHook
			//
			throw new NotImplementedException();
		}

		public override void handlePOSTDataWithoutContentLength(string line)
		{
			if (line.StartsWith("{"))
			{
				AppiumLogPacket log = AppiumLogPacket.fromJson(line);
				this.logDictionary.Add(new LogEntry(DateTime.Now, log.@params.message));
				Console.WriteLine(log.@params.message);
			}
		}
		
		public void startReading()
		{
			base.listen();
		}
		
		public List<LogEntry> getLogLines()
		{
			return this.logDictionary;
		}
		
		public void clear()
		{
			this.logDictionary.Clear();
		}
				
	}

	

}



