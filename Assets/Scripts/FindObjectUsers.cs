using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class FindObjectUsers : EditorWindow
{
    private GameObject targetObject;
    private List<GameObject> users = new List<GameObject>();

    [MenuItem("Tools/Find Object Users")]
    public static void ShowWindow()
    {
        GetWindow<FindObjectUsers>("Object Users");
    }

    private void OnGUI()
    {
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);

        if (GUILayout.Button("Find Users"))
        {
            users.Clear();
            if (targetObject != null)
            {
                FindReferences(targetObject);
            }
        }

        EditorGUILayout.LabelField("Objects using the target:");
        foreach (GameObject user in users)
        {
            EditorGUILayout.ObjectField("", user, typeof(GameObject), true);
        }
    }

    private void FindReferences(GameObject target)
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null) continue;

                SerializedObject serializedObject = new SerializedObject(component);
                SerializedProperty iterator = serializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyType == SerializedPropertyType.ObjectReference && iterator.objectReferenceValue == target)
                    {
                        users.Add(obj);
                        break; // Found a reference in this object, move to the next
                    }
                }
            }
        }
    }
}