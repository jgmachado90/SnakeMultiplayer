using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandeler : MonoBehaviour
{
    private List<KeyCode> _activeInputs = new List<KeyCode>();
    public List<KeyCode> ActiveInputs { get { return _activeInputs; } }

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

        foreach (KeyCode code in _activeInputs)
        {
            releasedInput.Add(code);

            if (!pressedInput.Contains(code))
            {
                releasedInput.Remove(code);

                //Debug.Log(code + " was released");
            }
        }
        _activeInputs = releasedInput;
    }


    private void HandlePressedInputs(List<KeyCode> pressedInput)
    {
        foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(code))
            {
                _activeInputs.Remove(code);
                _activeInputs.Add(code);
                pressedInput.Add(code);
            }
        }
    }


}
