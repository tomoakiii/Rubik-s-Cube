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
    public GameObject[] cubes;

    private bool WaitRotation;
    private GameObject MainCamera;
    private int RandomIndex = -1;
    private GameObject[,,] RK;
    private RubiksCubeColorMap RK_col;
    private List<string> GameLog = new();
    private List<string> SolveScript = new();
    public GameObject LogPanel;
    private LogScript logscript;
    private DebugScript debugscript;
    private InputScript inputscript;
    private int AutoModeStage = 0, AutoModeStopStage = 0;
    private string DebugKeyword = "";

    System.Random r1 = new();
    // Start is called before the first frame update
    private enum AutoMode
    {
        None,
        AutoResolveMode,
        AutoSequenceMode,
        Random10Mode,
    }
    AutoMode isAutoMode = AutoMode.None;

    void Start()
    {
        logscript = LogPanel.GetComponent<LogScript>();
        debugscript = LogPanel.GetComponent<DebugScript>();
        inputscript = LogPanel.GetComponent<InputScript>();
        isAutoMode = AutoMode.None;
        DebugKeyword = "";
        try{
            RK = new GameObject[3,3,3];
            WaitRotation = false;
            MainCamera = Camera.main.gameObject;
            int ind = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        cubes[ind].transform.localPosition = new Vector3(x, y, z);
                        cubes[ind].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        RK[x+1,y+1,z+1] = cubes[ind];
                        cubes[ind].GetComponent<CubeRotation>().Initialize();
                        ind++;
                    }
                }
            }
            RK_col = new RubiksCubeColorMap(RK);
            ClearLog();
            debug();
        }   
        catch (System.Exception e)
        {
            EmergencyStop("System mulfunction at main");
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!WaitRotation)
        {
            if (isAutoMode == AutoMode.AutoSequenceMode
             || isAutoMode == AutoMode.AutoResolveMode)
            {
                if (SolveScript.Any())
                {
                    RotateCube_Command(SolveScript[0]);
                    SolveScript.RemoveAt(0);
                }
                else if (isAutoMode == AutoMode.AutoSequenceMode)
                {
                    if (AutoModeStage == 0)
                    {
                        isAutoMode = AutoMode.None;
                        AutoModeStopStage = 0;
                    }
                    else if (AutoModeStage == 1 && AutoModeStopStage >= 1)
                    {
                        Auto1CallBack();
                    }
                    else if (AutoModeStage == 2 && AutoModeStopStage >= 2)
                    {
                        Auto2CallBack();
                    }
                    else if (AutoModeStage == 3 && AutoModeStopStage >= 3)
                    {
                        Auto3CallBack();
                    }
                    else if (AutoModeStage == 4 && AutoModeStopStage >= 4)
                    {
                        Auto4CallBack();
                    }
                    else if (AutoModeStage == 5 && AutoModeStopStage >= 5)
                    {
                        Auto5CallBack();
                    }
                    else if (AutoModeStage == 6 && AutoModeStopStage >= 6)
                    {
                        Auto6CallBack();
                    }
                    else if (AutoModeStage == 7 && AutoModeStopStage >= 7)
                    {
                        Auto7CallBack();
                    }
                    else if (AutoModeStage == 8 && AutoModeStopStage >= 8)
                    {
                        Auto8CallBack();
                    }
                    else if (AutoModeStage == 9 && AutoModeStopStage >= 9)
                    {
                        Auto9CallBack();
                    }
                    else
                    {
                        // Normally stopped here
                    }
                }
                else
                {
                    isAutoMode = AutoMode.None;
                    AutoModeStopStage = 0;
                }
            }
            else if (isAutoMode == AutoMode.Random10Mode)
            {
                if (RandomIndex > 0)
                {
                    int randNext = r1.Next(0,3);
                    RotateCube( // randomize rotate for x, y or z + 90 or -90 deg
                        ((randNext==1)?Vector3.right:((randNext==2)?Vector3.up:Vector3.forward)),
                        r1.Next(-1,2),  // either of -1, 0, 1
                        90 * r1.Next(1,3) ); // either of 90, 180
                    RandomIndex--;
                }
                else
                {
                    isAutoMode = AutoMode.None;
                }
            }
            else if (Input.GetMouseButtonDown(0)) 
            {
                LeftButtonDownFcn();
            }
            else if (Input.GetMouseButtonUp(0) && clickedGameObject1 != null)
            {
                LeftButtonUpFcn();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightButtonDownFcn();
            }
            else if (Input.GetMouseButtonUp(1) && clickedGameObject2 != null)
            {
                RightButtonUpFcn();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                debug();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                EmergencyStop("Stop by Return key");
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                AutoModeStage = 1;
                Auto1CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                AutoModeStage = 2;
                Auto2CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                AutoModeStage = 3;
                Auto3CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                AutoModeStage = 4;
                Auto4CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                AutoModeStage = 5;
                Auto5CallBack();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                AutoModeStage = 6;
                Auto6CallBack();
            }
        }
        else
        {
            bool tmp_isRot = false;
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (sc.isRotating)
                {
                    tmp_isRot = true; // still rotating
                    break;
                }
            }  
            WaitRotation = tmp_isRot;
            if (!tmp_isRot)
            {
                // Finish Rotation
                SetRK();
                debug();
            }
        }
    }

    private void EmergencyStop(string inputstr)
    {
        SolveScript.Clear();
        isAutoMode = AutoMode.None;
        AutoModeStage = 0;
        Debug.Log(inputstr);
        //EditorUtility.DisplayDialog("Error", inputstr, "OK");
    }

    private void RotateCube_Command(string command)
    {
        string[] currentOperation = command.Split(',');
        
        if (currentOperation.Length == 3)
        {
            RotateCube(XYZToVector3(currentOperation[0]),
                Convert.ToInt32(currentOperation[1]), 
                Convert.ToInt32(currentOperation[2]));
        }
        else if (currentOperation.Length == 2)
        {
            AllRotation(XYZToVector3(currentOperation[0]),
                Convert.ToInt32(currentOperation[1]));
        }
        else
        {
            EmergencyStop("Command cannot be converted to format");
        }
    }

    private void RotateCube(Vector3 rotAxis, int linenum, float rotangle)
    {
        Vector3 tmprot = rotangle * rotAxis;
        Quaternion quat = Quaternion.Euler(tmprot.x, tmprot.y, tmprot.z); // x axis 90 deg turn is defined as (90, 0, 0) in Euler Angles
        if (linenum == 0) /* Prohibit center cell rotation to keep uniqueness */
        {
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (Mathf.RoundToInt(Vector3.Dot(cube.transform.position, rotAxis)) == 0) // when rotAxis=(1,0,0), find (0,*,*)
                {
                    sc.RotationStart(Vector3.zero, rotAxis, rotangle, 
                        V_RoundToInt(quat * cube.transform.position), 
                        Q_RoundToInt(quat * cube.transform.rotation) );
                }
            }
        }
        else
        {
            foreach (GameObject cube in cubes)
            {
                CubeRotation sc = cube.GetComponent<CubeRotation>();
                if (Mathf.RoundToInt(Vector3.Dot(cube.transform.position, rotAxis * linenum)/linenum/linenum) == 1)
                {
                    sc.RotationStart(linenum * rotAxis, rotAxis, rotangle, 
                        V_RoundToInt(quat * cube.transform.position), 
                        Q_RoundToInt(quat * cube.transform.rotation) );
                }
            }
        }
        WaitRotation = true;        
        MakeLog(rotAxis, linenum, rotangle);
    }


    private void AllRotation(Vector3 rotAxis, float rotangle)
    {
        Vector3 tmprot = rotangle * rotAxis;
        Quaternion quat = Quaternion.Euler(tmprot.x, tmprot.y, tmprot.z); // x axis 90 deg turn is defined as (90, 0, 0) in Euler Angles
        foreach (GameObject cube in cubes)
        {
            CubeRotation sc = cube.GetComponent<CubeRotation>();
            sc.RotationStart(
                new Vector3(rotAxis.x * cube.transform.position.x, rotAxis.y * cube.transform.position.y, 
                rotAxis.z * cube.transform.position.z), 
                rotAxis, rotangle, 
                V_RoundToInt(quat * cube.transform.position), 
                Q_RoundToInt(quat * cube.transform.rotation) );
        }
        WaitRotation = true;
        MakeLog_AllRot(rotAxis, rotangle);
    }

    private void ClearLog()
    {
        GameLog.Clear();
        inputscript.Clear();
        debugscript.Clear();
        logscript.Clear();
        
    }

    private void MakeLog(Vector3 rotAxis, int linenum, float rotangle)
    {
        GameLog.Add(Vector3ToXYZ(rotAxis)  + "," + linenum.ToString("D1") + "," + rotangle.ToString("F0"));
        logscript.MakeLog(GameLog);
    }

    private void MakeLog_AllRot(Vector3 rotAxis, float rotangle)
    {
        GameLog.Add(Vector3ToXYZ(rotAxis)  + "," + rotangle.ToString("F0"));
        logscript.MakeLog(GameLog);
    }


    private Vector3 V_RoundToInt(Vector3 inv)
    {
        return new Vector3(Mathf.RoundToInt(inv.x), Mathf.RoundToInt(inv.y), Mathf.RoundToInt(inv.z));
    }

    private Quaternion Q_RoundToInt(Quaternion inq)
    {
        return Quaternion.Euler(
                Mathf.RoundToInt(inq.eulerAngles.x),
                Mathf.RoundToInt(inq.eulerAngles.y),
                Mathf.RoundToInt(inq.eulerAngles.z));
    }


    private void SetRK()
    {
        foreach (GameObject cube in cubes)
        {
            RK[Mathf.RoundToInt(cube.transform.position.x)+1,
                Mathf.RoundToInt(cube.transform.position.y)+1,
                Mathf.RoundToInt(cube.transform.position.z)+1] = cube;
        }
        RK_col.SetRK(RK);
    }



    private string Vector3ToXYZ(Vector3 vc)
    {
        return ((vc.x > 0.8)?"X":((vc.y > 0.8)?"Y":"Z"));
    }

    private Vector3 XYZToVector3(string xyz)
    {
        return (xyz.Equals("X")?Vector3.right:(xyz.Equals("Y")?Vector3.up:Vector3.forward));
    }

    void debug()
    {
        string output = "";
        string[] xyz = new string[]{"+X", "-X", "+Y", "-Y", "+Z", "-Z"};

        // output = output + DebugKeyword + "\n";
        foreach (string xyz_ind in xyz)
        {
            output = output + "Plane " + xyz_ind + ": ";
            for (int y = 0 ; y < 3 ; y ++)
            {
                for (int x = 0 ; x < 3 ; x ++)
                {
                    output = output + RK_col.GetCellColorStr(xyz_ind, y, x) + ", ";
                }
            }
            output = output + "\n";
        }

        /*
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    string tmpout = "(" + x.ToString("D0") + "," + y.ToString("D0") + "," + z.ToString("D0") + "):" + RK[x,y,z].name;
                    output = output + tmpout + " | ";
                }
            }
        }
        */

        debugscript.set(output);

    }

}

