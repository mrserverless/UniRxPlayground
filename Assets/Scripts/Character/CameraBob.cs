using System;
using Assets.Scripts.Observables;
using Observables;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class CameraBob : MonoBehaviour {

    [Inject]
    public IPlayerActions _playerActions;

    [Inject]
    private Inputs _inputs;

    [Inject]
    private Camera _view;

    [Inject]
    private Config _config;

    public AnimationCurve bob = new AnimationCurve(
        new Keyframe(0.00f,  0f),
        new Keyframe(0.25f,  1f),
        new Keyframe(0.50f,  0f),
        new Keyframe(0.75f, -1f),
        new Keyframe(1.00f,  0f));

    private Vector3 initialPosition;

    public void Awake()
    {
        initialPosition = _view.transform.localPosition;
    }

    private void Start() {
        var distance = 0f;
        _playerActions.Walked.Subscribe(w => {
            // Accumulate distance walked (modulo stride length).
            distance += w.magnitude;
            distance %= _playerActions.StrideLength;
            // Use distance to evaluate the bob curve.
            var magnitude = _inputs.Run.Value ? _config.RunBobMagnitude : _config.WalkBobMagnitude;
            var deltaPos = magnitude * bob.Evaluate(distance / _playerActions.StrideLength) * Vector3.up;
            // Adjust camera position.
            _view.transform.localPosition = initialPosition + deltaPos;
        }).AddTo(this);
    }

    [Serializable]
    public class Config
    {
        public float WalkBobMagnitude = 0.05f;
        public float RunBobMagnitude = 0.10f;
    }
}
