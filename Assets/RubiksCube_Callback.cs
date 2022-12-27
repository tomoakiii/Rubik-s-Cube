using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StandardRK;


public partial class RubiksCube : MonoBehaviour
{
 

    public void ResetButtonCallback()
    {
        Start();
    }

    public void RevButtonCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        for (int i = GameLog.Count - 1; i >= 0; i--)
        {
            SolveScript.Add(SingleRevOperation(GameLog[i]));
        }
    }

    public void UndoButtonCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        RotateCube_Command(SingleRevOperation(GameLog[GameLog.Count - 1]));
        GameLog.RemoveAt(GameLog.Count - 1); // Clear the log of Undo Rotation
        GameLog.RemoveAt(GameLog.Count - 1); // Clear the log of Target Rotation
        logscript.MakeLog(GameLog); // Synchronize display log
    }

    public string SingleRevOperation(string op)
    {
        string[] outop = op.Trim().Split(',');
        int tmpangle = -1 * Convert.ToInt32(outop[2]);
        return (outop[0] + ", " + outop[1] + ", " + tmpangle.ToString("F0"));
    }

    public void RandButtonCallback()
    {
        RandomIndex = 10;
        isAutoMode = AutoMode.Random10Mode;
    }

    public void RunButtonCallback()
    {
        StringReader sr = new(inputscript.get());
        string line;
        SolveScript.Clear();
        while ((line = sr.ReadLine()) != null) {
            SolveScript.Add(line);
        }
        inputscript.Clear();
        isAutoMode = AutoMode.AutoResolveMode;
    }

    public void CopyButtonCallback()
    {
        string str = "";
        for (int n = 0; n < GameLog.Count; n++)
        {
            str = str  + GameLog[n] + "\n";
        }
        GUIUtility.systemCopyBuffer = str;
    }


    public void FacingCallback()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit = new();
        if (Physics.Raycast(ray, out hit, MainCamera.transform.position.magnitude))
        {
            object tmpObject = hit.collider.gameObject;
        }
        else
        {
            EmergencyStop("Facing Failure");
        }
    }

    public void OneFaceCallback()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 1;
        AutoModeStopStage = 4;
    }
    public void AutoResolveCallback()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 1;
        AutoModeStopStage = 100;
    }

    public void UPCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        SolveScript.Add("Z, 90"); 
    }
    public void DWCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        SolveScript.Add("Z, -90");
    }
    public void RHCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        SolveScript.Add("Y, -90");
    }
    public void LHCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        SolveScript.Add("Y, 90");
    }
    public void RightTriggerCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        Solve_Operation_right();
    }
    public void LeftTriggerCallback()
    {
        isAutoMode = AutoMode.AutoResolveMode;
        SolveScript.Clear();
        Solve_Operation_left();
    }
}

