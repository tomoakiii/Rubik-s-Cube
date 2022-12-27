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
    private Colors[] TargetColors = new Colors[2];
    //CheckIfFaceComplete(TargetColors);
    
    public void Auto1CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 1;
        SolveScript.Clear();

        if (CheckIfFaceComplete(TargetColors))
        {
            AutoModeStage = 5;
        }
        else
        {
            AutoModeStage = 2;
        }
        YtoDisignedColor(TargetColors[0]);
    }

    private void YtoDisignedColor(Colors inCol)
    {
        if(RK_col.GetCellColor("+X", 1, 1) == inCol)
        {
            SolveScript.Add("Z, 90");
        }
        else if(RK_col.GetCellColor("-X", 1, 1) == inCol)
        {
            SolveScript.Add("Z, -90");
        }
        else if(RK_col.GetCellColor("-Y", 1, 1) == inCol)
        {
            SolveScript.Add("Z, 180");
        }
        else if(RK_col.GetCellColor("+Z", 1, 1) == inCol)
        {
            SolveScript.Add("X, -90");
        }
        else if(RK_col.GetCellColor("-Z", 1, 1) == inCol)
        {
            SolveScript.Add("X, 90");
        }
    }


    private void XtoDesignedColor(Colors inCol)
    {
        if(RK_col.GetCellColor("-X", 1, 1) == inCol)
        {
            SolveScript.Add("Y, 180");
        }
        else if(RK_col.GetCellColor("+Z", 1, 1) == inCol)
        {
            SolveScript.Add("Y, 90");
        }
        else if(RK_col.GetCellColor("-Z", 1, 1) == inCol)
        {
            SolveScript.Add("Y, -90");
        }
    }

    
    private bool CheckIfFaceComplete(Colors[] retColors)
    {
        retColors[0] = RK_col.GetCellColor("+Y", 1, 1);
        retColors[1] = RK_col.GetCellColor("-Y", 1, 1);
        string[] scanDir = new string[] { "+X", "+Y", "+Z", "-X", "-Y", "-Z" };
        for (int i = 0; i < 6; i++)
        {
            bool isFindComp = true;
            string tmpDir = scanDir[i];
            string tmpBackDir = scanDir[(i+3)%6];

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    if (RK_col.GetCellColor(tmpDir, x, y) != RK_col.GetCellColor(tmpDir, 1, 1))
                    {
                        isFindComp = false;
                        break;
                    }
                }
                if (!isFindComp)
                {
                    break;
                }
            }
            if (isFindComp)
            {
                retColors[1] = RK_col.GetCellColor(tmpDir, 1, 1);
                retColors[0] = RK_col.GetCellColor(tmpBackDir, 1, 1);
                return true;
            }
        }
        return false;
    }
}
