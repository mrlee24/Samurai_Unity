using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float smoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;
    AIController ai;

    float[] animatorIndex = { 0, 0.5f };

    public GameObject[] items;

    private bool isCovered;

    public void SetIsCovered(bool value)
        => isCovered = value;

    public bool IsCovered()
        => isCovered;

    private void Start()
    {
        items[1].gameObject.SetActive(true);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ai = GetComponent<AIController>();
        isCovered = true;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

    }

    public void PlayAnimation(string animationName)
        => animator.Play(animationName);

    public void SetFloat(float speedPercent)
        => animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);

    public void SetBool(string animation, bool decide)
        => animator.SetBool(animation, decide);

    private void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
    }

    public void HoldWeapon()
        => animator.Play("SwordIdle");

    public void PerformAttack()
    {
        animator.Play("Attack");
        //ai.MakeADecision();
    }

    public void PerformDefend()
    {
        animator.Play("Defend");
    }

    public void DrawWeapon()
    {
        animator.SetBool("isHold", false);
        items[1].gameObject.SetActive(false);
        animator.Play("WithdrawSword1");
        items[0].gameObject.SetActive(true);
        isCovered = false;
    }

    public void SheathWeapon()
    {
        animator.SetBool("isHold", true);
        items[1].gameObject.SetActive(true);
        animator.Play("SheathSword1");
        items[0].gameObject.SetActive(false);
        isCovered = true;
    }
}
