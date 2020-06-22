using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private NPC currentTarget;

    private void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // throw the raycast at the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null)
            {
                // if already have a target, deselect it
                if (currentTarget != null)
                {
                    currentTarget.Deselect();
                }

                // target the new NPC and select it's hit box
                currentTarget = hit.collider.GetComponent<NPC>();
                player.MyTarget = currentTarget.Select();

                // show the target frame
                UIManager.Instance.ShowTargetFrame(currentTarget);
            }
            else
            {
                // hide the target frame
                UIManager.Instance.HideTargetFrame();

                // clicking on empty space, clear targets
                if (currentTarget != null)
                {
                    currentTarget.Deselect();
                }

                currentTarget = null;
                player.MyTarget = null;
            }
        }
    }
}
