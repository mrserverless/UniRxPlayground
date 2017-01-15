using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UniRx.Zenject
{
    public static partial class DisposableExtensions
    {
        /// <summary>Dispose self on target gameObject has been destroyed. Return value is self disposable.</summary>
        public static T AddTo<T>(this T disposable)
            where T : IDisposable
        {
            disposable.Dispose();
            return disposable;
        }
    }
}