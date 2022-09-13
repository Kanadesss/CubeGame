using UnityEngine;

public class CameraAlignment : MonoBehaviour {

  void OnEnable() {
    CubeAlignment.OnAligned += getQuaternion;
  }

  void OnDisable() {
    CubeAlignment.OnAligned -= getQuaternion;
  }

  public Quaternion getQuaternion() {
    return transform.rotation;
  }

}
