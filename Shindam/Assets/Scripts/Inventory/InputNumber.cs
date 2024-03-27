using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using UnityEngine.Rendering;
using System;

public class InputNumber : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TMP_Text previewText;
    [SerializeField]
    private TMP_Text inputText;

    private bool activated = true;

    private int itemIndex;

    private String preview, input;

    public event Action<int, int> OnEndInput;
    private void Start()
    {
        Hide();
    }
    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                OK();
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void InitializeInputField(int index, string countText)
    {
        Show();
        activated = true;
        inputField.text = "";
        previewText.text = countText;
        itemIndex = index;
        preview = previewText.text;
    }
    public void OK()
    {
        int throwCount;
        input = inputField.text;
        if(input != "")
        {
            if (CheckNumber(input))
            {
                throwCount = int.Parse(input);
                if (throwCount > int.Parse(preview))
                {
                    throwCount = int.Parse(preview);
                }
            }
            else throwCount = 1;
        }
        else throwCount = int.Parse(preview);
        Debug.Log(throwCount);
        OnEndInput?.Invoke(itemIndex, throwCount);
        Hide();
    }
    public void Cancel()
    {
        activated = false;
        Hide();
    }
    public bool CheckNumber(string text)
    {
        char[] charArr = text.ToCharArray();
        bool isNumber = true;
        for(int i = 0; i < charArr.Length; i++)
        {
            if (charArr[i] >= 48 && charArr[i] <= 57) continue;
            isNumber = false;
        }
        return isNumber;
    }
}
