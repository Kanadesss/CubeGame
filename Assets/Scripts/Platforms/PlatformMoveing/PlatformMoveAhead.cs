using System.Collections;
using UnityEngine;


public class PlatformMoveAhead : PlatformMoveing {
  [SerializeField] private float _respawnTime;
  [SerializeField] private GameObject _platform;
  [SerializeField] private Transform _spawnPoint;

  void Awake() {
    _distanceToPlatformEdge = 1;
    _currentWayPoint = 0;
    SetSpeed(_moveSpeeds[_currentWayPoint]);
  }

  void Start() {
    Debug.Log(_distanceToPlatformEdge);
    StartCoroutine(platformLive());
  }

  private IEnumerator platformLive() {

    while (true) {
      StartCoroutine(Move());
      yield return new WaitWhile(() => _platform.activeSelf == true);
      yield return new WaitForSecondsRealtime(_respawnTime);
      RespawnPlatform();
    }

  }

  private new IEnumerator Move() {
    _currentWayPoint = 0;

    while (_platform.activeSelf == true) {
      SetSpeed(_moveSpeeds[_currentWayPoint]);

      while (Vector3.Distance(_platform.transform.position, _wayPoints[_currentWayPoint].position) >= _distanceToPlatformEdge) {
        _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, _wayPoints[_currentWayPoint].position, _currentSpeed * Time.deltaTime);
        yield return null;
      }

      yield return new WaitForSecondsRealtime(_delayTimes[_currentWayPoint]);
      ChangeActive();
      ChangeWayPoint();
    }

  }

  public void ChangeActive() {

    if (_currentWayPoint == _wayPoints.Length - 1) {
      _platform.SetActive(false);
    } else {
      _platform.SetActive(true);
    }

  }

  private void RespawnPlatform() { // to do
    Debug.Log("respawn");
    ChangeActive();
    _platform.transform.position = _spawnPoint.position;
    
  }

}