using System.Collections;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Vector3 checkpointWorldPos;
    public int level;
    public bool waterLevel = false;
    public bool satanLevel = false;
    public GameObject[] enemyList;

    public void activate() {
        foreach (GameObject enemy in enemyList) {
            if (enemy) { 
                enemy.GetComponent<EnemyBase>().active = true;
            }
        }
    }
}
