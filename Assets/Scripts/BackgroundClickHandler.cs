using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClickHandler : MonoBehaviour
{
    public void OnMouseDown()
    {
        // Block input if the cat is performing an action
        if (!CatController.Instance.IsCatPoppingBubbles())
        {
            CatController.Instance.PerformPawAction();
            //  log shows there is a mousedown
            Debug.Log("!!!!@@@@!!!!Background clicked!");
        }
    }
}
