using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    public bool Moving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    [SerializeField]
    private float speed;

    [SerializeField]
    private float initHealth;

    private Rigidbody2D myRigidbody;

    [SerializeField]
    protected Stat health;

    protected Animator myAnimator;
    protected Vector2 direction;
    protected bool attacking = false;
    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        health.Initialize(initHealth, initHealth);
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // move the player with physics
        // make sure the direction is normalized
        myRigidbody.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        // animate layers if moving
        if (Moving)
        {
            // change to the walk animation layer
            ActivateLayer("Walk");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if (attacking)
        {
            ActivateLayer("Attack");
        }
        else
        {
            // go back to the idle layer
            ActivateLayer("Idle");
        }
    }

    public void ActivateLayer(string layerName)
    {
        if (myAnimator == null)
        {
            return;
        }

        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attacking = false;
            myAnimator.SetBool("attack", attacking);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        // reduce health
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            // die
            print("DIE");

            myAnimator.SetTrigger("die");
        }
    }
}
