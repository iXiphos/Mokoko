using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStatsUI : MonoBehaviour
{
    private bool selectState;
    private Vector2 defaultPos;
    public RectTransform selectPos;
    void Start()
    {
        defaultPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    public bool SelectState
    {
        get
        {
            return selectState;
        }
        set
        {
            selectState = value;
            if(value == true)
            {
                gameObject.GetComponent<Animator>().Play("CharacterUI_In");
            }
            else
            {
                gameObject.GetComponent<Animator>().Play("CharacterUI_Out");
            }
        }
    }

    void Update()
    {
        if(selectState)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition, selectPos.anchoredPosition, 0.1f);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition, defaultPos, 0.1f);
        }
    }

    public void UpdateHealth(float percent)
    {
        RectTransform rect = gameObject.transform.Find("Health").Find("Circle").Find("HealthFill").GetComponent<RectTransform>();
        percent = 1 - percent;
        float newY = rect.GetComponent<RectTransform>().sizeDelta.y * -percent;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
    }

    public void UpdateHunger(float percent)
    {
        RectTransform rect = gameObject.transform.Find("Hunger").Find("Circle").Find("HealthFill").GetComponent<RectTransform>();
        percent = 1 - percent;
        float newY = rect.GetComponent<RectTransform>().sizeDelta.y * -percent;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
    }

    public void UpdateSanity(float percent)
    {
        RectTransform rect = gameObject.transform.Find("Sanity").Find("Circle").Find("HealthFill").GetComponent<RectTransform>();
        percent = 1 - percent;
        float newY = rect.GetComponent<RectTransform>().sizeDelta.y * -percent;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);
    }
}
