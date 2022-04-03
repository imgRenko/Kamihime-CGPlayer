using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Icon : MonoBehaviour
{
    public string FolderName;
    public string StandName;
    public InputField inputField;
    public int Rank;
    public string Mode;
    public void GoStart()
    {
        inputField.text = FolderName;
        StartCoroutine(DownloadManager.Instance.SetDrawImageInfo(FolderName, StandName));
        
    }

}
