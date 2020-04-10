using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController SingletonInstance { get; private set; }

    [SerializeField]
    private Sprite fullHeart, emptyHeart, halfHeart;
    [SerializeField]
    private Image[] heartUi;

    private void Awake()
    {
        SingletonInstance = this;
    }

    private void Start()
    {
        UpdateHealthUi();
    }

    public void UpdateHealthUi()
    {
        var playerHealth = PlayerHealthController.SingletonInstance.GetHealthValue();
        var playerMaxHealth = PlayerHealthController.SingletonInstance.GetMaxHealth();
        var heartsToRender = (int) Math.Ceiling((float) playerMaxHealth / 2);
        var heartsToFill = playerHealth / 2;

        for (var i = 0; i < heartUi.Length; i++)
        {
            heartUi[i].enabled = i < heartsToRender;

            if (i < heartsToFill)
            {
                heartUi[i].sprite = fullHeart;
            }
            else if (i == heartsToFill && playerHealth % 2 != 0)
            {
                heartUi[i].sprite = halfHeart;
            }
            else
            {
                heartUi[i].sprite = emptyHeart;
            }
        }
    }
}
