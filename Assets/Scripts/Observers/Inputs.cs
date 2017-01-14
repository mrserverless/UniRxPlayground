using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Scripts.Observers
{
    public class Inputs : MonoBehaviour {

        public IObservable<Vector2> Movement { get; private set; }
        public IObservable<Vector2> Mouselook { get; private set; }
        public ReadOnlyReactiveProperty<bool> Run { get; private set; }

        private void Awake()
        {
            // Hide the mouse cursor and lock it in the game window.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Movement = this.FixedUpdateAsObservable()
                .Select(_ => {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                });

            // Mouse look ticks on Update
            Mouselook = this.UpdateAsObservable()
                .Select(_ => {
                    var x = Input.GetAxis("Mouse X");
                    var y = Input.GetAxis("Mouse Y");
                    return new Vector2(x, y);
                });

            Run = this.UpdateAsObservable()
                .Select(_ => Input.GetButton("Fire3"))
                .ToReadOnlyReactiveProperty();
        }
    }
}