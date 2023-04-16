using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Slot : MonoBehaviour
{
    public Image slotContainer;
    public Image pieContainer;

    private Sprite empty;
    private AbilityHolder currentAbility;
    private float duration;
    private float timeLeft;
    private float waitTime = 3f;

    private void Start()
    {
        LoadEmptySprite();
        LoadNewAbility();
        DisplayAbility(currentAbility.ability.sprite, currentAbility.ability.activeTime);
    }

    private void LoadEmptySprite()
    {
        empty = Resources.Load<Sprite>("Sprites/empty");
    }

    private void LoadNewAbility()
    {
        currentAbility = AbilityManager.Instance.GenerateAbility();
    }

    private void DisplayAbility(Sprite sprite, float duration)
    {
        slotContainer.sprite = sprite;
        this.duration = duration;
        this.timeLeft = duration;

        if (duration > 0)
            pieContainer.color = new Color(0, 0, 0, .5f);        
        else
            pieContainer.color = new Color(0.00f, 0.00f, 0.00f, 0.78f);
    }

    public void Refresh()
    {
        if(currentAbility.IsActive == true && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            pieContainer.fillAmount = Mathf.Lerp(1, 0, (timeLeft / duration));
        }
        if(timeLeft <= 0)
        {
            RemoveAbility();
        }
    }

    private void RemoveAbility()
    {
        pieContainer.fillAmount = 0;
        slotContainer.sprite = empty;
        StartCoroutine(GenerateNewAbility(waitTime));
    }

    IEnumerator GenerateNewAbility(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadNewAbility();
        DisplayAbility(currentAbility.ability.sprite, currentAbility.ability.activeTime);
    }

    public AbilityHolder GetCurrentAbility()
    {
        return this.currentAbility;
    }
}
