using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StandardRK;

public class RubiksCubeColorMap
{

    Colors[,] XPSideColor = new Colors[3,3];
    Colors[,] XNSideColor = new Colors[3,3];
    Colors[,] YPSideColor = new Colors[3,3];
    Colors[,] YNSideColor = new Colors[3,3];
    Colors[,] ZPSideColor = new Colors[3,3];
    Colors[,] ZNSideColor = new Colors[3,3];

    // Start is called before the first frame update
    public RubiksCubeColorMap()
    {
        for (int Y = 0; Y < 3; Y++)
        {
            for (int X = 0; X < 3; X++)
            {
                XPSideColor[Y, X] = Colors.Green;
                YPSideColor[Y, X] = Colors.White;
                ZPSideColor[Y, X] = Colors.Red;
                XNSideColor[Y, X] = Colors.Blue;
                YNSideColor[Y, X] = Colors.Yellow;
                ZNSideColor[Y, X] = Colors.Orange;
            }
        }
    }

    public void  SetRK(GameObject[,,] RK)
    {
        // Positive X
        int X, Y, Z;
        X = 2;
        for (Y = 0; Y < 3; Y++)
        {
            for (Z = 0; Z < 3; Z++)
            {
                XPSideColor[2-Y, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_X;
            }
        }

        // Negative X
        X = 0;
        for (Y = 0; Y < 3; Y++)
        {
            for (Z = 0; Z < 3; Z++)
            {
                XNSideColor[2-Y, 2-Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_X;
            }
        }

        // Positive Y
        Y = 2;
        for (X = 0; X < 3; X++)
        {
            for (Z = 0; Z < 3; Z++)
            {
                YPSideColor[X, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_Y;
            }
        }

        // Negative Y
        Y = 0;
        for (X = 0; X < 3; X++)
        {
            for (Z = 0; Z < 3; Z++)
            {
                YNSideColor[2-X, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_Y;
            }
        }

        // Positive Z
        Z = 2;
        for (X = 0; X < 3; X++)
        {
            for (Y = 0; Y < 3; Y++)
            {
                ZPSideColor[2-Y, 2-X] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_Z;
            }
        }

        // Negative Z
        Z = 0;
        for (X = 0; X < 3; X++)
        {
            for (Y = 0; Y < 3; Y++)
            {
                ZNSideColor[2-Y, X] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_Z;
            }
        }
    }

    public Colors[,] GetSideColor(string xyz)
    {
        switch( xyz )
        {
            case "-X":
                return XNSideColor;
            case "+X":
                return XPSideColor;
                
            case "-Y":
                return YNSideColor;
                
            case "+Y":
                return YPSideColor;
                
            case "-Z":
                return ZNSideColor;
                
            case "+Z":
                return ZPSideColor;
                
            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                return null;
                
        }
    } 

    public Colors GetCellColor(string xyz, int ind1, int ind2)
    {
        switch( xyz )
        {
            case "-X":
                return XNSideColor[ind1, ind2];
                
            case "+X":
                return XPSideColor[ind1, ind2];
                
            case "-Y":
                return YNSideColor[ind1, ind2];
                
            case "+Y":
                return YPSideColor[ind1, ind2];
                
            case "-Z":
                return ZNSideColor[ind1, ind2];
                
            case "+Z":
                return ZPSideColor[ind1, ind2];

            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                return Colors.ErrorCol;
        }
    }

    public string GetCellColorStr(string xyz, int ind1, int ind2)
    {
        Colors cout;
        switch( xyz )
        {
            case "-X":
                cout = XNSideColor[ind1, ind2];
                break;
            case "+X":
                cout = XPSideColor[ind1, ind2];
                break;
            case "-Y":
                cout = YNSideColor[ind1, ind2];
                break;
            case "+Y":
                cout = YPSideColor[ind1, ind2];
                break;
            case "-Z":
                cout = ZNSideColor[ind1, ind2];
                break;
            case "+Z":
                cout = ZPSideColor[ind1, ind2];
                break;
            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                cout = Colors.ErrorCol;
                break;
        }

        switch (cout)
        {
            case (Colors.Red):
                return "Red";

            case (Colors.Green):
                return "Green";

            case (Colors.Blue):
                return "Blue";

            case (Colors.Yellow):
                return "Yellow";

            case (Colors.White):
                return "White";

            case (Colors.Orange):
                return "Orange";

            case (Colors.Black):
                return "Black";

            default:
                return "ErrorCol";
            
        }
    }

}
