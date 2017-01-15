using System;
using UnityEngine;

namespace Assets.Scripts.Observables
{
    public interface IPlayerActions
    {
        IObservable<Vector3> Walked { get; }
        float StrideLength { get; }
    }

}