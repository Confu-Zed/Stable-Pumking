using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public float BalancedSpeed { get; private set; }
    [field: SerializeField] public float UnbalancedSpeed { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public bool IsUnbalanced { get; private set; }
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();
        ChangeState(new PlayerBalancedState(this));
    }
    public Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += transform.right * InputReader.MovementValue.x;
        movement += transform.forward * InputReader.MovementValue.y;

        return movement;
    }
    public void UpdateAnimator(float deltaTime, int LocomotionRightHash, int LocomotionForwardHash, float CrossFadeDuration)
    {
        if (InputReader.MovementValue.x == 0)
        {
            Animator.SetFloat(LocomotionRightHash, 0, CrossFadeDuration, deltaTime);
        }
        else
        {
            float value = InputReader.MovementValue.x > 0 ? 1f : -1f;
            Animator.SetFloat(LocomotionRightHash, value, CrossFadeDuration, deltaTime);
        }

        if (InputReader.MovementValue.y == 0)
        {
            Animator.SetFloat(LocomotionForwardHash, 0, CrossFadeDuration, deltaTime);
        }
        else
        {
            float value = InputReader.MovementValue.y > 0 ? 1f : -1f;
            Animator.SetFloat(LocomotionForwardHash, value, CrossFadeDuration, deltaTime);
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Unbalanced"))
            IsUnbalanced = true;
    }
    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.CompareTag("Unbalanced"))
            IsUnbalanced = false;
    }
}
