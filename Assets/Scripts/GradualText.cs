using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;

public class GradualText : MonoBehaviour
{
    private TMP_Text tmpText;
    private string text;
    private float timer;
    public float letterTime = 0.1f;
    public float displayTime = 2f;

    private enum State
    {
        adding, displaying, removing, waiting
    };

    State state = State.waiting;
    int currentLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            switch (state)
            {
                case State.adding:
                    currentLength++;
                    timer = letterTime;
                    tmpText.text = text.Substring(0, currentLength);
                    if (currentLength >= text.Length)
                    {
                        state = State.displaying;
                    }
                    break;
                case State.removing:
                    currentLength--;
                    timer = letterTime;
                    tmpText.text = text.Substring(0, currentLength);
                    if (currentLength <= 0)
                    {
                        state = State.waiting;
                    }
                    break;
                case State.waiting:
                    break;
                case State.displaying:
                    timer = displayTime;
                    state = State.removing;
                    break;
            }
        }
    }

    public void displayText(string textToDisplay)
    {
        text = textToDisplay;
        state = State.adding;
        tmpText.text = "";
        currentLength = 0;
    }

    public void displayWave()
    {
        displayText("Wave " + RoomController.wave + "/" + RoomController.maxWaves);
    }

}
