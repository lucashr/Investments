using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Enum;
using Newtonsoft.Json;

namespace Investments.Application.Utils
{
    public static class LogUtils
    {
        public static async Task LogActions(PageIdentification page,
                                            object data, 
                                            string sessionId = "", 
                                            bool toAllSessions = false)
        {

            var message = new 
                            { 
                                pageIdentification = page,
                                data = JsonConvert.SerializeObject(data)
                            };
            
            var jsonMessage = JsonConvert.SerializeObject(message);

            Console.WriteLine(jsonMessage);
            Debug.WriteLine(jsonMessage);

            if(string.IsNullOrEmpty(sessionId)) return;
            
            if(toAllSessions)
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageToAllAsync(sessionId, $"{jsonMessage}");
            else
                await VariablesManager.ConectionsWebSocket.socketManager.SendMessageAsync(sessionId, jsonMessage);
            
        }
    }
}