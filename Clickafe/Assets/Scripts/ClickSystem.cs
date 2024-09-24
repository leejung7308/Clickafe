using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSystem : MonoBehaviour, IPointerClickHandler
{
    GameObject player;
    GameObject UI;
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnPointerClick(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            player.GetComponent<Status>().Sell();
        }
    }
}
