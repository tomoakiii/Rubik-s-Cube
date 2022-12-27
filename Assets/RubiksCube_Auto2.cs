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
    public void Auto2CallBack()
    {
        isAutoMode = AutoMode.AutoSequenceMode;
        AutoModeStage = 2;
        SolveScript.Clear();

        FindTopMiddleYellow();
        if (SolveScript.Count > 0)
        {
            return;
        }

        FindTopMiddleYellow_Rev();
        if (SolveScript.Count > 0)
        {
            return;
        }

        FindTopMiddleYellow_Top();
        if (SolveScript.Count > 0)
        {
            return;
        }

        FindTopMiddleYellow_Side1();
        if (SolveScript.Count > 0)
        {
            return;
        }

        FindTopMiddleYellow_Side2();
        if (SolveScript.Count > 0)
        {
            return;
        }


        AutoModeStage = 3;
   }
    
    private void FindTopMiddleYellow()
    {
        // Making Flower around negative Y (yellow) = move white middle part to next to center yellow
        if ((RK_col.GetCellColor("+X", 1, 2) != TargetColors[1])
         && (RK_col.GetCellColor("-Z", 1, 2) != TargetColors[1])
         && (RK_col.GetCellColor("-X", 1, 2) != TargetColors[1])
         && (RK_col.GetCellColor("+Z", 1, 2) != TargetColors[1]))
        {
            return;
        }

        // check if next to yellow is already white
        if (RK_col.GetCellColor("+Y", 1, 2) == TargetColors[1])
        {
            if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 90");
            }
            else if (RK_col.GetCellColor("+Y", 1, 0) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 180");
            }
            else if (RK_col.GetCellColor("+Y", 2, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, -90");
            }
            else
            {
                return; // Nothing on list
            }
        }
        if (RK_col.GetCellColor("+X", 1, 2) == TargetColors[1])
        {
            SolveScript.Add("Z, 1, 90");
        }
        else if (RK_col.GetCellColor("-Z", 1, 2) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, -90");
            SolveScript.Add("Z, 1, 90");
        }
        else if (RK_col.GetCellColor("-X", 1, 2) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, 180");
            SolveScript.Add("Z, 1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 1, 2) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, 90");
            SolveScript.Add("Z, 1, 90");
        }
        else
        {
            // nothing stored on List
        }
    }

    private void FindTopMiddleYellow_Rev()
    {
        if ((RK_col.GetCellColor("+X", 1, 0) != TargetColors[1])
         && (RK_col.GetCellColor("-Z", 1, 0) != TargetColors[1])
         && (RK_col.GetCellColor("-X", 1, 0) != TargetColors[1])
         && (RK_col.GetCellColor("+Z", 1, 0) != TargetColors[1]))
        {
            return;
        }

        // Making Flower around negative Y (yellow) = move white middle part to next to center yellow
        // check if next to yellow is already white
        if (RK_col.GetCellColor("+Y", 1, 0) == TargetColors[1])
        {
            if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, -1, -90");
            }
            else if (RK_col.GetCellColor("+Y", 1, 2) != TargetColors[1])
            {
                SolveScript.Add("Y, -1, 180");
            }
            else if (RK_col.GetCellColor("+Y", 2, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, -1, 90");
            }
            else
            {
                return; // Nothing on list
            }
        }
        if (RK_col.GetCellColor("+X", 1, 0) == TargetColors[1])
        {
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("-Z", 1, 0) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, -90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("-X", 1, 0) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, 180");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 1, 0) == TargetColors[1])
        {
            SolveScript.Add("Y, 0, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else
        {
            // nothing stored on List
        }
    }

    private void FindTopMiddleYellow_Top()
    {
        // Making Flower around negative Y (yellow) = move white middle part to next to center yellow
        // check if next to yellow is already white
        if (RK_col.GetCellColor("+Y", 1, 0) == TargetColors[1])
        {
            if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, -90");
            }
            else if (RK_col.GetCellColor("+Y", 1, 2) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 180");
            }
            else if (RK_col.GetCellColor("+Y", 2, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 90");
            }
            else
            {
                return; // Nothing on list
            }
        }
        if (RK_col.GetCellColor("-Y", 1, 0) == TargetColors[1])
        {
            SolveScript.Add("Z, -1, 180");
        }
        else if (RK_col.GetCellColor("-Y", 0, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 180");
        }
        else if (RK_col.GetCellColor("-Y", 1, 2) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, 180");
            SolveScript.Add("Z, -1, 180");
        }
        else if (RK_col.GetCellColor("-Y", 2, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 180");
        }
        else
        {
            // nothing stored on List
        }
    }

    private void FindTopMiddleYellow_Side1()
    {
        if (RK_col.GetCellColor("+X", 0, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, 1, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 0, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, 1, 180");
            SolveScript.Add("Z, -1, 180");
        }
        else if (RK_col.GetCellColor("-X", 0, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, 1, -90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("-Z", 0, 1) == TargetColors[1])
        {
            SolveScript.Add("Z, -1, 90");
        }
    }

    private void FindTopMiddleYellow_Side2()
    {
        if (RK_col.GetCellColor("+Y", 1, 0) == TargetColors[1])
        {
            if (RK_col.GetCellColor("+Y", 0, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, -90");
            }
            else if (RK_col.GetCellColor("+Y", 1, 2) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 180");
            }
            else if (RK_col.GetCellColor("+Y", 2, 1) != TargetColors[1])
            {
                SolveScript.Add("Y, 1, 90");
            }
            else
            {
                return; // Nothing on list
            }
        }
        // Making Flower around negative Y (yellow) = move white middle part to next to center yellow
        // check if next to yellow is already white
        else if (RK_col.GetCellColor("+X", 2, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, 90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("+Z", 2, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, 180");
            SolveScript.Add("Z, -1, 90");
        }
        
        else if (RK_col.GetCellColor("-X", 2, 1) == TargetColors[1])
        {
            SolveScript.Add("Y, -1, -90");
            SolveScript.Add("Z, -1, 90");
        }
        else if (RK_col.GetCellColor("-Z", 2, 1) == TargetColors[1])
        {
            SolveScript.Add("Z, -1, 90");
        }
    }


}
