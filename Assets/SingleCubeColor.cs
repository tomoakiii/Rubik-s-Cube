using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StandardRK;


public class SingleCubeColor
{
    public Colors forward_X;
    public Colors forward_Y;
    public Colors forward_Z;
    public Colors backward_X;
    public Colors backward_Y;
    public Colors backward_Z;

    public string str_forward_X = "Err";
    public string str_forward_Y = "Err";
    public string str_forward_Z = "Err";
    public string str_backward_X = "Err";
    public string str_backward_Y = "Err";
    public string str_backward_Z = "Err";

    public SingleCubeColor()
    {
        forward_X = Colors.Green;
        forward_Y = Colors.White;
        forward_Z = Colors.Red;
        backward_X = Colors.Blue;
        backward_Y = Colors.Yellow;
        backward_Z = Colors.Orange;
    }
    
    public void Rotate(string xyz)
    {
        Colors tmp;
        string tmpstr;
        switch( xyz )
        {
            case "-X":
                tmp = forward_Z;
                forward_Z = backward_Y;
                backward_Y = backward_Z;
                backward_Z = forward_Y;
                forward_Y = tmp;
                tmpstr = str_forward_Z;
                str_forward_Z = str_backward_Y;
                str_backward_Y = str_backward_Z;
                str_backward_Z = str_forward_Y;
                str_forward_Y = tmpstr;
                break;
            case "+X":
                tmp = forward_Z;
                forward_Z = forward_Y;
                forward_Y = backward_Z;
                backward_Z = backward_Y;
                backward_Y = tmp;
                tmpstr = str_forward_Z;
                str_forward_Z = str_forward_Y;
                str_forward_Y = str_backward_Z;
                str_backward_Z = str_backward_Y;
                str_backward_Y = tmpstr;
                break;
            case "-Y":
                tmp = forward_X;
                forward_X = backward_Z;
                backward_Z = backward_X;
                backward_X = forward_Z;
                forward_Z = tmp;

                tmpstr = str_forward_X;
                str_forward_X = str_backward_Z;
                str_backward_Z = str_backward_X;
                str_backward_X = str_forward_Z;
                str_forward_Z = tmpstr;
                break;
            case "+Y":
                tmp = forward_X;
                forward_X = forward_Z;
                forward_Z = backward_X;
                backward_X = backward_Z;
                backward_Z = tmp;

                tmpstr = str_forward_X;
                str_forward_X = str_forward_Z;
                str_forward_Z = str_backward_X;
                str_backward_X = str_backward_Z;
                str_backward_Z = tmpstr;
                break;
            case "+Z":
                tmp = forward_X;
                forward_X = backward_Y;
                backward_Y = backward_X;
                backward_X = forward_Y;
                forward_Y = tmp;

                tmpstr = str_forward_X;
                str_forward_X = str_backward_Y;
                str_backward_Y = str_backward_X;
                str_backward_X = str_forward_Y;
                str_forward_Y = tmpstr;
                break;
            case "-Z":
                tmp = forward_X;
                forward_X = forward_Y;
                forward_Y = backward_X;
                backward_X = backward_Y;
                backward_Y = tmp;

                tmpstr = str_forward_X;
                str_forward_X = str_forward_Y;
                str_forward_Y = str_backward_X;
                str_backward_X = str_backward_Y;
                str_backward_Y = tmpstr;
                break;
            default:
                Debug.Log("EXCEPTION: ERORR SINGLE CUBE ROTATION");
                break;
        }
    }
}
