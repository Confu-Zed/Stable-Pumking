using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    private void Update()
    {
        currentState?.Execute(Time.deltaTime);
    }
    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
