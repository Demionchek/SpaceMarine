using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;

public class Saver : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private AudioMixer audioMixer;

    private int _ammoCountToSave;
    private int _grenadesCountToSave;
    private int _totalScoreToSave;
    private float _characterSpeedToSave;
    private float _reloadDelayToSave;
    private float _shootDelayToSave;
    private float _musicValue;
    private float _effectsValue;
    private float _voicesValue;

    public int GrenadesCountToSave
    {
        get { return _grenadesCountToSave; }
        set { _grenadesCountToSave = value; }
    }
    public int AmmoCountToSave
    {
        get { return _ammoCountToSave; }
        set { _ammoCountToSave = value; }
    }
    public int TotalScoreToSave
    {
        get { return _totalScoreToSave; }
        set { _totalScoreToSave = value; }
    }
    public float CharacterSpeedToSave
    {
        get { return _characterSpeedToSave; }
        set { _characterSpeedToSave = value; }
    }
    public float ReloadDelayToSave
    {
        get { return _reloadDelayToSave; }
        set { _reloadDelayToSave = value; }
    }
    public float ShootDelayToSave
    {
        get { return _shootDelayToSave; }
        set { _shootDelayToSave = value; }
    }
    public float MusicValue
    {
        get { return _musicValue; }
        set { _musicValue = value; }
    }
    public float EffectsVolume
    {
        get { return _effectsValue; }
        set { _effectsValue = value; }
    }
    public float VoicesValue
    {
        get { return _voicesValue; }
        set { _voicesValue = value; }
    }


    private void Awake()
    {
        LoadScore();
        LoadAmmo();
        LoadReloadDelay();
        LoadShootDelay();
        LoadSpeed();
        LoadPlayerPrefs();
    }

    public void SavePlayerPrefs(float music, float effects, float voices)
    {
        _musicValue = music;
        _effectsValue = effects;
        _voicesValue = voices;
        PlayerPrefs.SetFloat("SavedMusicValue", _musicValue);
        PlayerPrefs.SetFloat("SavedEffectsValue", _effectsValue);
        PlayerPrefs.SetFloat("SavedVoicesValue", _voicesValue);
    }

    private void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("SavedMusicValue"))
        {
            audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("SavedMusicValue", 0f));
            audioMixer.SetFloat("Effects", PlayerPrefs.GetFloat("SavedEffectsValue", 0f));
            audioMixer.SetFloat("Voices", PlayerPrefs.GetFloat("SavedVoicesValue", 0f));
        }
    }

    public void SaveScore()
    {
        _totalScoreToSave = GetComponent<UIStates>().TotalScoreCount;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataScore.dat");
        SaveData data = new SaveData();
        data.savedTotalScore = _totalScoreToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveSpeed()
    {
        _characterSpeedToSave = PlayerObj.GetComponent<PlayerMovement>().Speed;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataCharSpeed.dat");
        SaveData data = new SaveData();
        data.savedCharacterSpeed = _characterSpeedToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveShootDelay()
    {
        _shootDelayToSave = PlayerObj.GetComponent<PlayerInput>().ShootDelay;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataShootDelay.dat");
        SaveData data = new SaveData();
        data.savedShootDelay = _shootDelayToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveAmmo()
    {
        _ammoCountToSave = PlayerObj.GetComponent<PlayerInput>().FullAmmo;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataAmmo.dat");
        SaveData data = new SaveData();
        data.savedAmmoCount = _ammoCountToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveReloadDelay()
    {
        _reloadDelayToSave = PlayerObj.GetComponent<PlayerInput>().ReloadDelay;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataReloadDelay.dat");
        SaveData data = new SaveData();
        data.savedReloadDelay = _reloadDelayToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveGrenades()
    {
        _grenadesCountToSave = PlayerObj.GetComponent<PlayerInput>().MaxGrenades;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveDataGrenades.dat");
        SaveData data = new SaveData();
        data.savedGrenadesCount = _grenadesCountToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGrenades()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataGrenades.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataGrenades.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _grenadesCountToSave = data.savedGrenadesCount;
        }
    }

    public void LoadReloadDelay()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataReloadDelay.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataReloadDelay.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _reloadDelayToSave = data.savedReloadDelay;
        }
    }

    public void LoadAmmo()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataAmmo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataAmmo.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _ammoCountToSave = data.savedAmmoCount;
        }
    }

    public void LoadShootDelay()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataShootDelay.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataShootDelay.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _shootDelayToSave = data.savedShootDelay;
        }
    }


    public void LoadSpeed()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataCharSpeed.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataCharSpeed.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _characterSpeedToSave = data.savedCharacterSpeed;
        }
    }

    public void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveDataScore.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _totalScoreToSave = data.savedTotalScore;
        }
    }

    public void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveDataScore.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataScore.dat");
            _totalScoreToSave = 0;
        }

        if (File.Exists(Application.persistentDataPath + "/MySaveDataCharSpeed.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataCharSpeed.dat");
            _characterSpeedToSave = 2.5f;
        }

        if (File.Exists(Application.persistentDataPath + "/MySaveDataShootDelay.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataShootDelay.dat");
            _shootDelayToSave = 0.35f;

        }

        if (File.Exists(Application.persistentDataPath + "/MySaveDataAmmo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataAmmo.dat");
            _ammoCountToSave = 10;
        }

        if (File.Exists(Application.persistentDataPath + "/MySaveDataReloadDelay.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataReloadDelay.dat");
            _reloadDelayToSave = 2f;
        }

        if (File.Exists(Application.persistentDataPath + "/MySaveDataGrenades.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveDataGrenades.dat");
            _grenadesCountToSave = 0;
        }
    }
}
[Serializable]
class SaveData
{
    public int savedAmmoCount;
    public int savedGrenadesCount;
    public int savedTotalScore;
    public float savedCharacterSpeed;
    public float savedReloadDelay;
    public float savedShootDelay;

}
