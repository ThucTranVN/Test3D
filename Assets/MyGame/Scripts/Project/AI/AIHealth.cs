using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : Health
{
    private float blinkDuration;
    private AIAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private UIHealthBar healthBar;

    protected override void OnStart()
    {
        agent = GetComponent<AIAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        if (DataManager.HasInstance)
        {
            blinkDuration = DataManager.Instance.DataConfig.BlinkDuration;
            maxHealth = DataManager.Instance.DataConfig.AIMaxHealth;
            currentHealth = maxHealth;
        }
    }

    protected override void OnDamage(Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
    }

    protected override void OnDeath(Vector3 direction)
    {
        AIDeathState deathState = agent.stateMachine.GetState(AIStateID.Death) as AIDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AIStateID.Death);
    }

    private IEnumerator EnemyFlash()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(blinkDuration);
        meshRenderer.material.DisableKeyword("_EMISSION");
        StopCoroutine(nameof(EnemyFlash));
    }
}
