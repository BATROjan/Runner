using UnityEditor;
using UnityEngine;

public class SaveDataSystem
{
    public void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt("BestScore", score);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save best score");
    }
    
    public int LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            return PlayerPrefs.GetInt("BestScore");
        }

        return 0;
    }
    
    public void SaveStarScore(int score)
    {
        PlayerPrefs.SetInt("StarScore", score);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save star score");
    }
    
    public int LoadStarScore()
    {
        if (PlayerPrefs.HasKey("StarScore"))
        {
            return PlayerPrefs.GetInt("StarScore");
        }

        return 0;
    }

    public void SaveStateIntro(int isComplete)
    {
        PlayerPrefs.SetInt("IntroState", isComplete);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save state intro");
    }

    public bool LoadStateIntro()
    {
        if (PlayerPrefs.HasKey("IntroState"))
        {
            return true;
        }
        return false;
    }
    
    public void SaveStateTutor(int isComplete)
    {
        PlayerPrefs.SetInt("TutorState", isComplete);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save state tutor");
    }

    public bool LoadStateTutor()
    {
        if (PlayerPrefs.HasKey("TutorState"))
        {
            return true;
        }
        return false;
    }

    public void SaveStateSound(int isComplete)
    {
        PlayerPrefs.SetInt("SoundState", isComplete);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save sound state");
    }
    
    public int LoadStateSound()
    {
        if (PlayerPrefs.HasKey("SoundState"))
        {
            return PlayerPrefs.GetInt("SoundState");
        }
        return 1;
    }
    
    public void SaveLanguage(int isComplete)
    {
        PlayerPrefs.SetInt("Language", isComplete);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save language");
    }
    
    public int LoadLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            return PlayerPrefs.GetInt("Language");
        }
        return 0;
    }
    
    public void SaveStateStyleWorld(int isComplete)
    {
        PlayerPrefs.SetInt("Style", isComplete);
        PlayerPrefs.Save();
        Debug.Log("[SaveDataSystem] Save Style");
    }
    
    public int LoadStateStyleWorld()
    {
        if (PlayerPrefs.HasKey("Style"))
        {
            return PlayerPrefs.GetInt("Style");
        }
        return 0;
    }
    
    
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("[SaveDataSystem] Save file delete");
    }
}
