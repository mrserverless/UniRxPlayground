using System;
using Assets.Scripts.Observers;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;

        private CharacterController _character;
        private Camera _camera;

        [Inject]
        Inputs _inputs;

//        public PlayerController(
////            CharacterController character, Camera camera,
//            Inputs inputs)
//        {
////            _character = character;
////            _camera = camera;
//            _inputs = inputs;
//        }

        private void Awake()
        {
            _character = GetComponent<CharacterController>();
            _camera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            _inputs.Movement
                .Where(v => v != Vector2.zero)
                .Subscribe(inputMovement =>
                {
                    var inputVelocity = inputMovement * walkSpeed;

                    var playerVelocity =
                        inputVelocity.x * transform.right +
                        inputVelocity.y * transform.forward;

                    var distance = playerVelocity * Time.fixedDeltaTime;
                    _character.Move(distance);
                }).AddTo(this);
        }
    }
}