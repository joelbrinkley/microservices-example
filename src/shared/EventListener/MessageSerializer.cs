﻿using Domain.DomainEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventListener
{
    public class MessageSerializer
    {
        public static byte[] Serialize(object message)
        {
            var json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T Deserializer<T>(byte[] messageData)
        {
            var json = Encoding.UTF8.GetString(messageData);

            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

    }
}
