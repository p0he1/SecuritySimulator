using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public bool dialogueIsPlaying;
    private Story currentStory;

    public GameObject[] choices;
    public TextMeshProUGUI[] choicesText;
    public bool choisesActive;
    public GameObject joystick;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("it is more then once dialogue manager on the scene");
        }
        instance = this;
        dialogueText.text = "";
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !choisesActive && dialogueIsPlaying)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset InkJSON)
    {
        currentStory = new Story(InkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        joystick.SetActive(false);
        dialogueText.text = currentStory.Continue();
        foreach(GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        joystick.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    public void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            dialogueText.text = "";
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given then UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            choisesActive = true;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoise());
    }

    public IEnumerator SelectFirstChoise()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choisesActive = false;
    }
}