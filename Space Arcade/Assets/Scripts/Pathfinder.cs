using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    private WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    private void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].position;
    }
    private void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        FollowPath();

    }

    private void FollowPath() {
        if(waypointIndex < waypoints.Count) {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float deltaPosition = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaPosition);
            if(transform.position.Equals(targetPosition)) {
                waypointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
