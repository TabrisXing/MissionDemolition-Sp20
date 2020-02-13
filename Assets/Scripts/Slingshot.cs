﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject prefabProjectile;

    public float velocityMult = 8f;

    [Header("Set Dynamically")]

    public GameObject launchPoint;

    public Vector3 launchPos;
    public GameObject projectile; 
    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); 
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter()
    {
        print("Slingshot:OnMouseEnter()");
    }
    void OnMouseExit()
    {
        print("Slingshot:OnMouseExit()");
    }

    void OnMouseDown()
    { 
      // The player has pressed the mouse button while over Slingshot
        aimingMode = true;
        // Instantiate a Projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        // Start it at the launchPoint
        projectile.transform.position = launchPos;
        // Set it to isKinematic for now
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {   
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;

            FollowCam.POI = projectile;
            projectile = null;
        }
    }


}