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
    public void Auto6CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 6;
        SolveScript.Clear();

        YPlusYellowCross();
        if (SolveScript.Count > 0)
        {
            return;
        }
       DebugKeyword = DebugKeyword + "\n ";
       AutoModeStage = 7;
    }

 


    private void YPlusYellowCross() // 
    {
        if (RK_col.GetCellColor("+Y", 0, 1) == TargetColors[0] && RK_col.GetCellColor("+Y", 1, 0) == TargetColors[0] 
            && RK_col.GetCellColor("+Y", 1, 2) == TargetColors[0] && RK_col.GetCellColor("+Y", 2, 1) == TargetColors[0])
        { // completed
            DebugKeyword = DebugKeyword + "step6-1; ";
            return;
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) == TargetColors[0] && RK_col.GetCellColor("+Y", 1, 0) != TargetColors[0] 
            && RK_col.GetCellColor("+Y", 1, 2) == TargetColors[0] && RK_col.GetCellColor("+Y", 2, 1) != TargetColors[0])
        { // 0:15 shape
            DebugKeyword = DebugKeyword + "step6-2; ";
            SolveScript.Add("Y, -90");
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[0] && RK_col.GetCellColor("+Y", 1, 0) == TargetColors[0] 
            && RK_col.GetCellColor("+Y", 1, 2) != TargetColors[0] && RK_col.GetCellColor("+Y", 2, 1) == TargetColors[0])
        { // 9:30
            DebugKeyword = DebugKeyword + "step6-3; ";
            SolveScript.Add("Y, 90");
        }
        else if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[0] && RK_col.GetCellColor("+Y", 1, 0) != TargetColors[0] 
            && RK_col.GetCellColor("+Y", 1, 2) == TargetColors[0] && RK_col.GetCellColor("+Y", 2, 1) == TargetColors[0])
        { // 3:30
            DebugKeyword = DebugKeyword + "step6-5; ";
            SolveScript.Add("Y, 180");
        }
        
        SolveScript.Add("X, 1, 90");
        Solve_Operation_right();
        SolveScript.Add("X, 1, -90");
    }
}
