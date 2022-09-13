using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour {
  private readonly float _maxAngularVelocity = 5.5f;   // 5.5 - магическое число, рассчитанное при 0.5 <= Scale <= 4

  private Rigidbody _rigidbody;
  private float _rotationSpeed;

  void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }

  void OnEnable() {
    SetMaxAngularVelocity();
  }

  public void Move(Vector3 rotatiotnAxis) {
    _rigidbody.AddTorque(rotatiotnAxis * _rotationSpeed, ForceMode.VelocityChange);
  }

  private void SetMaxAngularVelocity() {
    _rigidbody.maxAngularVelocity = Mathf.Abs(_maxAngularVelocity - transform.localScale.x);
    _rotationSpeed = _rigidbody.maxAngularVelocity;
  }

}
