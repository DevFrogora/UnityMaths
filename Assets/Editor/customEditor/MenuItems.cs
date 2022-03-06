using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Tools/ObjectInfo %g")]
    private static bool  ObjectInfo()
    {
        if (Selection.activeGameObject.gameObject.GetComponent<LevelScript>())
        {
            Debug.Log("<color=Red>Name :</color> <color=Green>" + Selection.activeObject.name + "</color>");
            Debug.Log("<color=Red>Type :</color> <color=Green>" + Selection.activeObject.GetType() + "</color>");
            Debug.Log("<color=Red>Total Child :</color> <color=Green>" + Selection.activeGameObject.transform.childCount + "</color>");
            Debug.Log("<color=Red>Total Child :</color> <color=Green>" + Selection.activeGameObject.transform.localPosition + "</color>");
            Debug.Log("<color=Red>Total Child :</color> <color=Green>" + Selection.activeGameObject.transform.position + "</color>");
            return true;
        }
        else {
            return false;
        }

    }

    // Add a new menu item under an existing menu

    [MenuItem("Window/New Option")]
    private static void NewMenuOptionInWindow()
    {
        
    }

    // Add a menu item with multiple levels of nesting

    [MenuItem("Tools/SubMenu/Option")]
    private static void NewNestedOptionInTools()
    {
    }



    /// Special Path 
    /// 

    [MenuItem("Assets/Load Additive Scene")]
    private static bool LoadAdditiveScene()
    {

        var selected = Selection.activeObject;

        if (selected.GetType().Equals(typeof(UnityEditor.SceneAsset)))
        {
            EditorApplication.OpenSceneAdditive(AssetDatabase.GetAssetPath(selected));
            Debug.Log(selected.GetType());
            return true;

        }
        else
        {
            return false;
        }

    }


    // Adding a new menu item under Assets/Create

    [MenuItem("Assets/Create/Add Configuration")]
    private static void AddConfig()
    {
        // Create and add a new ScriptableObject for storing configuration
    }

    // Add a new menu item that is accessed by right-clicking inside the RigidBody component

    [MenuItem("CONTEXT/Rigidbody/SetGravity")]
    private static bool SetGravity(MenuCommand menuCommand)
    {
        if (Selection.activeGameObject.gameObject.GetComponent<LevelScript>())
        {
            var rigid = menuCommand.context as Rigidbody;
            rigid.useGravity = false;
            Debug.Log("Gravity set");
            return true;
        }
        else
        {
            Debug.Log("Its not LevelScript");
            return false;
        }
    }




}