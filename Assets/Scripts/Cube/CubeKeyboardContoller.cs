using UnityEngine;

public class CubeKeyboardContoller : MonoBehaviour {
  [SerializeField] private CubeMovement _cubeMovement;
  [SerializeField] private Camera _cameraCurrent;

  private void FixedUpdate() {

    if (Input.GetKey(KeyCode.W)) {
      Vector3 rotatiotnAxis = _cameraCurrent.transform.right;
      rotatiotnAxis.y = 0;
      _cubeMovement.Move(rotatiotnAxis);
    } else if (Input.GetKey(KeyCode.S)) {
      Vector3 rotatiotnAxis = _cameraCurrent.transform.right * -1;
      rotatiotnAxis.y = 0;
      _cubeMovement.Move(rotatiotnAxis);
    }

    if (Input.GetKey(KeyCode.A)) {
      Vector3 rotatiotnAxis = FormRotatiotnAxis(_cameraCurrent.transform.forward, _cameraCurrent.transform.up);
      _cubeMovement.Move(rotatiotnAxis);
    } else if (Input.GetKey(KeyCode.D)) {
      Vector3 rotatiotnAxis = FormRotatiotnAxis(_cameraCurrent.transform.forward * -1, _cameraCurrent.transform.up * -1);
      _cubeMovement.Move(rotatiotnAxis);
    }

  }

  private Vector3 FormRotatiotnAxis(Vector3 mainAxis, Vector3 auxiliaryAxis) {
    mainAxis.y = 0;

    if (Mathf.Abs(mainAxis.x) < 0.25 && Mathf.Abs(mainAxis.z) < 0.25) {
      mainAxis.x = (mainAxis.x + auxiliaryAxis.x) / 2;
      mainAxis.z = (mainAxis.z + auxiliaryAxis.z) / 2;
    }

    return mainAxis;
  }
}
