using System.Collections.Generic;
using TestCrazyPawns.Pawn;
using UnityEngine;

namespace TestCrazyPawns.Connections
{
    public class ConnectionsController : MonoBehaviour
    {
        [SerializeField] private Connection connectionPrefab;
        private List<Connection> _connections = new List<Connection>();
    
        private void Update()
        {
            if (_connections != null)
            {
                foreach (var connection in _connections)
                {
                    connection.UpdateInternal();
                }
            }
        }

        public void AddConnection(PawnConnector fromConnector, PawnConnector toConnector)
        {
            var connection = Instantiate(connectionPrefab, transform);
            connection.SetData(fromConnector, toConnector);
            _connections.Add(connection);
        }

        public void RemoveConnectionsByPawn(PawnConnector[] connectors)
        {
            for (int i = _connections.Count - 1; i >= 0; i--)
            {
                var connection = _connections[i];
            
                if (connection.IsContainConnector(connectors))
                {
                    _connections.Remove(connection);
                    Destroy(connection.gameObject);
                }
            }
        }
    }
}
