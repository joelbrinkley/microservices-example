using Newtonsoft.Json;
using System;
using System.Text;

namespace Messaging
{
    public class MessageSerializer
    {
        public static byte[] Serialize<TMessage>(TMessage message)
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
