using System;
using Assets.Scripts.Observables;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IPlayerActions
    {
        [Inject]
        private readonly Config _config;

        [Inject]
        private CharacterController _character;

        [Inject]
        private Camera _camera;

        [Inject]
        private Inputs _inputs;

        private Subject<Vector3> walked; // We get to see this as a Subject
        public IObservable<Vector3> Walked
        {
            // Everyone else sees it as an IObservable
            get { return walked; }
        }

        public float StrideLength
        {
            get { return _config.StrideLength; }
        }

        private void Awake()
        {
            walked = new Subject<Vector3>().AddTo(this);
        }

        private void Start()
        {
            _inputs.Movement
                .Where(v => v != Vector2.zero)
                .Subscribe(inputMovement =>
                {
                    var inputVelocity = inputMovement * (_inputs.Run.Value ? _config.RunSpeed : _config.WalkSpeed);

                    var playerVelocity =
                        inputVelocity.x * transform.right +
                        inputVelocity.y * transform.forward;

                    var distance = playerVelocity * Time.fixedDeltaTime;
                    _character.Move(distance);

                    // signal walk has happened
                    walked.OnNext(_character.velocity * Time.fixedDeltaTime);
                })
                .AddTo(this);

            // Handle mouse input (free mouse look).
            _inputs.Mouselook
                .Where(v => v != Vector2.zero) // We can ignore this if mouse look is zero.
                .Subscribe(inputLook =>
                {
                    // Translate 2D mouse input into euler angle rotations.

                    // inputLook.x rotates the character around the vertical axis (with + being right)
                    var horzLook = inputLook.x * Time.deltaTime * Vector3.up * _config.MouseSensitivity;
                    transform.localRotation *= Quaternion.Euler(horzLook);

                    // inputLook.y rotates the camera around the horizontal axis (with + being up)
                    var vertLook = inputLook.y * Time.deltaTime * Vector3.left * _config.MouseSensitivity;
                    var newQ = _camera.transform.localRotation * Quaternion.Euler(vertLook);

                    // We have to flip the signs and positions of min/max view angle here because the math
                    // uses the contradictory interpretation of our angles (+/- is down/up).
                    _camera.transform.localRotation =
                        ClampRotationAroundXAxis(newQ, -_config.MaxViewAngle, -_config.MinViewAngle);
                })
                .AddTo(this);
        }

        // Ripped straight out of the Standard Assets MouseLook script. (This should really be a standard function...)
        private static Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        [Serializable]
        public class Config
        {
            [Range(0, 5)] public float WalkSpeed = 5;

            [Range(-90, 0)] public float MinViewAngle = -60f; // How much can the user look down (in degrees)

            [Range(0, 90)] public float MaxViewAngle = 60f; // How much can the user look up (in degrees)

            [Range(0, 200)] public float MouseSensitivity = 100f;

            [Range(10, 20)] public float RunSpeed = 10;

            [Range(0, 5)] public float StrideLength = 2.5f;
        }
    }
}