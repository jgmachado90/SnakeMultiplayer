using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandeler : MonoBehaviour
{
    public List<KeyCode> activeInputs = new List<KeyCode>();

    public void Update()
    {
        List<KeyCode> pressedInput = new List<KeyCode>();

        if (Input.anyKeyDown || Input.anyKey)
        {
            HandlePressedInputs(pressedInput);
        }
        HandleReleasedInputs(pressedInput);
    }

    private void HandleReleasedInputs(List<KeyCode> pressedInput)
    {
        List<KeyCode> releasedInput = new List<KeyCode>();

        foreach (KeyCode code in activeInputs)
        {
            releasedInput.Add(code);

            if (!pressedInput.Contains(code))
            {
                releasedInput.Remove(code);

                //Debug.Log(code + " was released");
            }
        }
        activeInputs = releasedInput;
    }


    private void HandlePressedInputs(List<KeyCode> pressedInput)
    {
        foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(code))
            {
                activeInputs.Remove(code);
                activeInputs.Add(code);
                pressedInput.Add(code);

                //Debug.Log(code + " was pressed");
            }
        }
    }


}
