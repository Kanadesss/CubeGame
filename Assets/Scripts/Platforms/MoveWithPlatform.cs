using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

  private void OnCollisionEnter(Collision collision) {
    collision.transform.parent = transform;
  }

  private void OnCollisionExit(Collision collision) {
    collision.transform.parent = null;
  }

}
