using System.Collections;
using System.Collections.Generic;
using Shooting.Aspects;
using Shooting.Components;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    private EntityManager _entityManager;


    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider slider;

    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {
        HealthComponent healthPlayerComponent;
        var isHealthComponent = _entityManager.CreateEntityQuery(typeof(PlayerTag), typeof(HealthComponent))
            .TryGetSingleton(out healthPlayerComponent);
        if (!isHealthComponent)
        {
            showHealth(0, 0);
            return;
        }

        showHealth(healthPlayerComponent.Health, healthPlayerComponent.HealthMax);
    }


    private void showHealth(int health, int maxHeath)
    {
        healthText.text = $"{health}/{maxHeath}";
        if (maxHeath == 0)
        {
            return;
        }

        slider.value = health / maxHeath;
    }
}