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
   
    private void Solve_Operation_right()
    {
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, 1, -90");
    }
    private void Solve_Operation_left()
    {
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
    }

    private void Solve_OperationA2()
    {
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
    }
    private void Solve_OperationA()
    {
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
    }

    private void Solve_OperationB2()
    {
        SolveScript.Add("X, 1, 90");
        Solve_OperationA2();
        SolveScript.Add("X, 1, -90");
    }

    private void Solve_OperationB()
    {
        SolveScript.Add("X, 1, -90");
        Solve_OperationA();
        SolveScript.Add("X, 1, 90");
    }



    private void Solve_OperationC1()
    {
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 180");
        SolveScript.Add("Z, -1, -90");
    }

    private void Solve_OperationC2()
    {
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, 180");
        SolveScript.Add("Z, 1, -90");
    }

    private void Solve_OperationD()
    {
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Z, 1, -90");
    }

    private void Solve_OperationE()
    {
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Z, -1, 90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, -90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, -1, -90");
        SolveScript.Add("Y, 1, 90");
        SolveScript.Add("Z, 1, 90");
        SolveScript.Add("Y, 1, -90");
        SolveScript.Add("Z, 1, -90");
    }
}
