﻿using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public float damage = 1;    //damage of a fireball spell.
    public LayerMask NotHit;
    private float spellDistance = 5;
    Transform spellPoint;

    public Transform FirePrefab;

    void Awake(){
        spellPoint = transform.FindChild("SpellCast");
        if(spellPoint == null)
        {
            Debug.LogError("No FirePoint?");
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastFireBall();
        }
	}

    void CastFireBall()
    {
        Vector2 SpellPosition = new Vector2(spellPoint.position.x, spellPoint.position.y);

        GameObject character = GameObject.FindGameObjectWithTag("Player");
        Vector2  playerPos = character.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(SpellPosition, new Vector2(SpellPosition.x - playerPos.x, SpellPosition.y - playerPos.y), spellDistance, NotHit);
        if (SpellPosition.x > playerPos.x)
        {
            //Player facing to the right.
            Instantiate(FirePrefab, spellPoint.position, spellPoint.rotation);
           /* if (hit.collider != null)
            {
                Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x + spellDistance, playerPos.y), Color.red);
            }
            else
            {
                Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x + spellDistance, playerPos.y), Color.black);
            }*/
        }
        else
        {
            //player facing to the left.
            Instantiate(FirePrefab, spellPoint.position, spellPoint.rotation * Quaternion.Euler(Vector3.up * 180));
            /*if (hit.collider != null)
            {
                Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x - spellDistance, playerPos.y), Color.red);
            }
            else
            {
                Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x - spellDistance, playerPos.y), Color.black);
            }*/
        }
    }
}