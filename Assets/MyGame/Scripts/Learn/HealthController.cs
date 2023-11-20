using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    [Range(0,1)]
    private float maximumInjuredLayerWeight;

    private float maxHealth = 100;
    private float curHealth;
    private int injuredLayerIndex;
    private float layerWeightVelocity;

    void Start()
    {
        curHealth = maxHealth;
        animator = GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("Injured Layer");

        print($"Injured Layer Index: {injuredLayerIndex}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curHealth -= maxHealth / 10;

            if(curHealth < 0)
            {
                curHealth = maxHealth;
            }
        }

        float healthPercentage = curHealth / maxHealth;
        healthText.text = $"HP:{healthPercentage * 100}%";

        float currentInjuredLayerWeight = animator.GetLayerWeight(injuredLayerIndex);
        float targetInjuredLayerWeight = (1 - healthPercentage) * maximumInjuredLayerWeight;
        animator.SetLayerWeight(injuredLayerIndex,
            Mathf.SmoothDamp(currentInjuredLayerWeight,
            targetInjuredLayerWeight,
            ref layerWeightVelocity,
            0.2f)
            );
    }
}
