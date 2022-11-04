using UnityEngine;

public class RunningState : IState
{
    BaseState BaseState;
    PlayerController controller;
    Rigidbody rigidbody;
    Animator anim;

    public float forwardValue;
    public float sidewardValue;
    public float rotateSpeed = 1000f;
    public float runSpeed = 10f;

    private bool isAttackPressed;

    Quaternion playerRotation;
    
    public RunningState(BaseState baseState, PlayerController playerController, Rigidbody rigid, Animator animator)
    {
        BaseState = baseState;
        controller = playerController;
        rigidbody = rigid;
        anim = animator;
    }
    
    public void Enter()
    {
        Debug.Log("Running State!!!");
        anim.SetBool("isRunning", true);
    }

    public void Exit()
    {
        isAttackPressed = false;
        anim.SetBool("isRunning", false);
    }

    public void FixedUpdateLogic()
    {
        PlayerRun();
        PlayerRotation();
        CheckStopping();
        CheckFirstAttack();
    }

    public void UpdateLogic()
    {
        forwardValue = controller.inputValue.y;
        sidewardValue = controller.inputValue.x;

        playerRotation = controller.transform.rotation;
    }

    void PlayerRotation()
    {
        Vector3 cameraVector = new Vector3(controller.transform.position.x - Camera.main.transform.position.x, 0.0f,
            controller.transform.position.z - Camera.main.transform.position.z);

        Vector3 playerLookDirection = Quaternion.LookRotation(cameraVector) * new Vector3(sidewardValue, 0.0f, forwardValue);

        if (playerLookDirection != Vector3.zero)
        {
            Quaternion destinationRotation = Quaternion.LookRotation(playerLookDirection);
            controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, destinationRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void PlayerRun()
    {
        rigidbody.MovePosition(controller.transform.position + controller.transform.forward * runSpeed * Time.deltaTime);
    }

    void CheckStopping()
    {
        if (Mathf.Approximately(controller.inputValue.sqrMagnitude, 0f))
        {
            BaseState.ChangeState(BaseState.StoppingState);
        }
    }

    void CheckFirstAttack()
    {
        if ((controller.inputAttackValue) && !isAttackPressed)
        {
            isAttackPressed = true;
            BaseState.ChangeState(BaseState.FirstAttackState);
        }
    }
}
