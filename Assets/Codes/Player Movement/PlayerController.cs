using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    public NewInputSystem input;        // Input action class reference
    NavMeshAgent agent;          // Controls movement
    Animator animator;           // Controls animations

    [Header("Movement Settings")]
    [SerializeField] ParticleSystem clickEffect; 
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;   // Speed of rotation

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new NewInputSystem();   // Must match your input action class
        AssignInput();
    }

    void AssignInput()
    {
        // Move action triggers click-to-move
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            agent.SetDestination(hit.point);

            // Spawn and destroy click effect
            if (clickEffect != null)
            {
                ParticleSystem effect = Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), Quaternion.identity);
                Destroy(effect.gameObject, effect.main.duration);  // 🔥 destroy after finish
            }
        }
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        FaceTarget();
        SetAnimations();
    }

    // Smoothly face toward target
    void FaceTarget()
    {
        if (!agent.hasPath) return;

        Vector3 direction = (agent.steeringTarget - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    // Switch between Idle and Walk animations
    void SetAnimations()
    {
        if (agent.velocity.magnitude < 0.1f)
            animator.Play(IDLE);
        else
            animator.Play(WALK);
    }
}
