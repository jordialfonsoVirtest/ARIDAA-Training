using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections.Generic;
// System.IO is no longer strictly needed if only using TextAsset assigned via Inspector.
// using System.IO;

public class LessonRenderer : MonoBehaviour
{
    [Tooltip("Drag your 'Lesson' Prefab here from the Project window.")]
    public GameObject lessonPrefab;

    [Tooltip("Drag the JSON text file (e.g., lessons.json) directly here from your Project window.")]
    public TextAsset jsonInputFile; // Changed from string fileName to TextAsset

    private int instanceCount = 0; // Added to keep track of instance numbers

    void Start()
    {
        LoadLessonsFromJson();
    }

    void LoadLessonsFromJson()
    {
        if (jsonInputFile == null)
        {
            Debug.LogError("JSON Input File is not assigned! Please drag your JSON file into the 'Json Input File' slot in the Inspector.");
            return;
        }

        string jsonString = jsonInputFile.text; // Get the text content directly from the TextAsset

        // Deserialize the JSON into a list of LessonData objects
        // We still need the wrapper because JsonUtility doesn't directly support root arrays.
        LessonDataWrapper lessonDataWrapper = JsonUtility.FromJson<LessonDataWrapper>("{\"lessons\":" + jsonString + "}");

        if (lessonDataWrapper == null || lessonDataWrapper.lessons == null || lessonDataWrapper.lessons.Count == 0)
        {
            Debug.LogError("Failed to parse JSON or no lessons found. Check your JSON format and content.");
            return;
        }

        // Reset the counter before instantiating
        instanceCount = 0;

        foreach (LessonData lesson in lessonDataWrapper.lessons)
        {
            instanceCount++; // Increment for each new instance
            InstantiateLessonPrefab(lesson.title, lesson.text);
        }
    }

    void InstantiateLessonPrefab(string titleText, string bodyText)
    {
        if (lessonPrefab == null)
        {
            Debug.LogError("Lesson Prefab is not assigned! Please assign the 'Lesson' Prefab in the Inspector.");
            return;
        }

        // Instantiate the prefab
        GameObject newLesson = Instantiate(lessonPrefab, transform); // 'transform' makes it a child of this GameObject

        // Set the name of the instantiated GameObject
        newLesson.name = $"{instanceCount} - {titleText}"; // This is the new line

        // Find the TextMeshPro components within the instantiated prefab
        TMP_Text[] tmpTexts = newLesson.GetComponentsInChildren<TMP_Text>();

        if (tmpTexts.Length >= 2)
        {
            // If you named your TMP fields "Title" and "Text" as per your image:
            TMP_Text titleField = newLesson.transform.Find("Title")?.GetComponent<TMP_Text>();
            TMP_Text textField = newLesson.transform.Find("Text")?.GetComponent<TMP_Text>();

            if (titleField != null)
            {
                titleField.text = titleText;
            }
            else
            {
                Debug.LogWarning($"Could not find 'Title' TextMeshPro component in instantiated prefab '{newLesson.name}'.");
            }

            if (textField != null)
            {
                textField.text = bodyText;
            }
            else
            {
                Debug.LogWarning($"Could not find 'Text' TextMeshPro component in instantiated prefab '{newLesson.name}'.");
            }
        }
        else
        {
            Debug.LogWarning($"Not enough TextMeshPro components found in the '{newLesson.name}' prefab. Expected at least two.");
        }
    }
}

// These classes are used to help Unity's JsonUtility deserialize the JSON.
// JsonUtility requires the root object to be a class.
[System.Serializable]
public class LessonData
{
    public string title;
    public string text;
}

[System.Serializable]
public class LessonDataWrapper
{
    public List<LessonData> lessons;
}