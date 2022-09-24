using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Buret : MonoBehaviour
{
    private const float _minNozzleTurnAngle = 0.0f, _maxNozzleTurnAngle = 90.0f;
    
    [Range(_minNozzleTurnAngle, _maxNozzleTurnAngle)]
    public float nozzleTurnAngle = 0.0f;
    
    public GameObject liquid;
    public GameObject nozzle;
    [HideInInspector]
    public Interactable buretInteractable;

    private DropletSpawner dropletSpawner;
    private Interactable nozzleInteractable;
    private SteamVR_Skeleton_Poser nozzlePoser;
    private SteamVR_Action_Vector2 scrollAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("NozzleAdjust");

    // Start is called before the first frame update
    void Start()
    {
        buretInteractable = GetComponent<Interactable>();
        dropletSpawner = GetComponentInChildren<DropletSpawner>();

        var nozzle = transform.Find("buret_nozzle_pivot");
        nozzleInteractable = nozzle.GetComponent<Interactable>();
        nozzlePoser = nozzle.GetComponent<SteamVR_Skeleton_Poser>();

        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), collider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var nozzleSensitivity = 2.0f;

        // Adjust hand pose
        if (nozzleInteractable.attachedToHand)
        {
            IncrementNozzleAngle(scrollAction.GetAxis(nozzleInteractable.attachedToHand.handType).y * nozzleSensitivity);
            nozzlePoser.SetBlendingBehaviourValue("NozzleTwistBlend", nozzleTurnAngle / 90.0f);
        }

        if (nozzleTurnAngle == 90.0f)
        {
            dropletSpawner.timerEnabled = false;
        }
        else
        {
            dropletSpawner.timerEnabled = true;
            dropletSpawner.frequency = Mathf.Lerp(0.0f, 1.0f, nozzleTurnAngle / 90.0f);
        }
    }

    private void OnValidate()
    {
        SetNozzleAngle(nozzleTurnAngle);
    }

    private void IncrementNozzleAngle(float angle)
    {
        var newAngle = nozzleTurnAngle + angle;
        SetNozzleAngle(newAngle);
    }

    private void SetNozzleAngle(float angle)
    {
        nozzleTurnAngle = Mathf.Clamp(angle, _minNozzleTurnAngle, _maxNozzleTurnAngle);
        nozzle.transform.localRotation = Quaternion.Euler(-nozzleTurnAngle, 0.0f, 0.0f);
    }
}
