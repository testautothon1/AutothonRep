//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogCapturer.HttpWebHook
{
    public class AppiumLogPacket
    {
        public String method;
        public AppiumLogPacketParams @params;
        
        public static AppiumLogPacket fromJson(String jsonString)
        {
            return JsonConvert.DeserializeObject<AppiumLogPacket>(jsonString);
        }
    }

    public class AppiumLogPacketParams
    {
        public String message;
        public String level;
        public String timestamp;

    }
}
