using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    private string textKey = "Hello, world";

    private string dialogueText = "This is dialogue text";

    private int choiceCount = 0;

    private bool showChildren;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Beginning the dialogue stuff", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Make new"))
        {

        }

        if (GUILayout.Button("Load existing"))
        {

        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        textKey = EditorGUILayout.TextField("Dialogue Key", textKey);

        EditorGUILayout.Space();
        dialogueText = EditorGUILayout.TextField("Enter sentences", dialogueText, GUILayout.Height(100));

        EditorGUILayout.Space();

        choiceCount = EditorGUILayout.IntField("Choices:", choiceCount);

        if (choiceCount == 0)
        {
            showChildren = EditorGUILayout.Toggle("Show Button", showChildren);
            if (showChildren)
            {
                GUILayout.Label("It has children!");
            }
            else
            {
                GUILayout.Label("No Children");
            }
        }

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        for (int i = 0; i < choiceCount; i++)
        {
            EditorGUILayout.TextField("Choice " + i, GUILayout.Height(80));
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        
        GUILayout.BeginHorizontal();
        for (int i = 0; i < choiceCount; i++)
        {
            EditorGUILayout.TextField("Choice key");
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        for (int i = 0; i < choiceCount; i++)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Stat");
            EditorGUILayout.IntField(0);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Save"))
        {
            GUILayout.Label("Saved!", EditorStyles.boldLabel);
        }
    }
}
