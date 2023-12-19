﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Threading;
using System;
using System.IO;
using System.Threading.Tasks;

public class keywordscene4 : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private KeywordRecognitionModel keywordModel;
    // Start is called before the first frame update
    void Start()
    {

        string scriptDirectory = System.IO.Path.GetDirectoryName(Application.dataPath);
        string keywordModelPath = "Assets\\AssetsKeywordModels\\Eddie.table";
        keywordModelPath = ConvertWindowsToMacOSPath(keywordModelPath);
        Debug.Log(keywordModelPath);
        keywordModel = KeywordRecognitionModel.FromFile(keywordModelPath);
        var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        keywordRecognizer = new KeywordRecognizer(audioConfig);
        StartKeywordRecognition();
        keywordscene.r = true;
    }
    static string ConvertWindowsToMacOSPath(string windowsPath)
    {
        if (Path.DirectorySeparatorChar == '/')
        {
            string fullPath = Path.GetFullPath(windowsPath);
            string macOSPath = fullPath.Replace('\\', Path.DirectorySeparatorChar);
            return macOSPath;
        }
        else return windowsPath;
    }
    // Update is called once per frame
    private async void StartKeywordRecognition()
    {
        try
        {
            KeywordRecognitionResult result = await keywordRecognizer.RecognizeOnceAsync(keywordModel);

            if (result.Reason == ResultReason.RecognizedKeyword)
            {
                keywordscene.r = false;
                await Task.Delay(50);
                PlayerPrefs.SetInt("CharacterSelected", 3);
                PlayerPrefs.Save();
                try
                {
                    File.WriteAllText("NowAssistent.txt", "艾迪(Eddie)");
                }
                catch
                {
                    // 如果讀取文件失敗，創建一個新的文件
                    using (var fs = File.Create("NowAssistent.txt"))
                    {
                        fs.Close(); // 確保文件被關閉和釋放
                    }
                    File.WriteAllText("NowAssistent.txt", "艾迪(Eddie)");
                }
                
                SceneManager.LoadScene(1);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("" + ex.Message);
        }
    }
    private async Task StopKeywordRecognitionAsync()
    {
        await keywordRecognizer.StopRecognitionAsync();

    }
    void Update()
    {
        if (keywordscene.r == false)
        {
            StopKeywordRecognitionAsync();
        }
    }

}