using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.Messenger
{
    public class ChatConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<String>> _chatConnection = new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _chatConnection.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock(_chatConnection)
            {
                HashSet<string> connections;
                if(!_chatConnection.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _chatConnection.Add(key, connections);
                }
                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
            
        }

        public HashSet<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_chatConnection.TryGetValue(key, out connections))
            {
                return connections;
            }

            return null;
        }

        public void Remove(T key, string connectionId)
        {
            lock (_chatConnection)
            {
                HashSet<string> connections;
                if (!_chatConnection.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _chatConnection.Remove(key);
                    }
                }
            }
        }


    }
}