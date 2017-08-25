using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Net;
using System.Threading;

namespace LogCapturer.HttpWebHook
{
	public abstract class HttpServer :IDisposable
	{

		protected int port;
		protected IPAddress localAdr;

		TcpListener listener;
		private bool is_active = true;

		public HttpServer(IPAddress localAdr, int port)
		{
			this.localAdr = localAdr;
			this.port = port;
		}

		public IPAddress getHostedLocalAddress()
		{
			return this.localAdr;
		}

		public int getListenerPort()
		{
			return this.port;
		}

		protected void listen()
		{
			listener = new TcpListener(localAdr, port);
			listener.Start();
			while (is_active)
			{
				try{
					TcpClient s = listener.AcceptTcpClient();
					HttpProcessor processor = new HttpProcessor(s, this);
					Thread thread = new Thread(new ThreadStart(processor.process));
					thread.Start();
					Thread.Sleep(1);
				}catch(Exception ex){
					if(is_active)
						throw ex;
					
					//most probably, the Dispose method of this class has been called
					// lets silently catch this exception.
				}
				
			}
		}

		public abstract void handleGETRequest(HttpProcessor p);
		public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
		public abstract void handlePOSTDataWithoutContentLength(string line);

		
		public void Dispose()
		{
			is_active = false;
			listener.Stop();
			listener = null;
			
		}
	}
}
