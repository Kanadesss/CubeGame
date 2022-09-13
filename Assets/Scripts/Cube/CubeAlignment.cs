using UnityEngine;
using System;

public class CubeAlignment : MonoBehaviour {
  public static event Func<Quaternion> OnAligned;

  [SerializeField] private LayerMask _whatHasCollider;

  void Update() {

    if (Input.GetKeyDown(KeyCode.X)) {
      Alignment();
    }

  }

  private void Alignment() {
    AlignmentPosition();
    AlignmentRotation();
  }

  private void AlignmentPosition() {
    transform.position = SetAlignmentCoordinates(transform.position, transform.localScale.x);
  }

  private Vector3 SetAlignmentCoordinates(Vector3 position, float size) {
    bool xReverseSearch = false;
    bool zReverseSearch = false;
    int positionsCount = 4;
    Vector3 newPosition = position;

    if (!CheckCoordinates(position, size)) {
      positionsCount = 0;
    }

    do {
      newPosition.x = SetAlignmentCoordinate(size, position.x, xReverseSearch);
      newPosition.z = SetAlignmentCoordinate(size, position.z, zReverseSearch);
      xReverseSearch = positionsCount % 2 == 0;    // false, true, false, true
      zReverseSearch = positionsCount % 5 <= 3;    // false, false, true, true
      --positionsCount;
    } while (!CheckCoordinates(newPosition, size) && positionsCount > 0);

    if (!CheckCoordinates(newPosition, size) && positionsCount == 0) {
      Debug.LogError("Error alignment, no free space");
      return position;
    }

    return newPosition;
  }

  private int SetAlignmentCoordinate(float scale, float position, bool reverseSearch) {
    float positionAbs = Mathf.Abs(position);
    float shift = positionAbs % scale;

    if ((shift >= scale / 2 || reverseSearch) && (!(shift >= scale / 2) || !reverseSearch)){
      positionAbs += scale - shift;
    } else {
      positionAbs -= shift;
    }

    return (int)(positionAbs * Mathf.Sign(position));
  }
  
  private bool CheckCoordinates(Vector3 position, float diameter) {
    Collider[] colliders = Physics.OverlapSphere(position, diameter / 2 - diameter / 20 , _whatHasCollider);

    if (colliders.Length == 0) {
      return true;
    } else {
      return false;
    }

  }

  private void AlignmentRotation() {

    if (OnAligned != null) {
      transform.rotation = Quaternion.Euler(PositionAlongAxis(OnAligned.Invoke()));
    } else {
      Debug.LogError("onAligned event has no listeners");
    }
    
  }

  private Vector3 PositionAlongAxis(Quaternion quaternion) {
    Vector3 vectorRotation = Vector3.zero;
    Vector3 eulerAngles = quaternion.eulerAngles;
    
    if (eulerAngles.y >= 315 || eulerAngles.y < 45) {
      vectorRotation.y = 0;
    } else if (eulerAngles.y < 135) {
      vectorRotation.y = 90;
    } else if (eulerAngles.y < 225) {
      vectorRotation.y = 180;
    } else {
      vectorRotation.y = -90;
    }

    return vectorRotation;
  }

}