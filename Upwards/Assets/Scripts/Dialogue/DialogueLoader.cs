using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset[] keys;    //All keys are dragged into here for now. LoadAll would be nice!
    public List<Dialogue> dialogues;

    private static bool exists;

    public DialogueTrigger[] allTriggers;

    void Awake()
    {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        ParseKeys();
        SetAllTriggers();
    }

    public void SetAllTriggers()
    {
        Debug.Log("Setting all triggers");
        allTriggers = FindObjectsOfType<DialogueTrigger>();
        foreach (DialogueTrigger trigger in allTriggers)
        {
            
            trigger.SetTrigger();
        }
    }

    public void ParseKeys()
    {
        dialogues = new List<Dialogue>();

        keys = Resources.LoadAll<TextAsset>("Keys");

        for (int i = 0; i < keys.Length; i++)
        {
            string[] dummy = keys[i].text.Split("\n"[0]);
            //THIS IS THE PROCESSING OF THE CHUNK.
            //Index 0 will always be the key. 
            //Index 2 will always be the start of content.

            dummy[0] = dummy[0].Trim();
            int childDivider = 0;
            for (int y =0; y < dummy.Length; y++)
            {
                Debug.Log(dummy[y]);
                if (dummy[y].Contains("[[")){
                    childDivider = y;
                    break;
                }
            }

            //We know the length of our dialogue must be the divider, minus 2 spaces, minus key.
            string[] dummySentences = new string[childDivider - 3];

            //We also know sentences are index 2, up to the index childDivider - 2.
            for (int q = 2; q < (childDivider - 1); q++)
            {
                //This can be passed as is to the constructor. No further editing needed.
                dummySentences[q - 2] = dummy[q];
            }

            //Creates a combined string for further manipulation.
            string childCombo = "";
            for (int m = childDivider; m < dummy.Length; m++)
            {
                childCombo += dummy[m];
            }


            //CLEANING UP THE CHOICES / CHILDREN
            //Splits the dummy choices by character, then cleans them up and adds them back into the correct
            string[] messyDummyChoices = childCombo.Split("]][["[0]);

            List<string> refinedDummy = new List<string>();
            
            for (int t = 0; t < messyDummyChoices.Length; t++)
            {
                if (messyDummyChoices[t] != "\n" && messyDummyChoices[t] != "")
                {
                    messyDummyChoices[t] = messyDummyChoices[t].Replace("[[", "");
                    refinedDummy.Add(messyDummyChoices[t]);
                }
            }

            //Now we should be able to go back through and reprocess.
            string[] cleanDummyChoices = new string[refinedDummy.Count];
            for (int p = 0; p < cleanDummyChoices.Length; p++)
            {
                cleanDummyChoices[p] = refinedDummy[p];
            }

            string[] dummyChoices = new string[cleanDummyChoices.Length];
            string[] dummyChildren = new string[cleanDummyChoices.Length];
            ///Ooooookay, now we need to split clean dummy choices into choices, and children.
            for (int z = 0; z < cleanDummyChoices.Length; z++)
            {
                string[] splitOptions = cleanDummyChoices[z].Split("| "[0]);
                //The first will be the choice, the second will be the child.
                dummyChoices[z] = splitOptions[0];
                dummyChildren[z] = splitOptions[1];
                dummyChildren[z] = dummyChildren[z].Remove(0, 1);    //Remove the extra space they start with
            }

            Dialogue newLog = new Dialogue(dummy[0], dummy[1], dummyChildren, dummy[3], dummySentences, dummyChoices);
            dialogues.Add(newLog);
            
        }
    }

    //Sets a key based on the dialogue that was passed in.
    public Dialogue SetDialogue(string _key)
    {
        Debug.Log("Key to check: " + _key);
        for (int i = 0; i < dialogues.Count; i++)
        {
            Debug.Log("Stored key: " + dialogues[i].dialogueKey);
            if (dialogues[i].dialogueKey == _key)
            {
                Debug.Log("found it");
                return dialogues[i];
            }
        }
        return null;
    }
}