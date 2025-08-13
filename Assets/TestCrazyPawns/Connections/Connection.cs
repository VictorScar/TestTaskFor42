using System.Collections.Generic;
using UnityEngine;

namespace TestCrazyPawns.Connections
{
    public class Connection : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        private List<PawnConnector> _connectors = new List<PawnConnector>();
        private int _connectorsCount = 2;

        public void UpdateInternal()
        {
            if (_connectors != null && _connectors.Count > 1)
            {
                for (int i = 0; i < _connectors.Count; i++)
                {
                    lineRenderer.SetPosition(i, _connectors[i].Position);
                }
            }
        }

        public void SetData(PawnConnector fromConnector, PawnConnector toConnector)
        {
            _connectors.Clear();
            _connectors.Add(fromConnector);
            _connectors.Add(toConnector);
        }

        public bool IsContainConnector(PawnConnector[] connectors)
        {
            foreach (var connector in connectors)
            {
                if (IsContainConnector(connector))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsContainConnector(PawnConnector findingConnector)
        {
            foreach (var connector in _connectors)
            {
                if (findingConnector == connector)
                {
                    return true;
                }
            }

            return false;
        }
    }
}