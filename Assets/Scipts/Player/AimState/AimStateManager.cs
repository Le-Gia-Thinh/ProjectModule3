using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AimStateManager : MonoBehaviour
{

    AimBaseState currentState ;
    public HipFireState Hip =  new HipFireState();
    public AimState Aim = new AimState();
    [HideInInspector] public Animator animator;


    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform cameraFollow;
    [SerializeField] float mouseSense;



    [HideInInspector] public CinemachineVirtualCamera vCam ;
    public float adsFov = 30 ;  
    [HideInInspector] public float hipFov ;
    [HideInInspector] public float currentFov ;
    public float fovSmoothSpeed ;


    public Transform aimPos ;
    [SerializeField] float aimSmoothSpeed = 20 ;
    [SerializeField] LayerMask aimMask ;
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        SwitchState(Hip);

    }
    void Update()
    {
        xAxis.Value += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis.Value -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis.Value = Mathf.Clamp(yAxis.Value, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView , currentFov, fovSmoothSpeed * Time.deltaTime);
        
        Vector2 sceenCenter = new Vector2( Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(sceenCenter);

        if (Physics.Raycast(ray , out RaycastHit hit , Mathf.Infinity  , aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }



        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        cameraFollow.localEulerAngles = new Vector3(yAxis.Value, cameraFollow.localEulerAngles.y, cameraFollow.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);


    }
    public void SwitchState (AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
