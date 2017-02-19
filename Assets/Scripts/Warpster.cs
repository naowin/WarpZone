using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpster : MonoBehaviour {

    public WarpSystem ws;
    public float velocity;
    public float rotationVelocity;

    private WarpSection currentSection;
    private float spaceRotation;
    private float sparkRotation;
    private Transform space;
    private Transform rotator;

    private float deltaToRotation;
    private float systemRotation;
    private float distanceTraveled;

    private void Start()
    {
        space = ws.transform.parent;
        rotator = transform.GetChild(0);            
        currentSection = ws.SetupFirstWarp();
        SetupCurrentWarp();
    }

    private void Update()
    {
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;

        if(systemRotation >= currentSection.CurveAngle)
        {
            delta = (systemRotation - currentSection.CurveAngle) / deltaToRotation;
            currentSection = ws.SetupNextWarp();
            SetupCurrentWarp();
            systemRotation = delta * deltaToRotation;
        }

        ws.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        UpdateSparkRotation();
    }

    private void SetupCurrentWarp()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentSection.CurveRadius);
        spaceRotation += currentSection.RelativeRotation;
        if(spaceRotation < 0f)
        {
            spaceRotation += 360f;
        } 
        else if(spaceRotation >= 360f)
        {
            spaceRotation -= 360f;
        }

        space.localRotation = Quaternion.Euler(spaceRotation, 0f, 0f);
    }

    private void UpdateSparkRotation()
    {
        sparkRotation += rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if(sparkRotation < 0f)
        {
            sparkRotation += 360f;
        }
        else if(sparkRotation >= 360f)
        {
            sparkRotation -= 360f;
        }

        rotator.localRotation = Quaternion.Euler(sparkRotation, 0f, 0f);
    }

    // {}
}
