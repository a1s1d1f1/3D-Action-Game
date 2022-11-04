using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackState : IState
{
    BaseState BaseState;
    PlayerController controller;
    Animator anim;
    AttackData AttackData;

    private float currentAnimationTime;
    private bool isNextAttack;

    float _moveTimer = 0.0f;


    public FirstAttackState(BaseState baseState, PlayerController playerController, AttackData[] attackData, Animator animator)
    {
        BaseState = baseState;
        controller = playerController;
        AttackData = attackData[0];
        anim = animator;
    }

    public void Enter()
    {
        Debug.Log("FirstAttack State!!!");
        anim.SetBool("isFirstAttack", true);
    }

    public void Exit()
    {
        anim.SetBool("isFirstAttack", false);
    }

    public void FixedUpdateLogic()
    {
        AttackMove();
        CheckAttackPressed();
        CheckNextAction();
    }

    public void UpdateLogic()
    {
        currentAnimationTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void CheckAttackPressed()
    {
        if ((currentAnimationTime >= 0.1f && currentAnimationTime <= 0.5f) && !isNextAttack)
        {
            isNextAttack = controller.inputAttackValue;
        }
    }

    void CheckNextAction()
    {
        if (currentAnimationTime >= 0.5f && isNextAttack)
            BaseState.ChangeState(BaseState.SecondAttackState);

        else if ((currentAnimationTime >= 0.5f && !isNextAttack) && !Mathf.Approximately(controller.inputValue.sqrMagnitude, 0f))
            BaseState.ChangeState(BaseState.RunningState);

        else if (currentAnimationTime >= 0.5f && !isNextAttack)
            BaseState.ChangeState(BaseState.StandbyforBattleState);
    }

    void AttackMove()
    {
        //controller.transform.position = controller.transform.position + controller.transform.forward * animSpeed * Time.deltaTime;

        _moveTimer += Time.deltaTime;
        float speed = AttackData._moveCurve.Evaluate(_moveTimer) * AttackData.acceleration;
        controller.transform.position = controller.transform.position + controller.transform.forward * speed;
    }
}