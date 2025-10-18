using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Controller.enabled = false;
    }
    public override void Execute(float deltaTime)
    {
        if (stateMachine.IsGameOver)
        {
            stateMachine.GameOver.SetActive(true);
        }
    }
    public override void Exit()
    {
        
    }
}
