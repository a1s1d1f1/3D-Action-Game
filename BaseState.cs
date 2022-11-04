using UnityEngine;

public class BaseState : StateMachine
{
    public StandbyforBattleState StandbyforBattleState { get; private set; }
    public IdlingState IdlingState { get; private set; }
    public RunningState RunningState { get; private set; }
    public StoppingState StoppingState { get; private set; }
    public FirstAttackState FirstAttackState { get; private set; }
    public SecondAttackState SecondAttackState { get; private set; }
    public ThirdAttackState ThirdAttackState { get; private set; }
    public FourthAttackState FourthAttackState { get; private set; }
    public FifthAttackState FifthAttackState { get; private set; }

    PlayerController playerController;
    Rigidbody rigid;
    Animator animator;
    [SerializeField] AttackData[] attackData;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        StandbyforBattleState = new StandbyforBattleState(this, playerController, animator);
        IdlingState = new IdlingState(this, playerController, animator);
        RunningState = new RunningState(this, playerController, rigid, animator);
        StoppingState = new StoppingState(this, playerController, animator);
        FirstAttackState = new FirstAttackState(this, playerController, attackData, animator);
        SecondAttackState = new SecondAttackState(this, playerController, attackData, animator);
        ThirdAttackState = new ThirdAttackState(this, playerController, attackData, animator);
        FourthAttackState = new FourthAttackState(this, playerController, attackData, animator);
        FifthAttackState = new FifthAttackState(this, playerController, attackData, animator);
    }

    void Start()
    {
        ChangeState(StandbyforBattleState);
    }
}