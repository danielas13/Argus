﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    //Place the spawner about 1f above the ground on y axis in order to have the knights on level with the ground.

    //public int TotalSpawns = 2;     //Max Spawns of this spawner.
    public Transform EnemyPrefab;   //The enemy prefab.
    public float coolDown = 10;       //cooldown between spawns

    public float MaxRangeFromPlayer = 10f;  //Distance between the player in order to spawn new objects.
    public LayerMask spawnLayers;           //the layer filtering out the player.

    private float currentCount = 0;   //The current counter for spawning enemies.
    private Object lis;               //The enemy object.
    GameObject player;                //The Player object.
	public bool SingleSpawn = false;
	private bool canSpawn = true;
    private bool spawning = false;
    private bool hasSpawned = false;

    // Use this for initialization
    void Start () {
        //Fetching the player object.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            spawning = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawning = false;
            hasSpawned = false;
        }
    }


    // Update is called once per frame
    void Update () {
        if (spawning)
        {
            if (canSpawn)
            {
                if (!hasSpawned)    //making sure that the enemy can´t respawn unless the player has exited the area.
                {

                    //fidning the player object in case the player has respawned.
                    if (player == null)
                    {
                        player = GameObject.FindGameObjectWithTag("Player");
                    }
                    //Creating the ray towards player to check if he is in sight.
                    Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
                    Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
                    RaycastHit2D rayToPlayer = Physics2D.Raycast(enemyPos, playerPos - enemyPos, MaxRangeFromPlayer, spawnLayers);

                    //Check if the player is in range.
                    if (rayToPlayer.collider == null)
                    {
                        if (lis == null)
                        {
                            //Reset the timer.
                            this.currentCount = this.coolDown;
                            //instantiate the new object ( create a new clone of the enemy prefab).
                            lis = Instantiate(EnemyPrefab, transform.position, transform.rotation);
                            if (SingleSpawn)
                            {
                                canSpawn = false;
                            }
                        }
                    }
                    hasSpawned = true;
                }
            }
        }
        //Updating the timer if it's on cooldown.
        if (this.currentCount > 0)
        {
            currentCount -= Time.deltaTime;
        }
    }
}
