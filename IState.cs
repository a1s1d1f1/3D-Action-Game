using UnityEngine;

public interface IState
{
    void Enter();
    void UpdateLogic();
    void FixedUpdateLogic();
    void Exit();
}
