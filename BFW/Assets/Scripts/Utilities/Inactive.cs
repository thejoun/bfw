using System;
using UnityEngine;

namespace Utilities
{
    public class Inactive : IDisposable
    {
        private GameObject _go;
        
        public Inactive(GameObject go)
        {
            go.SetActive(false);
            
            _go = go;
        }

        public void Dispose()
        {
            _go.SetActive(true);
        }
    }
}