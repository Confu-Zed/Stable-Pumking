using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        Debug.Log("Stated");
    }
    public override void Execute(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        
    }
}
