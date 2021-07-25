/**************************************************************************
* Copyright (C) echoAR, Inc. 2018-2020.                                   *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echoar.xyz/terms, or another agreement                      *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CustomBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Entry entry;

    //bad scene objects
    private const string coke = "209d655c-184a-44f7-8261-2701775d294e"; //0.03
    private const string deadfish = "0f9eb13c-c0ad-409f-8604-eab5aa962f5e"; //0.05
    private const string deadsquid = "bcc3a82d-5af8-4c1a-8195-829b85474379"; //0.03
    private const string bottles = "e1b6b378-081f-401f-8c98-d5016e29d8f3"; //0.1
    private const string chip = "e9d44806-7ffe-493e-ba62-c66ca0910b61"; //0.05

    //good scene objects
    private const string dolphin = "30867461-de19-4deb-83b4-d98d1dd50b0b"; //0.12
    private const string jellyfish = "c8cb5d1c-5bda-45f2-b0bc-7fd98727d291"; //0.002
    private const string coral = "c8606030-87fe-40b1-9ff1-c3ac501b61e1"; //1
    private const string fish = "8f686b2e-a9ca-418a-bbf8-63d38cd3d302"; //0.0004

    private const string nemo = "20798e5e-5d2f-41b5-858f-814795b7f8a6";  //0.06

    string[] badObjects = new string[] { coke, deadfish, deadsquid, bottles, chip };
    string[] badObjectsValue = new string[] { "0.03", "0.05", "0.03", "0.1", "0.05" };
    string[] goodObjects = new string[] { dolphin, jellyfish, coral, fish, nemo };
    string[] goodObjectsValue = new string[] { "0.12", "0.002", "1", "0.0004", "0.006" };

    /// <summary>
    /// EXAMPLE BEHAVIOUR
    /// Queries the database and names the object based on the result.
    /// </summary>

    // Set walls parameters
    string[] wallNames = { "wallNearRight","wallNearLeft",
                           "WallNear", "WallFar",
                           "wallRight", "wallLeft",
                           "wallUp", "wallDown"
                         };
    Vector3[] wallPositions = { new Vector3(35.5f, 39, 0), new Vector3(-35.5f, 39, 0),
                                new Vector3(0, 71.5f, 0f), new Vector3(0, 39, 89.5f),
                                new Vector3(52f, 39, 45), new Vector3(-52f, 39, 45),
                                new Vector3(0, 79, 45), new Vector3(0, -1, 45),
                              };
    Vector3[] wallScales = { new Vector3(34, 80, 1), new Vector3(34, 80, 1),
                             new Vector3(38, 15, 1), new Vector3(104, 80, 1),
                             new Vector3(1, 80, 90), new Vector3(1, 80, 90),
                             new Vector3(105, 1, 90), new Vector3(105, 1, 90),
                           };
    Vector3[] wallOffset = { -Vector3.forward, -Vector3.forward,
                             -Vector3.forward, Vector3.forward,
                             Vector3.right, -Vector3.right,
                             Vector3.up, -Vector3.up,
                           };


    // Use this for initialization
    void Start()
    {
        // Add RemoteTransformations script to object and set its entry
        this.gameObject.AddComponent<RemoteTransformations>().entry = entry;

        // Check if this is the door game object
        if (this.gameObject.name.Equals("Door.glb"))
        {
            // Get materials
            Material matWater = (Material)Resources.Load("Materials/Stylize Water Diffuse");
            Material matInvisible = (Material)Resources.Load("Invisible");
            Material[] wallMaterials = { matWater, matWater,
                                         matWater, matWater,
                                         matWater, matWater,
                                         matWater, matWater };
            // Create walls
            Debug.Log("Creating walls...");
            for (int i = 0; i < wallNames.Length; i++)
            {
                // Create wall
                GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.name = wallNames[i];
                wall.transform.parent = this.transform;
                wall.transform.position = wallPositions[i];
                wall.transform.localScale = wallScales[i];
                if (wallMaterials[i]) wall.GetComponent<Renderer>().material = wallMaterials[i];
            }

            // Create invisible walls
            Debug.Log("Creating invisible walls...");
            for (int i = 0; i < wallNames.Length; i++)
            {
                // Create wall
                GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.name = wallNames[i] + "_Invisible";
                wall.transform.parent = this.transform;
                wall.transform.position = wallPositions[i] + wallOffset[i];
                wall.transform.localScale = wallScales[i] + Vector3.one;
                wall.GetComponent<Renderer>().material = matInvisible;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Bad effect button
    public void EntryBad()
    {
        // Load bad environment scene
        // SceneManager.LoadScene(1);
        // var echoObject = GameObject.Find("echoAR").GetComponent<echoAR>();
        for (int i = 0; i < badObjects.Length; i++)
        {
            StartCoroutine(GameObject.Find("echoAR").GetComponent<echoAR>().UpdateEntryData(badObjects[i], "scale", badObjectsValue[i]));
            StartCoroutine(GameObject.Find("echoAR").GetComponent<echoAR>().UpdateEntryData(goodObjects[i], "scale", "0"));
        }

    }

    // Good effect button
    public void EntryGood()
    {
        // Load good environment scene
        // SceneManager.LoadScene(2);
        // var echoObject = GameObject.Find("echoAR").GetComponent<echoAR>();
        for (int i = 0; i < goodObjects.Length; i++)
        {
            StartCoroutine(GameObject.Find("echoAR").GetComponent<echoAR>().UpdateEntryData(goodObjects[i], "scale", goodObjectsValue[i]));
            StartCoroutine(GameObject.Find("echoAR").GetComponent<echoAR>().UpdateEntryData(badObjects[i], "scale", "0"));
        }
    }
}