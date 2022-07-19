using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mov : MonoBehaviour
{
    //[Range(0,1)]
    //public float dotCutOff = 0.7f;
    private Vector2 MovementAxis;
    public Vector2 MovementAxis { get { return MovementAxis.normalized; } set { MovementAxis = value; } }
    private Vector3 Direction;
    public Camera cam;
    public float Speed, MaxSpeed = 2, StopSpeedOffset = 0.2f;
    public GameObject prop;
    bool moving;
    Vector3 Forward, Right, Movement;

    [SerializeField] float LerpRotationPercentatge = 0.2f;
    [SerializeField] float CurrVelocityPlayer;
    CharacterController CharacterController;

    public Animator Anim;
    // bool cutOff;
    private void Start()
    {
        Speed = MaxSpeed;
        CharacterController = GetComponent<CharacterController>();

         
        GameManager.GetManager().playerInputs._MoveUp += MoveUp;
        GameManager.GetManager().playerInputs._MoveDown += MoveDown;
        GameManager.GetManager().playerInputs._MoveLeft += MoveLeft;
        GameManager.GetManager().playerInputs._MoveRight += MoveRight;
        GameManager.GetManager().playerInputs._ResetMove += ResetMove;
        GameManager.GetManager().playerInputs._StopMoving += StopMoving;
    }

    private void OnDisable()
    {
        GameManager.GetManager().playerInputs. _MoveUp -= MoveUp;
        GameManager.GetManager().playerInputs._MoveDown -= MoveDown;
        GameManager.GetManager().playerInputs._MoveLeft -= MoveLeft;
        GameManager.GetManager().playerInputs._MoveRight -= MoveRight;
        GameManager.GetManager().playerInputs._ResetMove -= ResetMove;
        GameManager.GetManager().playerInputs._StopMoving -= StopMoving;
    }


    

    private void ResetMove()
    {
        MovementAxis = Vector2.zero;
    }
    private void MoveLeft()
    {
        MovementAxis += new Vector2(-1, 0);
        moving = true;
    }

    private void MoveRight()
    {
        MovementAxis += new Vector2(1, 0);
        moving = true;
    }

    private void MoveUp()
    {
        MovementAxis += new Vector2(0, 1);
        moving = true;
    }

    private void MoveDown()
    {
        MovementAxis += new Vector2(0, -1);
        moving = true;
    }

    private void StopMoving()
    {
        MovementAxis = Vector2.zero;
        moving = false;
    }

    void Update()
    {
        if (GameManager.GetManager().gameStateController.CurrentStateGame != GameStateController.StateGame.GamePlay)
        {
            //CurrVelocityPlayer = 0;
            //Anim.SetFloat("Speed", Mathf.Clamp(CurrVelocityPlayer, 0, 1));
            return;
        }

        if (MovementAxis != Vector2.zero)
        {
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward.Normalize();
            right.Normalize();

            Vector2 movementAxis = MovementAxis;
            Direction += forward * movementAxis.y;
            Direction += right * movementAxis.x;
             Direction.Normalize();

            Vector3 movement = Direction*Time.deltaTime* Speed;

            CollisionFlags CollisionFlags = CharacterController.Move(movement);
        }

        

        //parametros de la camara
        //Forward = cam.transform.forward;
        //Right = cam.transform.right;

        //Forward.y = 0;
        //Right.y = 0;

        //Forward.Normalize();
        //Right.Normalize();

        //if (Input.GetKey(KeyCode.A))
        //{
        //    Movement = -Right;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    Movement = Right;
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    Movement += Forward;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    Movement -= Forward;
        //}

        //slow anim transition(walk to idle)
        //if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        //{
        //    Speed -= 0.03f;
        //    Speed = Mathf.Clamp(Speed, 0.0f, MaxSpeed);
        //    CurrVelocityPlayer = CharacterController.velocity.magnitude;
        //    CurrVelocityPlayer -= 0.01f;
        //    Mathf.Clamp(CurrVelocityPlayer, 0, 1);
        //}
        //else
        //{
        //    CurrVelocityPlayer = CharacterController.velocity.magnitude;
        //    Speed = MaxSpeed;
        //}

        //print(CalculateWall(Forward));
        //Debug.DrawLine(transform.position, transform.position + Forward * 10);

        //if (cutOff)//(CalculateWall(Forward))
        //{
        //    CurrVelocityPlayer = 0;
        //    StartCoroutine(DelayAnimation());
        //}

        //Anim.SetFloat("Speed", Mathf.Clamp(CurrVelocityPlayer, 0, 1));

        //Movement.Normalize();

        //if (Movement != Vector3.zero)
        //    prop.transform.rotation = Quaternion.Lerp(prop.transform.rotation, Quaternion.LookRotation(Movement), LerpRotationPercentatge);

        //Movement *= Speed * Time.deltaTime;

        //CollisionFlags CollisionFlags = CharacterController.Move(Movement);

        //SetAnimations();
    }

    private void SetAnimations()
    {
        if (CharacterController.velocity.magnitude <= MaxSpeed - StopSpeedOffset)
        {
            Speed = 0.0f;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == 3 << 3)
    //    {
    //        Vector3 otherPos = collision.transform.position - transform.position;
    //        float dot = Vector3.Dot(transform.forward, otherPos);

    //        cutOff = dot < dotCutOff;
    //        print(cutOff + collision.collider.name);
    //    }
    //}
}
