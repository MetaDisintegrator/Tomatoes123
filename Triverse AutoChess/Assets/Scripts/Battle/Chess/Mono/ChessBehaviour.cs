using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Battle.Chess
{
    public class ChessBehaviour : MonoBehaviour
    {
        public event Action UpdateCallback;
        public event Action DestroyCallback;

        // Update is called once per frame
        void Update()
        {
            UpdateCallback?.Invoke();
        }

        private void OnDestroy()
        {
            DestroyCallback?.Invoke();
        }
    }
}

