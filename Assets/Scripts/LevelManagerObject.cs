using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManagerObject : MonoBehaviour
{
    // -- Goals --
    // - Get a list of object types, their positions, rotations, scales and materials
    // - Think of a way to store this data as a struct of some sort or as byte data to be passed to the DLL
    // - Use DLL for reading and writing save file as well as custom logic for parsing the data to send between Unity and the DLL
    // - Create functions to be called by UI elements to execute saving and loading 

    protected static LevelManagerObject instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Ensures that there aren't multiple Singletons

        instance = this;
    }

    private const string DLL_NAME = "LevelManagerPlugin";

    // object properties struct
    [StructLayout(LayoutKind.Sequential)]
    private struct RawGameObject
    {
        public int typeID;
        public int matID;
        public float xPos, yPos, zPos;
        public float xRot, yRot, zRot, wRot;
        public float xScl, yScl, zScl;
    }

    // DLL imports
    [DllImport(DLL_NAME)]
    private static extern void SetSlotID(int slotID);

    [DllImport(DLL_NAME)]
    private static extern void AddToObjectList(RawGameObject obj);

    [DllImport(DLL_NAME)]
    private static extern RawGameObject GetObjectFromList(int index);

    [DllImport(DLL_NAME)]
    private static extern int GetObjectCount();
    [DllImport(DLL_NAME)]
    private static extern void ClearList();

    [DllImport(DLL_NAME)]
    private static extern bool SaveFile();

    [DllImport(DLL_NAME)]
    private static extern bool LoadFile();


    // references
    public GameObject PropsParentObj; // this is effectively a list of all objects in the scene
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject marksmanPrefab;
    public GameObject rockPrefab;
    public GameObject lightPrefab;
    public Material defaultMat;
    public Material red;
    public Material green;
    public Material blue;
    public GameObject loadOne;
    public GameObject loadTwo;
    public GameObject loadThree;

    // vars
    private List<GameObject> props = new List<GameObject>();
    private List<RawGameObject> rawProps = new List<RawGameObject>();


    private void Start()
    {
        loadOne.GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f);
        loadTwo.GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f);
        loadThree.GetComponent<Text>().color = new Color(1.0f, 0.0f, 0.0f);

        UpdateSlotOccupied();
    }

    public void SaveToSlot(int slot)
    {
        UpdatePropsList();
        PopulateRawPropsList();
        SendRawPropsToDLL();
        
        if (GetObjectCount() == rawProps.Count)
        {
            SetSlotID(slot);
            if (SaveFile())
            {
                Debug.Log("saved to slot " + slot);
                UpdateSlotOccupied();
                return;
            }
            Debug.Log("failed to save slot " + slot);
        }
    }

    public void LoadFromSlot(int slot)
    {
        SetSlotID(slot);
        ClearList();
        if (LoadFile())
        {
            int objCount = GetObjectCount();

            rawProps.Clear();

            for (int i = 0; i < objCount; i++)
            {
                rawProps.Add(GetObjectFromList(i));
            }

            if (rawProps.Count == objCount)
            {
                // Remove all children of PropsParentObj
                DestroyAllProps();

                // Clear props list
                props.Clear();

                // Create props from raw
                foreach (RawGameObject raw in rawProps)
                {
                    CreateFromRaw(raw);
                }

                Debug.Log("loaded from slot " + slot);
            }
        }
        else
        {
            Debug.Log("load failed");
        }
    }

    // TODO: change the way we get every prop from the scene. Parenting all props to an object is not ideal.
    // Maybe we use a label for this.
    private void UpdatePropsList()
    {
        props.Clear(); // make sure we have no duplicates
        foreach (Transform child in PropsParentObj.transform)
        {
            props.Add(child.gameObject);
        }
    }

    // TODO: make this more modular. Making a case for every type of object we have is not ideal.
    // Maybe we get asset UID's? Is that possible?
    private RawGameObject ConvertToRaw(GameObject obj)
    {
        RawGameObject rawObj = new RawGameObject { };
        string name = obj.name;
        string matName = obj.GetComponent<MeshRenderer>().material.name;
        Vector3 pos = obj.transform.position;
        Quaternion rot = obj.transform.rotation;
        Vector3 scl = obj.transform.localScale;

        // type to int
        int type;
        if (name.Contains("Cube"))
        {
            type = 1;
        }
        else if (name.Contains("Sphere"))
        {
            type = 2;
        }
        else if (name.Contains("Marksman"))
        {
            type = 3;
        }
        else if (name.Contains("Rock"))
        {
            type = 4;
        }
        else if (name.Contains("Light"))
        {
            type = 5;
        }
        else
        {
            type = 0;
        }
        rawObj.typeID = type;

        // mat to int
        int mat;
        if (matName.Contains("default"))
        {
            mat = 1;
        }
        else if (matName.Contains("red"))
        {
            mat = 2;
        }
        else if (matName.Contains("green"))
        {
            mat = 3;
        }
        else if (matName.Contains("blue"))
        {
            mat = 4;
        }
        else
        {
            mat = 0;
        }
        rawObj.matID = mat;

        // pos to float
        rawObj.xPos = pos.x;
        rawObj.yPos = pos.y;
        rawObj.zPos = pos.z;

        // rot to float
        rawObj.xRot = rot.x;
        rawObj.yRot = rot.y;
        rawObj.zRot = rot.z;
        rawObj.wRot = rot.w;

        // scl to float
        rawObj.xScl = scl.x;
        rawObj.yScl = scl.y;
        rawObj.zScl = scl.z;

        return rawObj;
    }

    private void PopulateRawPropsList()
    {
        rawProps.Clear();
        foreach (GameObject obj in props)
        {
            RawGameObject sanityCheck = ConvertToRaw(obj);

            if (sanityCheck.typeID == 0)
            {
                continue; // something went wrong. instead of dealing with it, skip it.
            }
            
            rawProps.Add(sanityCheck);
        }
    }

    private void SendRawPropsToDLL()
    {
        ClearList();
        foreach (RawGameObject obj in rawProps)
        {
            AddToObjectList(obj);
        }
    }

    private void DestroyAllProps()
    {
        foreach (Transform child in PropsParentObj.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // TODO: make this more modular, same as above. We can't make a case for every type of tracked object.
    // again, maybe figure out if we can get asset IDs.
    private void CreateFromRaw(RawGameObject obj)
    {
        // Convert to GameObject
        if (obj.typeID == 1) // cube
        {
            props.Add(Instantiate(cubePrefab));
        }
        else if (obj.typeID == 2) // sphere
        {
            props.Add(Instantiate(spherePrefab));
        }
        else if (obj.typeID == 3) // marksman
        {
            props.Add(Instantiate(marksmanPrefab));
        }
        else if (obj.typeID == 4) // rock
        {
            props.Add(Instantiate(rockPrefab));
        }
        else if (obj.typeID == 5) // light
        {
            props.Add(Instantiate(lightPrefab));
        }

        props[props.Count - 1].transform.parent = PropsParentObj.transform;

        props[props.Count - 1].transform.position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
        props[props.Count - 1].transform.rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, obj.wRot);
        props[props.Count - 1].transform.localScale = new Vector3(obj.xScl, obj.yScl, obj.zScl);

        // set material based on ID. if ID is 0, we don't set a material. This happens if obj is a light (typeID of 5)
        if (obj.matID == 1) // default
        {
            props[props.Count - 1].GetComponent<MeshRenderer>().material = defaultMat;
        }
        else if (obj.matID == 2) // red
        {
            props[props.Count - 1].GetComponent<MeshRenderer>().material = red;
            props[props.Count - 1].GetComponent<MeshRenderer>().material.color = new Color(props[props.Count - 1].GetComponent<MeshRenderer>().material.color.r,
                            props[props.Count - 1].GetComponent<MeshRenderer>().material.color.b, props[props.Count - 1].GetComponent<MeshRenderer>().material.color.g, 1.0f);
        }
        else if (obj.matID == 3) // green
        {
            props[props.Count - 1].GetComponent<MeshRenderer>().material = green;
            props[props.Count - 1].GetComponent<MeshRenderer>().material.color = new Color(props[props.Count - 1].GetComponent<MeshRenderer>().material.color.r,
                            props[props.Count - 1].GetComponent<MeshRenderer>().material.color.b, props[props.Count - 1].GetComponent<MeshRenderer>().material.color.g, 1.0f);
        }
        else if (obj.matID == 4) // blue
        {
            props[props.Count - 1].GetComponent<MeshRenderer>().material = blue;
            props[props.Count - 1].GetComponent<MeshRenderer>().material.color = new Color(props[props.Count - 1].GetComponent<MeshRenderer>().material.color.r,
                            props[props.Count - 1].GetComponent<MeshRenderer>().material.color.b, props[props.Count - 1].GetComponent<MeshRenderer>().material.color.g, 1.0f);
        }
    }

    private void UpdateSlotOccupied() // try to load file at each slot. if it is successful, slot is occupied.
    {
        SetSlotID(1);
        if (LoadFile())
        {
            loadOne.GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f);
        }
        SetSlotID(2);
        if (LoadFile())
        {
            loadTwo.GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f);
        }
        SetSlotID(3);
        if (LoadFile())
        {
            loadThree.GetComponent<Text>().color = new Color(0.0f, 1.0f, 0.0f);
        }
    }
}
