using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Arena;

public class CameraManagement : MonoBehaviour {
    public static CameraManagement Instance;
    [SerializeField]
    public Camera currentCamera;
    private ArenaManagement arena;
    private Vector3 player1X, player2X;
    float Xpos,Ypos,Zpos;
    Vector3 targetPos;
    [SerializeField] public Transform camTransform;
    [SerializeField] public float shakeDuration = 0f;
   
    [SerializeField] public float decreaseFactor = 1.0f;
    private bool shaking = false;

    Vector3 originalPos;

    void Awake()
    {
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

        Debug.Log(camTransform.localPosition);
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
       
        Mathf.Clamp(Xpos, -30, 30);
        player1X = new Vector3(arena.Players[0].playerInformation.transform.position.x,0,-10);
        player2X = new Vector3(arena.Players[1].playerInformation.transform.position.x,0,-10);
        targetPos = Vector3.Lerp(player1X, player2X, 0.5f);
        Xpos = targetPos.x;
        Ypos = targetPos.y;
        Zpos = targetPos.z;
        


        AdjustCameraSize(Vector2.Distance(player1X, player2X));
        if(shaking == false)
        {
            camTransform.localPosition = AdjustCameraPosition(new Vector3(Xpos, Ypos, Zpos), -30, 30);
        }

    }

    void AdjustCameraSize(float DistFromPlayers)
    {
        //Debug.Log(DistFromPlayers);
        if (DistFromPlayers < 15)
        {
            currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, 50, 0.1f);
        }
        else
        {
            if (DistFromPlayers < 20)
            {
                currentCamera.fieldOfView = Mathf.SmoothStep(currentCamera.fieldOfView, 50 + DistFromPlayers, 0.1f);
            }
        }
    }

    Vector3 AdjustCameraPosition(Vector3 tPos,float MinX, float MaxX)
    {
        if(tPos.x < MinX)
        {
            return new Vector3(-30, tPos.y, tPos.z);
        }
        else if(tPos.x > MaxX)
        {
            return new Vector3(30, tPos.y, tPos.z);

        }
        else
        {
            return tPos;
        }
    }

    void MoveCamera(Direction targetDir)
    {
        Vector2 cameraPos = currentCamera.transform.localPosition;
        if (targetDir == Direction.LEFT)
        {
            if(cameraPos.x > -30)
            {
                currentCamera.transform.Translate(-15 * Time.deltaTime, 0, 0);
            }
        }
        else if(targetDir == Direction.RIGHT)
        {
            if (cameraPos.x < 30)
            {
                currentCamera.transform.Translate(15 * Time.deltaTime, 0, 0);
            }
        }
    }

    public void CameraShake(float intensity)
    {
        if (shakeDuration > 0)
        {
            shaking = true;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * intensity;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shaking = false;
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
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

enum Direction
{
    LEFT,
    RIGHT
}
