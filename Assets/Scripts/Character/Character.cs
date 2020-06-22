using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    protected Vector2 direction;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            // go back to the idle layer
            ActivateLayer("Idle");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
}
