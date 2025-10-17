using System.Linq.Expressions;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((/*stateMachine.ForceReceiver.Movement + */motion) * deltaTime);
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
}
