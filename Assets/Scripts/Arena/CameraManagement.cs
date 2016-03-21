using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arena;

public class CameraManagement : MonoBehaviour {
    public static CameraManagement Instance;
    [SerializeField]
    public Camera currentCamera;
    public CameraState cameraStance = CameraState.CENTER;
    private ArenaManagement arena;
    private float player1X, player2X;
    Vector3 targetPos;
    [SerializeField] public Transform camTransform;
    [SerializeField] public float shakeDuration = 0.1f;
   
    [SerializeField] public float decreaseFactor = 1.0f;
    private bool shaking = false;
    public bool InBox, Left, Right;
    Vector3 originalPos;
    public CameraVars ownCameraPosition;

    void Awake()
    {
        //Fightclub = -5 -5 50 70 75
        Instance = this;
        if(currentCamera == null)
        {
            currentCamera = Camera.main;
        }
        if (arena == null)
        {
            arena = GetComponent<ArenaManagement>();
        }
        camTransform = currentCamera.transform;
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        player1X = arena.Players[0].playerInformation.transform.position.x;
        player2X = arena.Players[1].playerInformation.transform.position.x;
        
        TurnBoolsOff();
        CheckCharacterPosition(player1X);
        CheckCharacterPosition(player2X);
        ChangeCameraState();
        ChangeCameraPosition(cameraStance);
         CameraShake(0.04f);


    }


    public void ChangeCameraPosition(CameraState targetState)
    {
        switch (targetState)
        {
            case CameraState.CENTER:
                currentCamera.transform.localPosition = new Vector3(Mathf.SmoothStep(camTransform.localPosition.x, 0, 0.1f), 0, -10);
                currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, ownCameraPosition.startSize,0.1f);
            break;
            case CameraState.LEFT:
                currentCamera.transform.localPosition = new Vector3(Mathf.SmoothStep(camTransform.localPosition.x, ownCameraPosition.min, 0.1f), 0, -10);
                currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, ownCameraPosition.cornerSize, 0.1f);
            break;
            case CameraState.RIGHT:
                currentCamera.transform.localPosition = new Vector3(Mathf.SmoothStep(camTransform.localPosition.x, ownCameraPosition.max, 0.1f),0,-10);
                currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, ownCameraPosition.cornerSize, 0.1f);
            break;
            case CameraState.FULL:
                currentCamera.transform.localPosition = new Vector3(Mathf.SmoothStep(camTransform.localPosition.x, 0, 0.1f), 0, -10);
                currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, ownCameraPosition.fullSize, 0.1f);
            break;
        }
    }

    public void ChangeCameraState()
    {
        if (Left == true)
        {
            if(Right == true)
            {
                cameraStance = CameraState.FULL;

            }
            else
            {
                cameraStance = CameraState.LEFT;
            }
        }
        else if (Right == true)
        {
            cameraStance = CameraState.RIGHT;

        }
        else if (InBox == true)
        {
            cameraStance = CameraState.CENTER;
        }   
    }

    public void CheckCharacterPosition(float PlayerXPos)
    {
        if (PlayerXPos < -8.5)
        {
            Left = true;
        }
        else if (PlayerXPos > 8.5)
        {
            Right = true;
        }
        else
        {
            InBox = true;
        }

    }

    public void TurnBoolsOff()
    {
        Left = false;
        Right = false;
        InBox = false;
    }

    public void CameraShake(float intensity)
    {
        if (shakeDuration > 0)
        {
            if(shaking == false)
            {
                originalPos = camTransform.localPosition;
            }
            shaking = true;
            camTransform.localPosition = camTransform.localPosition + Random.insideUnitSphere * intensity;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shaking = false;
            shakeDuration = 0f;
            //camTransform.localPosition = new Vector3(Mathf.SmoothStep(camTransform.localPosition.x,originalPos.x,0.5f), Mathf.SmoothStep(camTransform.localPosition.y, originalPos.y, 0.5f),originalPos.z);
        }
    }

    public IEnumerator CameraBounce(float intensity)
    {
        shaking = true;
        for (int i = 0; i < 3;i++)
        {
            camTransform.localPosition = camTransform.localPosition + Random.insideUnitSphere * intensity;
        }
        yield return new WaitForSeconds(0.1f);
        shaking = false;

    }
    bool OnPosition()
    {
        return true;
    }





}

[System.Serializable]
public class CameraVars
{
    public float min;
    public float max;
    public float startSize;
    public float cornerSize;
    public float fullSize;

    public CameraVars(float Min,float Max, float StartSize,float CornerSize,float FullSize)
    {
        this.min        = Min;
        this.max        = Max;

        this.startSize  = StartSize;
        this.cornerSize = CornerSize;
        this.fullSize   = FullSize;
    }
}
public enum CameraState
{
    CENTER,
    LEFT,
    RIGHT,
    FULL
}
