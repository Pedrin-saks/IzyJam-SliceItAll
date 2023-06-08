using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forceFoward = 2f;
    [SerializeField] private ForceMode spinForceMode = ForceMode.VelocityChange;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 5f;

    private Rigidbody _rigidbody;
    [SerializeField] private bool isKnifeJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(GameManager.instance.currentStatus == StatusGame.GAMEPLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnTapHandler();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.inertiaTensorRotation = Quaternion.identity;
    }

    private void OnTapHandler()
    {
        //GameManager.Instance.SetGameState(GameState.InGame);

        _rigidbody.isKinematic = false;
        Jump();
        Spin();
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector3.up * jumpForce * forceFoward;
        _rigidbody.velocity = new Vector3(forceFoward, jumpForce, _rigidbody.velocity.y);
    }

    private void Spin()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(Vector3.right * rotationSpeed, spinForceMode);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isKnifeJump && collision.gameObject.CompareTag("Platform"))
        {
            _rigidbody.isKinematic = true;
            isKnifeJump = false;
            StartCoroutine(nameof(DelayKnifeJumpCoroutine));
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.currentStatus = StatusGame.LOSE;
        }
    }

    private IEnumerator DelayKnifeJumpCoroutine()
    {
        yield return new WaitForSeconds(1f);
        isKnifeJump = true;
    }
}