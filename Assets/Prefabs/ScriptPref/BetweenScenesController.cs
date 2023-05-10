using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BetweenScenesController : MonoBehaviour
{
	
	public static bool sceneEnd;
	public float fadeSpeed = 1.5f;
	public static int nextLevel;
	private Image _image;
	private bool sceneStarting;
	public Text levelText;

	void Awake()
	{
		_image = GetComponent<Image>();
		_image.enabled = true;
		sceneStarting = true;
		sceneEnd = false;
		Cursor.visible = true;
	}
    

    void Update()
	{
		if (sceneStarting) StartScene();
		if (sceneEnd) EndScene();
	}

	void StartScene()
	{
		
		_image.color = Color.Lerp(_image.color, Color.clear, fadeSpeed * Time.deltaTime);
		

		if (_image.color.a <= 0.01f)
		{
			_image.color = Color.clear;
			_image.enabled = false;
			sceneStarting = false;
			Cursor.visible = true;
		}
	}

	void EndScene()
	{
		
		_image.enabled = true;
		_image.color = Color.Lerp(_image.color, Color.black, fadeSpeed * Time.deltaTime);
		
		
		
		if (_image.color.a >= 0.95f)
		{
			Cursor.visible = true;
			_image.color = Color.black;
			SceneManager.LoadScene(nextLevel);
		}
	}
}
