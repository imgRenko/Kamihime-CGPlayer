using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Select : MonoBehaviour
{
    public Dropdown dr1,dr2;
    public void Set() {
        int i = 0;
        if (dr1.value != 0)
        {
            dr2.interactable = false;
            foreach (var p in DownloadManager.Instance.coreImagesList)
            {
                p.gameObject.SetActive(true);
                i++;
                Icon g = p.gameObject.GetComponent<Icon>();
                if (dr1.value == 1)
                    if (g.Mode != "job")
                    {
                        p.gameObject.SetActive(false);
                        i--;
                    }
                if (dr1.value == 2)
                    if (g.Mode != "summon")
                    {
                        p.gameObject.SetActive(false);
                        i--;
                    }
            }

        }
        else
        {
            dr2.interactable = true;
            foreach (var p in DownloadManager.Instance.coreImagesList)
            {
                Icon g = p.gameObject.GetComponent<Icon>();

                if (g.Mode != "chara")
                {
                    p.gameObject.SetActive(false);
                  
                }
                else
                {
                    
                    p.gameObject.SetActive(true);
                    i++;
                    if (dr2.value == 0)
                        if (g.Rank != 5)
                        {
                            p.gameObject.SetActive(false);
                            i--;
                        }
                    if (dr2.value == 1)
                        if (g.Rank != 6)
                        {
                            p.gameObject.SetActive(false);
                            i--;
                        }
                    if (dr2.value == 2)
                        if (g.Rank != 7)
                        {
                            p.gameObject.SetActive(false);
                            i--;
                        }
                }
            }
        }

        DownloadManager.Instance.panelRect.sizeDelta = new Vector2(DownloadManager.Instance.panelRect.sizeDelta.x, Mathf.Clamp(i / 5 * 200, 1141, 0xfffffff));

    }
}
