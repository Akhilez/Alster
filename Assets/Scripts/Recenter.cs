using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class Recenter : MonoBehaviour
{

    public GameObject placementIndicator;
    private ARSessionOrigin sessionOrigin;
    private Pose placementPose;

    private bool placementPoseIsValid = false;

    void Start()
    {
        sessionOrigin = FindObjectOfType<ARSessionOrigin>();
        Debug.Log("Created session origin");
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        placementIndicator.SetActive(placementPoseIsValid);
        if (placementPoseIsValid)
        {
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        } else
        {
            
        }
    }

    private void UpdatePlacementPose () {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        sessionOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;


        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            // Debug.Log("Found a plane");
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

}
