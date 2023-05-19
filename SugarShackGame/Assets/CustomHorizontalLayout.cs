using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHorizontalLayout : MonoBehaviour
{
    private int childCount;
    private bool allSet = false;
    private Transform child1;
    private Transform child2;


    // Update is called once per frame
    void Update()
    {
        childCount = transform.childCount;
        if (childCount == 0)
            return;

        if (!allSet)
        {
            Debug.Log(childCount);
            if (childCount == 1)
            {
                child1 = transform.GetChild(0);
                RectTransform rect = child1.transform.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(.5f, 1);
            }
            else if (childCount == 2)
            {
                child2 = transform.GetChild(1);
                RectTransform rect = child2.transform.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(.5f, 0);
                rect.anchorMax = new Vector2(1,1);
                allSet = true;
            }
        }
    }
}
