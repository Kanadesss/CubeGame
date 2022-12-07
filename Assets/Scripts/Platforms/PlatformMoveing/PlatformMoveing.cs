using System.Collections;
using UnityEngine;

public class PlatformMoveing : MonoBehaviour{
  [SerializeField] protected float[] _moveSpeeds;
  [SerializeField] protected Transform[] _wayPoints;
  [SerializeField] protected float[] _delayTimes;
  protected int _currentWayPoint;
  protected int _stepToNextPoint;
  protected int _currentRoute;
  protected float _currentSpeed;
  protected float _distanceToPlatformEdge;
  protected float[] _routeLength;

  void Awake() {
    _currentWayPoint = 0;
    _stepToNextPoint = 1;
    _currentRoute = 0;
    SetSpeed(_moveSpeeds[_currentWayPoint]);
    _routeLength = new float[_wayPoints.Length - 1];
    CalculateRouteLengths();
    Transform[] transforms = GetComponentsInChildren<Transform>();
    _distanceToPlatformEdge = transforms[1].localScale.z / 2;
}

  void Start() {
    StartCoroutine(Move());
  }

  protected IEnumerator Move() {
    
    while (true) {
      SetSpeed(_moveSpeeds[_currentWayPoint]);

      while (Vector3.Distance(transform.position, _wayPoints[_currentWayPoint].position) >= _distanceToPlatformEdge) {
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_currentWayPoint].position, _currentSpeed * Time.deltaTime);
        yield return null;
      }

      yield return new WaitForSecondsRealtime(_delayTimes[_currentWayPoint]);
      ChangeWayPoint();
    }

  }

  protected void ChangeWayPoint() {
    int previouseWayPoint = _currentWayPoint;
    _currentWayPoint += _stepToNextPoint;
    _currentRoute = Mathf.Min(previouseWayPoint, _currentWayPoint);

    if (_currentWayPoint % (_wayPoints.Length - 1) == 0) {
      _stepToNextPoint *= -1;
    }

  }

  protected void SpeedIncrease() {
    //to do
  }

  protected void SpeedBraking() {
    //to do
  }

  protected void SetSpeed(float newSpeed) {
    _currentSpeed = newSpeed;
  }

  protected void CalculateRouteLengths() {
    
    for(int i = 0; i < _wayPoints.Length - 1; ++i) {
      _routeLength[i] = Vector3.Distance(_wayPoints[i].position, _wayPoints[i + 1].position);
    }

  }

}