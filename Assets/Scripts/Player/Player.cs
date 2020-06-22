using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Transform MyTarget { get; set; }

    [SerializeField]
    private Stat mana;

    private SpellBook spellBook;

    private float initMana = 50;

    [SerializeField]
    private Transform[] exitPoints;

    private Exits exitIndex = Exits.DOWN;

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
        mana.Initialize(initMana, initMana);

        spellBook = GetComponent<SpellBook>();

        base.Start();
    }

    protected override void Update()
    {
        GetInput();

        base.Update();
    }

    public void CastSpell(int spellIndex)
    {
        if (MyTarget != null && !attacking && !Moving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
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
        }
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);

        attacking = true;

        myAnimator.SetBool("attack", attacking);

        yield return new WaitForSeconds(newSpell.CastTime); // hard coded cast time for debugging

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.SpellPrefab, exitPoints[(int)exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.Damage);
        }

        StopAttack();
    }

    private bool InLineOfSight()
    {
        if (MyTarget == null)
        {
            return false;
        }

        Vector2 directionToTarget = (MyTarget.position - transform.position).normalized;

        Vector2 facing = directions[(int)exitIndex]; // direction of facing

        float angleToTarget = Vector2.Angle(facing, directionToTarget);

        if (angleToTarget < fieldOfView / 2f)
        {
            return true;
        }

        return false;
    }
}
