using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; 
[System.Serializable]
public class TalkData
{
    public string chara, words, voice;
}
[System.Serializable]
public class SequenceData
{
    public int fps;
    public bool auto;
    public string film;
    public string bgm;
    public bool repeat;
    public string stay;
    public string transition;
    public TalkData[] talk;
}
[System.Serializable]
public class Sequence
{
    public SequenceData[] sequence;
}

public class PlayerManager {
    public int currentSequenceIndex = 0;
    public int currentSubSequenceIndex = 0;
    public Dictionary<string, AudioClip> voiceClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, Texture2D> imageCollections = new Dictionary<string, Texture2D>();
    public int animFPS = 1;
    public bool Opened = false;
    public int maxFrame = 16;
}
public class DownloadInfo {
    public string Name;
    public float Progress;
}
public class DownloadManager : MonoBehaviour
{
    public static DownloadManager Instance { get; private set; }

    public PlayerManager playerManager = new PlayerManager();
    [SerializeField]
    public Sequence sequenceData;
    // 此处代码用作DEBUG，视编辑器后续更新而定，将此处的public设置为private
    public string contentFolderName;

    public string jsonPath = "file:///C:/Users/imgRenko/Desktop/test.json";

    private RawImage sceneContent;

    private AudioSource audioSource;

    private Text Message, CharName;

    [HideInInspector]
    public Slider seqSlider, talSlider;

    public string yourName = "Master";

    public Text downloadProgress;

    public Button closeButton, downloadButton;

    private List<DownloadInfo> downloadInfos = new List<DownloadInfo>();

    private int FPSCount = 0;
    private float FrameCount = 0;

    private string localDirectory = Application.streamingAssetsPath;

    private void Awake()
    {
        GameObject imageObject = GameObject.Find("Content");

        GameObject messageText = GameObject.Find("Message");
        GameObject charNameText = GameObject.Find("CharName");

        GameObject A = GameObject.Find("A");
        GameObject B = GameObject.Find("B");

        sceneContent = imageObject.GetComponent<RawImage>();
        audioSource = imageObject.GetComponent<AudioSource>();

        Message = messageText.GetComponent<Text>();
        CharName = charNameText.GetComponent<Text>();

        seqSlider = A.GetComponent<Slider>();

 
 

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        test();
        //StartCoroutine(DownloadJsonTest());
        //
    }
    public void test() {
      
    }
    public IEnumerator DownloadImage()
    {
        string url = "file:///C:/Users/imgRenko/Desktop/5178-2-2_c1.jpg";

        WWW www = new WWW(url);
        yield return www;

        sceneContent.texture = www.texture;

    }

    public void BanButton(bool Ban) {
        closeButton.interactable = !Ban;
        downloadButton.interactable = !Ban;
    }

    public IEnumerator DownloadJsonTest()
    {


        string index6 = contentFolderName.Substring(contentFolderName.Length - 6, 3);
        string index3 = contentFolderName.Substring(contentFolderName.Length - 3, 3);

        string kamihimeUrl = string.Format("https://static-r.kamihimeproject.net/scenarios/{0}/{1}", index6, index3);

        bool isCache = true;

        string p = localDirectory + "/" + contentFolderName;

        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
            isCache = false;
        }
        if (!File.Exists(p + "/scenario.json"))
            isCache = false;


        string url = string.Format("{0}/{1}/{2}", isCache ? localDirectory : kamihimeUrl,contentFolderName, "scenario.json"); //jsonPath;


        if (isCache)
            Debug.Log("从缓存读取" + url);

        DownloadInfo info = new DownloadInfo();
        info.Name = url;
        info.Progress = 0;
        downloadInfos.Add(info);

        WWW www = new WWW(url);
        yield return www;
  
        

        if (www.isDone)
        {
            string content = "{\"sequence\":" + www.text + "}";
            content = content.Replace("},\n],}\n];", "}\n]}\n]");
            content = content.Replace("},\n],},\n", "}\n]},\n");
            content = content.Replace("\"words\":\"\",", "\"words\":\"\"");
            content = content.Replace(",\n\n", "");
            Debug.Log(content);
            
            downloadInfos.Remove(info);
            if (www.error != null)
            {
                Debug.LogError(www.error);
                Message.text = www.error;
            }      
            else
                Message.text = "已经装载json文件，再点击一次下载按钮。";

            if (isCache == false)
                File.WriteAllText(string.Format("{0}/{1}/{2}", localDirectory, contentFolderName, "scenario.json"),www.text);

            sequenceData = JsonUtility.FromJson<Sequence>(content);
        }
    }

    public IEnumerator DownloadAsset(string path,bool isImage)
    {


        string index6 = contentFolderName.Substring(contentFolderName.Length - 6, 3);
        string index3 = contentFolderName.Substring(contentFolderName.Length - 3, 3);

        string kamihimeUrl = string.Format( "https://static-r.kamihimeproject.net/scenarios/{0}/{1}", index6, index3);

        bool isCache = true;

        string p = localDirectory + "/" + contentFolderName;

        if (!Directory.Exists(p))
        {
            Directory.CreateDirectory(p);
            isCache = false;
        }
        if (!File.Exists(p + "/" + path))
            isCache = false;

        string url = string.Format("{0}/{1}/{2}", isCache ? localDirectory : kamihimeUrl, contentFolderName, path);
        if (isCache)
            Debug.Log("从缓存读取" + url);
        DownloadInfo info = new DownloadInfo();
        info.Name = url;
        info.Progress = 0;
        downloadInfos.Add(info);

        float Progess = 0;
        string Info = "";

        if (isImage)
        {
            WWW www = new WWW(url);
            
            while (!www.isDone)
            {
                Progess = www.progress;
                info.Progress = Progess;
                yield return null;
            }
           
            playerManager.imageCollections.Add(path, www.texture);
            if (www.error != null)
            {
                Debug.LogError(www.error);
                Message.text = www.error;
            }

            if (www.isDone) {
                downloadInfos.Remove(info);
               
            }
            if (isCache == false)
                File.WriteAllBytes(string.Format("{0}/{1}/{2}", localDirectory, contentFolderName, path), www.bytes);
        }
        else
        {
    
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            while (!www.isDone) {
                Progess = www.downloadProgress;
                info.Progress = Progess;
                yield return null;
            }
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                Message.text = www.error;
            }
            else
            {
                if (isCache == false)
                    File.WriteAllBytes(string.Format("{0}/{1}/{2}", localDirectory, contentFolderName, path), www.downloadHandler.data);
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                var memStream = new System.IO.MemoryStream(results);
                var mpgFile = new NLayer.MpegFile(memStream);
                var samples = new float[mpgFile.Length];
                mpgFile.ReadSamples(samples, 0, (int)mpgFile.Length);

                var clip = AudioClip.Create("voice", samples.Length, mpgFile.Channels, mpgFile.SampleRate, false);
                clip.SetData(samples, 0);
                playerManager.voiceClips.Add(path, clip); // source.clip = clip;
            }
            if (www.isDone)
            {
                downloadInfos.Remove(info);
            }
        }

         
    }

    public void Close() {
        playerManager.imageCollections.Clear();
        playerManager.voiceClips.Clear();
        playerManager.currentSubSequenceIndex = 0;
        playerManager.currentSequenceIndex = 0;
        sequenceData.sequence = new SequenceData[0] ;
        sceneContent.texture = Texture2D.whiteTexture;
        CharName.text = "";
        Message.text = "";
        playerManager.Opened = false;
    }

    public void DownloadCharacterData() {
        if (playerManager.Opened) 
            Close();
        if (sequenceData.sequence.Length == 0)
        {
            StartCoroutine(DownloadJsonTest());
           
        }
        else
        {
            CharName.text = "";
            Message.text = "";
            foreach (var subSequenceData in sequenceData.sequence)
            {
                if (subSequenceData.film != null)
                    StartCoroutine(DownloadAsset(subSequenceData.film, true));

                foreach (var talkingData in subSequenceData.talk)
                    if (talkingData.voice != null)
                        StartCoroutine(DownloadAsset(talkingData.voice, false));

            }
            seqSlider.maxValue = DownloadManager.Instance.sequenceData.sequence.Length - 1;
            playerManager.Opened = true;
        }
    }


    public void ClickAndGo(bool add = true) {
        if (playerManager.currentSequenceIndex > sequenceData.sequence.Length - 1)
            return;

        seqSlider.value = playerManager.currentSequenceIndex;
        talSlider.maxValue = sequenceData.sequence[playerManager.currentSequenceIndex].talk.Length - 1;

        SequenceData seqData = sequenceData.sequence[playerManager.currentSequenceIndex];
       
        if (seqData.film != null)
        {
            Texture2D output;
            playerManager.imageCollections.TryGetValue(seqData.film, out output);
            sceneContent.texture = output;
            if (output.height < 1440)
                playerManager.maxFrame = 1;
            else
                playerManager.maxFrame = 16;
           
           
        }
       
        playerManager.animFPS = seqData.fps;
        talSlider.value = playerManager.currentSubSequenceIndex;
        if (seqData.talk.Length != 0 && playerManager.currentSubSequenceIndex <= seqData.talk.Length - 1)
        {
            TalkData talData = seqData.talk[playerManager.currentSubSequenceIndex];
            if (talData.voice != null)
            {
                AudioClip output;
                playerManager.voiceClips.TryGetValue(talData.voice, out output);
                audioSource.clip = output;
                audioSource.Play();
            }
            if (add)
                playerManager.currentSubSequenceIndex++;
            CharName.text = talData.chara;
            Message.text = talData.words.Replace("{{主人公}}",yourName);
        }

        if (playerManager.currentSubSequenceIndex > seqData.talk.Length - 1)
        {
            playerManager.currentSequenceIndex++;
            
            playerManager.currentSubSequenceIndex = 0;
        }
    }
    private void Update()
    {
        bool downloadingContent = downloadInfos.Count != 0;
        downloadProgress.gameObject.SetActive(downloadingContent);
        BanButton(downloadingContent);
        if (downloadInfos.Count != 0)
        {
            downloadProgress.text = "";
            foreach (var info in downloadInfos)
            {
                downloadProgress.text +="Downloading "+ info.Name + " - " + (info.Progress*100.0f).ToString("f2") + "%\r\n";
            }
        }
        float avarrage = 1.0f / playerManager.maxFrame;

        float FPS = playerManager.animFPS;

        if (FPS != 1) {
            FrameCount+=Time.deltaTime;
            if (FrameCount >= 1.0f/ FPS)
            {
                if (FPSCount > playerManager.maxFrame)
                    FPSCount = 0;
                FPSCount++;
                FrameCount = 0;
            }

        }

        if (playerManager.maxFrame != 1)
            sceneContent.uvRect = new Rect(0, 1-FPSCount * avarrage, 1, 0.0625f);
        else
            sceneContent.uvRect = new Rect(0, 0, 1, 1);
    }
}
