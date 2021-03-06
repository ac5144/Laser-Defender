﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    Formation waveConfig;
    List<Transform> waypoints;

    int waypointIndex;

    Movement movement;

    private void Update() {

        Move();
    }

    private void Setup() {

        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        waypointIndex = 0;

        movement = gameObject.GetComponent<Movement>();
        movement.SetSpeed(waveConfig.GetMoveSpeed());
    }

    public void SetWaveConfig(Formation waveConfig) {

        this.waveConfig = waveConfig;
        Setup();
    }

    private void Move() {

        if (waypointIndex <= waypoints.Count - 1) {

            var targetPosition = waypoints[waypointIndex].transform.position;
            movement.Move(targetPosition);

            if (transform.position == targetPosition) {

                waypointIndex++;
            }
        }
        else {

            Destroy(gameObject);
        }
    }
}
