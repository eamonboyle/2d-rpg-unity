using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // just for debugging
        target = GameObject.Find("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // calculate position to target and move there
        Vector2 direction = target.position - transform.position;
        myRigidbody.velocity = direction.normalized * speed;

        // calculate the angle of the spell
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
