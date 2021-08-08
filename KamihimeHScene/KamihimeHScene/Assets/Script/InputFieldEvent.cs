using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputFieldEvent : MonoBehaviour
{
    private InputField inputField;
   
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void ChangeValue(int isFolder) {
        switch (isFolder) {
            case 0:
                DownloadManager.Instance.contentFolderName = inputField.text;
                break;
            case 1:
                DownloadManager.Instance.jsonPath = inputField.text;
                break;
            case 2:
                DownloadManager.Instance.yourName = inputField.text;
                break;
        }
      
    }
}
