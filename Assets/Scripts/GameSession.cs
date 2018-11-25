using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerScore = 0;

    private void Awake() {

        SetUpSingleton();
    }

    private void SetUpSingleton() {

        if (FindObjectsOfType(GetType()).Length > 1) {

            Destroy(gameObject);
        }
        else {

            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetPlayerScore() {

        return playerScore;
    }

    public void AddToScore(int points) {

        playerScore += points;
    }

    public void ResetGame() {

        Destroy(gameObject);
    }
}
