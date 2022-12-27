using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StandardRK;
using UnityEditor;


public partial class RubiksCube : MonoBehaviour
{
    private GameObject clickedGameObject1 = null;
    private Vector3 mouseClick1;
    private Vector3 mouseClick2;

    private GameObject clickedGameObject2 = null;
    private Vector3 mouseClick21;
    private Vector3 mouseClick22;


    void LeftButtonDownFcn()
    {
        mouseClick1 = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseClick1);
        RaycastHit hit = new();
        if (Physics.Raycast(ray, out hit, MainCamera.transform.position.magnitude))
        {
            clickedGameObject1 = hit.collider.gameObject;
        }
        else
        {
            clickedGameObject1 = null;
        }
    }

    void LeftButtonUpFcn()
    {
        mouseClick2 = Input.mousePosition;
        Vector3 diff = mouseClick2 - mouseClick1;
        Vector3 worldMov;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            worldMov = Camera.main.transform.TransformDirection((diff.x > 0) ? Vector3.down : Vector3.up);
            // When mouse moves to right, the rotation axis should be vertical
            // Convert camera's vertical direction vector to global world coordinate
        }
        else
        {
            worldMov = Camera.main.transform.TransformDirection((diff.y > 0) ? Vector3.right : Vector3.left);
            // When mouse moves to up, the rotation axis should be horizontal
            // Convert camera's horizontal direction vector to global world coordinate
        }
        worldMov.Normalize();
        float[] XYZdot = new float[3];
        XYZdot[0] = Vector3.Dot(worldMov, Vector3.right);
        XYZdot[1] = Vector3.Dot(worldMov, Vector3.up);
        XYZdot[2] = Vector3.Dot(worldMov, Vector3.forward);
        Vector3 rotAxis = new(0, 0, 0);
        int rotangle = 0, linenum = 0;
        bool rotateFlag = true;
        if (diff.magnitude < 30)
        {
            rotateFlag = false;
        }
        else if (Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[1]) / 1.5f && Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[2]) / 1.5f)
        {
            rotangle = (XYZdot[0] > 0) ? 90 : -90;
            rotAxis = new Vector3(1, 0, 0);
            linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.x);
        }
        else if (Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[0]) / 1.5f && Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[2]) / 1.5f)
        {
            rotangle = (XYZdot[1] > 0) ? 90 : -90;
            rotAxis = new Vector3(0, 1, 0);
            linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.y);

        }
        else if (Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[0]) / 1.5f && Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[1]) / 1.5f)
        {
            rotangle = (XYZdot[2] > 0) ? 90 : -90;
            rotAxis = new Vector3(0, 0, 1);
            linenum = Mathf.RoundToInt(clickedGameObject1.transform.position.z);
        }
        else
        {
            rotateFlag = false;
        }
        if (rotateFlag)
        {
            RotateCube(rotAxis, linenum, rotangle);
        }
        clickedGameObject1 = null;
    }


    void RightButtonDownFcn()
    {
        mouseClick21 = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseClick21);
        RaycastHit hit = new();
        if (Physics.Raycast(ray, out hit, MainCamera.transform.position.magnitude))
        {
            clickedGameObject2 = hit.collider.gameObject;
        }
        else
        {
            clickedGameObject2 = null;
        }
    }

    void RightButtonUpFcn()
    {
        mouseClick22 = Input.mousePosition;
        Vector3 diff = mouseClick22 - mouseClick21;
        Vector3 worldMov;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            worldMov = Camera.main.transform.TransformDirection((diff.x > 0) ? Vector3.down : Vector3.up);
            // When mouse moves to right, the rotation axis should be vertical
            // Convert camera's vertical direction vector to global world coordinate
        }
        else
        {
            worldMov = Camera.main.transform.TransformDirection((diff.y > 0) ? Vector3.right : Vector3.left);
            // When mouse moves to up, the rotation axis should be horizontal
            // Convert camera's horizontal direction vector to global world coordinate
        }
        worldMov.Normalize();
        float[] XYZdot = new float[3];
        XYZdot[0] = Vector3.Dot(worldMov, Vector3.right);
        XYZdot[1] = Vector3.Dot(worldMov, Vector3.up);
        XYZdot[2] = Vector3.Dot(worldMov, Vector3.forward);
        Vector3 rotAxis = new(0, 0, 0);
        int rotangle = 0;
        bool rotateFlag = true;
        if (diff.magnitude < 30)
        {
            rotateFlag = false;
        }
        else if (Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[1]) / 1.5f && Mathf.Abs(XYZdot[0]) > Mathf.Abs(XYZdot[2]) / 1.5f)
        {
            rotangle = (XYZdot[0] > 0) ? 90 : -90;
            rotAxis = new Vector3(1, 0, 0);
        }
        else if (Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[0]) / 1.5f && Mathf.Abs(XYZdot[1]) > Mathf.Abs(XYZdot[2]) / 1.5f)
        {
            rotangle = (XYZdot[1] > 0) ? 90 : -90;
            rotAxis = new Vector3(0, 1, 0);
        }
        else if (Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[0]) / 1.5f && Mathf.Abs(XYZdot[2]) > Mathf.Abs(XYZdot[1]) / 1.5f)
        {
            rotangle = (XYZdot[2] > 0) ? 90 : -90;
            rotAxis = new Vector3(0, 0, 1);
        }
        else
        {
            rotateFlag = false;
        }
        if (rotateFlag)
        {
            AllRotation(rotAxis, rotangle);
        }
        clickedGameObject2 = null;
    }


}

