using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject restartMenu;
    [SerializeField] public Slider slider;
    [SerializeField] public GameObject joystick;
    [SerializeField] public List<MonoBehaviour> scripts;
    public AudioSource audioSource;
    public string fileName = "difficulty.txt";
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Invoke("PlayMusic", audioSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayMusic()
    {
        audioSource.Play();
    }

    public void StartGame()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
  
        using (StreamWriter streamWriter = File.CreateText(filePath))
        {
            streamWriter.WriteLine(slider.GetComponent<Slider>().value);
        }

        SceneManager.LoadScene("Gameplay");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        joystick.SetActive(false);
        foreach(MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

    }

    public void ResumeGame()
    {
        /**/
        pauseMenu.SetActive(false);
        joystick.SetActive(true);
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }

    public void EndGame()
    {
        restartMenu.SetActive(true);
        joystick.SetActive(false);
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
