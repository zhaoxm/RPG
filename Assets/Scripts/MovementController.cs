using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    private Vector2 _movement;
    private Animator _animator;
    private Rigidbody2D _rb2D;
    private static readonly int State = Animator.StringToHash(AnimationState);
    private const string AnimationState = "AnimationState";

    private enum CharStates
    {
        WalkEast = 1,
        WalkSouth = 2,
        WalkWest = 3,
        WalkNorth = 4,
        IdleSouth = 5
    }
    
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (_movement.x)
        {
            case > 0:
                _animator.SetInteger(State, (int)CharStates.WalkEast);
                break;
            case < 0:
                _animator.SetInteger(State, (int)CharStates.WalkWest);
                break;
            default:
            {
                switch (_movement.y)
                {
                    case > 0:
                        _animator.SetInteger(State, (int)CharStates.WalkNorth);
                        break;
                    case < 0:
                        _animator.SetInteger(State, (int)CharStates.WalkSouth);
                        break;
                    default:
                        _animator.SetInteger(State, (int)CharStates.IdleSouth);
                        break;
                }
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement.Normalize();

        _rb2D.velocity = _movement * movementSpeed;
    }
}
