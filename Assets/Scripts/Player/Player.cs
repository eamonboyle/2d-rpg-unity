﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat mana;

    private float initHealth = 100;
    private float initMana = 50;

    [SerializeField]
    private GameObject[] spellPrefab;

    [SerializeField]
    private Transform[] exitPoints;

    private Exits exitIndex = Exits.DOWN;

    private Transform target;

    private Vector2[] directions = new Vector2[]
    {
        new Vector2(0f, 1f), // up
        new Vector2(1f, 0f), // left
        new Vector2(0f, -1f), // down
        new Vector2(-1f, 0f) // right
    };

    [SerializeField]
    private float fieldOfView = 180f;

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);

        // just for debugging
        target = GameObject.Find("Target").transform;

        base.Start();
    }

    protected override void Update()
    {
        GetInput();

        base.Update();
    }

    public void CastSpell()
    {
        Instantiate(spellPrefab[0], exitPoints[(int)exitIndex].position, Quaternion.identity);
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        // for debugging health & mana
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        // movement inputs
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = Exits.UP;
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = Exits.DOWN;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = Exits.LEFT;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = Exits.RIGHT;
            direction += Vector2.right;
        }

        // attack input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!attacking && !Moving && InLineOfSight())
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        attacking = true;

        myAnimator.SetBool("attack", attacking);

        yield return new WaitForSeconds(1); // hard coded cast time for debugging

        CastSpell();

        StopAttack();
    }

    private bool InLineOfSight()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        Vector2 facing = directions[(int)exitIndex]; // direction of facing

        float angleToTarget = Vector2.Angle(facing, directionToTarget);

        if (angleToTarget < fieldOfView / 2f)
        {
            return true;
        }

        return false;
    }
}
