using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] private float moveSpeed = 5f;      
    [SerializeField] private bool stop = false;   
    [SerializeField] private float interpolationSpeed = 5f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [field: SerializeField] public GameObject RedColor { get; set; }
    [field: SerializeField] public bool red { get; set; }
    [field: SerializeField] public GameObject OrangeColor { get; set; }
    [field: SerializeField] public bool orange { get; set; }
    [field: SerializeField] public GameObject BlueColor { get; set; }
    [field: SerializeField] public bool blue { get; set; }
    [field: SerializeField] public GameObject GreenColor { get; set; }
    [field: SerializeField] public bool green { get; set; }
    [Header("GroundMat")]
    [field:SerializeField] public Material[] groundMat;
    [field:SerializeField] public MeshRenderer[] platform;
    [field:SerializeField] public MeshRenderer platformEnd;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] footStepClip;

    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody rb;               
    private float targetSpeed;           

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetSpeed = 0f;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isPlaying) return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (stop)
        {
            targetSpeed = 0f;
        }
        else
        {
            targetSpeed = 1f;
        }
        float currentSpeed = Mathf.Lerp(animator.GetFloat(SPEED_TAG), targetSpeed, interpolationSpeed * Time.deltaTime);

        animator.SetFloat(SPEED_TAG, currentSpeed);

        Vector3 forwardMovement = transform.forward * currentSpeed * moveSpeed;

        Vector3 movement = forwardMovement;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    public async void StartDancing()
    {
        stop = true;
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        await Task.Delay(800);
        animator.SetTrigger(DANCE_TAG);
    }

    public void FootStep()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return footStepClip[Random.Range(0, footStepClip.Length)];
    }

    #region Cached Properties

    private readonly int SPEED_TAG = Animator.StringToHash("Speed");
    private readonly int DANCE_TAG = Animator.StringToHash("Dance");

    #endregion
}