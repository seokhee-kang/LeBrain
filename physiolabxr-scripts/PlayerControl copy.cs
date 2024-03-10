using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public WeaponSwitching weaponSwitchingController;
    public WeaponSwitching weaponSwitchingHand;
    public MotorImagery childActivator;
    public Camera mainCamera;
    public float maxViewDistance = 100f;
    public float fieldOfViewAngle = 60f;
    public ScoreSystem score;

    private WeaponSwitching activeWeaponSwitching;

    private ActionLSLInput actionLSLInput = new ActionLSLInput();

    void Update()
    {
        DetermineActiveWeaponSwitching();
        
        if (activeWeaponSwitching != null)
        {
            activeWeaponSwitching.SetSelectedWeapon(actionLSLInput.currentWeaponIndex);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Assuming the first child is at index 0
            childActivator.ActivateChild(0, 16);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Assuming the second child is at index 1
            childActivator.ActivateChild(1, 20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeClosestTargetInvisibleAndBack();
        }
    }

    void MakeClosestTargetInvisibleAndBack()
    {
        GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        GameObject[] targets = new GameObject[robots.Length + drones.Length];
        robots.CopyTo(targets, 0);
        drones.CopyTo(targets, robots.Length);
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        float smallestAngle = 180f;
        

        foreach (GameObject target in targets)
        {
            Vector3 directionToTarget = target.transform.position - cameraPosition;
            float angle = Vector3.Angle(directionToTarget, cameraForward);
            float sqrDistanceToTarget = directionToTarget.sqrMagnitude;

            if (angle <= smallestAngle)
            {
                if (angle < smallestAngle || (angle == smallestAngle && sqrDistanceToTarget < closestDistanceSqr))
                {
                    smallestAngle = angle;
                    closestDistanceSqr = sqrDistanceToTarget;
                    closestTarget = target;
                }
            }
        }

        if (closestTarget != null)
        {
            int scoreToAdd = 0; // Default score to add
            if (closestTarget.CompareTag("Robot"))
            {
                scoreToAdd = 120; // Score value for Robot
            }
            else if (closestTarget.CompareTag("Drone"))
            {
                scoreToAdd = 240; // Score value for Drone
            }

            closestTarget.GetComponent<Target>().MakeInvisibleAndBack();
            score.AddScore(scoreToAdd);
        }
    }

    void DetermineActiveWeaponSwitching()
    {
        // Check if the controller weapon holder is active, and assign it as the active weapon switching component
        if (weaponSwitchingController != null && weaponSwitchingController.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingController;
        }
        // Otherwise, check if the hand weapon holder is active
        else if (weaponSwitchingHand != null && weaponSwitchingHand.gameObject.activeInHierarchy)
        {
            activeWeaponSwitching = weaponSwitchingHand;
        }
        else
        {
            activeWeaponSwitching = null; // No active weapon holder found
        }
    }
}

