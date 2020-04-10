using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController SingletonInstance { get; private set; }

    [SerializeField]
    public int health, maxHealth;

    private const float InvincibleLength = 1;

    private SpriteRenderer _playerSpriteRenderer;
    private float _invincibleCount;

    private void Awake()
    {
        SingletonInstance = this;
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_invincibleCount > 0)
        {
            _invincibleCount -= Time.deltaTime;

            if (_invincibleCount <= 0)
            {
                var currentColor = _playerSpriteRenderer.color;

                _playerSpriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (_invincibleCount <= 0)
        {
            health -= damage;
            UiController.SingletonInstance.UpdateHealthUi();
        }

        if (health <= 0)
        {
            Die();
        }
        else if (_invincibleCount <= 0)
        {
            var currentColor = _playerSpriteRenderer.color;

            _invincibleCount = InvincibleLength;
            _playerSpriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, .6f);
        }
    }

    public int GetHealthValue()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
