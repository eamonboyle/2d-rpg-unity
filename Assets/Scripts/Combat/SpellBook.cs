using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Spell castingSpell;
    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;

    public Spell CastSpell(int index)
    {
        castingSpell = spells[index];

        castingBar.fillAmount = 0;
        castingBar.color = castingSpell.BarColor;
        spellName.text = castingSpell.Name;
        icon.sprite = castingSpell.Icon;

        spellRoutine = StartCoroutine(Progress());
        fadeRoutine = StartCoroutine(FadeBar());

        return castingSpell;
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
            canvasGroup.alpha = 0;
        }

        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    private IEnumerator Progress()
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / castingSpell.CastTime;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (castingSpell.CastTime - timePassed).ToString("F2");

            if (castingSpell.CastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }

        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.50f;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;

            yield return null;
        }
    }
}
