using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public delegate void NPCRemoved();

public class NPC : Character
{
    public event HealthChanged healthChanged;
    public event NPCRemoved npcRemoved;

    public Sprite Portrait
    {
        get
        {
            return portrait;
        }
    }

    [SerializeField]
    private Sprite portrait;

    public virtual void Deselect()
    {
        healthChanged -= new HealthChanged(UIManager.Instance.UpdateTargetFrame);
        npcRemoved -= new NPCRemoved(UIManager.Instance.HideTargetFrame);
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnNPCRemoved()
    {
        if (npcRemoved != null)
        {
            npcRemoved();
        }

        Destroy(gameObject);
    }
}
