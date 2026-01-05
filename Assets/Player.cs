using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement data")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float verticalInput;
    private float horizontalInput;

    [Header("Tower data")]
    [SerializeField] private Transform towerTransform;
    [SerializeField] private float towerRotationSpeed;

    [Header("Aim data")]
    [SerializeField] private LayerMask whatIsAimMask;
    [SerializeField] private Transform aimTransform;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        UpdateAim();
        CheckInput();

    }

    private void CheckInput()
    {
        // w = 1, s = -1...
        verticalInput = Input.GetAxis("Vertical"); // z
        horizontalInput = Input.GetAxis("Horizontal"); // x

        // back move adjust
        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyBodyRotation();
        ApplyTowersRotation();
    }

    private void ApplyTowersRotation()
    {
        Vector3 direction = aimTransform.position - towerTransform.position; // 銃からポインタへ向かうベクトルが出る
        direction.y = 0; // 上下を無視して、水平回転だけしたい
        Quaternion targetRotation = Quaternion.LookRotation(direction); // 前方向がdirectionになるような回転の定義
        towerTransform.rotation = Quaternion.RotateTowards(towerTransform.rotation, targetRotation, towerRotationSpeed); // 目標の値に少しずつ近づける。
    }

    private void ApplyBodyRotation()
    {
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveSpeed * verticalInput;
        rb.velocity = movement;
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsAimMask))
        {
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3(hit.point.x, fixedY, hit.point.z);
        }
    }
}
