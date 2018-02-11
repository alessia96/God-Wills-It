﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeToCreds : MonoBehaviour {
  public Image FadeImg;
  public float fadeSpeed = 1.5f;
  public bool sceneStarting = true;
  public Text story;
  public Text story2;

  public Text creditsText;

  void Awake()
    {
      FadeImg.rectTransform.localScale = new Vector2(100, 100);
    }

  void Update()
    {
      // If the scene is starting...
      if (sceneStarting)
        // ... call the StartScene function.
        StartScene();
      fade ();

    }

    void fade(){
        if (story.GetComponent<CreditFade>().done) {
          story2.GetComponent<CreditFade2>().enabled = true;
          StartCoroutine(fadeLater());
          //StartCoroutine (comeBack ());
        }
    }
    IEnumerator fadeLater()
    {
      yield return new WaitForSeconds(10);
      FadeImg.enabled = true;
      FadeToBlack();
      SceneManager.LoadScene(3);
    }

  void FadeToClear()
    {
      // Lerp the colour of the image between itself and transparent.
      FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


  public void FadeToBlack()
    {
      // Lerp the colour of the image between itself and black.
      FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


  void StartScene()
    {
      // Fade the texture to clear.
      FadeToClear();

      // If the texture is almost clear...
      if (FadeImg.color.a <= 0.05f)
        {
          // ... set the colour to clear and disable the RawImage.
          FadeImg.color = Color.clear;
          FadeImg.enabled = false;

          // The scene is no longer starting.
          sceneStarting = false;
      }
    }


  public void EndScene(int SceneNumber)
    {
      // Make sure the RawImage is enabled.
      FadeImg.enabled = true;

      // Start fading towards black.
      FadeToBlack();

      // If the screen is almost black...
      if (FadeImg.color.a >= 0.95f)
        // ... reload the level
        SceneManager.LoadScene(3);
    }

    IEnumerator comeBack(){
      yield return new WaitForSeconds (5);
      StartScene ();


    }

}