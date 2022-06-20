using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;
    [SerializeField] private float _speed = 1f;
    private int _currentWaypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].transform.position) < .1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].transform.position, _speed * Time.deltaTime);
    }
}
