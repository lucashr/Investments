using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Investments.Application.Utils
{
    public static class LogUtils
    {
        public static async Task LogActions(object message)
        {

            message = $"{JsonConvert.SerializeObject(message)}";

            Console.WriteLine(message);
            Debug.WriteLine(message);
            
            await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync($"{message}");

        }
    }
}