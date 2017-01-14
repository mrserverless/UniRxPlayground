﻿using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Scripts.Observers
{
    public class Inputs : MonoBehaviour {

        public IObservable<Vector2> Movement { get; private set; }

        // use Initialize according to Unity's Awake phase
        private void Awake()
        {
            Movement = this.FixedUpdateAsObservable()
                .Select(_ => {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                });
        }
    }
}