using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Observables
{
    public class Inputs : MonoBehaviour
    {
        public IObservable<Vector2> Movement { get; private set; }
        public IObservable<Vector2> Mouselook { get; private set; }
        public ReadOnlyReactiveProperty<bool> Run { get; private set; }

        private bool _runValue;

        private void Awake()
        {
            // Hide the mouse cursor and lock it in the game window.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Movement = MovementObservable();
            Mouselook = MouseObservable();
            Run = RunFlag();
        }

        private IObservable<Vector2> MovementObservable()
        {
            return this.FixedUpdateAsObservable()
                .Select(_ =>
                {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                });
        }

        private IObservable<Vector2> MouseObservable()
        {
            // Mouse look ticks on Update
            return this.UpdateAsObservable()
                .Select(_ =>
                {
                    var x = Input.GetAxis("Mouse X");
                    var y = Input.GetAxis("Mouse Y");
                    return new Vector2(x, y);
                });
        }

        private ReadOnlyReactiveProperty<bool> RunFlag()
        {
            return this.UpdateAsObservable()
                .Where(_ => Input.GetButtonDown("Fire 3"))
                .Select(_ => _runValue = !_runValue)
                .ToReadOnlyReactiveProperty();
        }
    }
}