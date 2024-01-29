using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float blinkDuration = 0.1f;

    private AIAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private UIHealthBar healthBar;

    void Start()
    {
        agent = GetComponent<AIAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        currentHealth = maxHealth;
        SetupHixBox();
    }

    private void SetupHixBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.gameObject.AddComponent<HitBox>().AIHealth = this;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        currentHealth -= damageAmount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
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
