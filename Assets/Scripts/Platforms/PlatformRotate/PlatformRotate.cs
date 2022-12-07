using UnityEngine;

public class PlatformRotate : MonoBehaviour {    //use K,L to rotate
  [SerializeField] private float _rotateSpeed;
  [SerializeField] private bool _inverseRotation;

  private void FixedUpdate() {

    if (Input.GetKey(KeyCode.K)) {
      Rotate(_inverseRotation);
    } else if (Input.GetKey(KeyCode.L)) {
      Rotate(!_inverseRotation);
    }

  }

  private void Rotate(bool isInverse) {
    int direction = 1;

    if (isInverse) direction = -1;

    Vector3 axisRotation = transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(axisRotation.x + _rotateSpeed * direction * Time.fixedDeltaTime, axisRotation.y, axisRotation.z);

  }

}
