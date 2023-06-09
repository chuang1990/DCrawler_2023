using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorCutscene : MonoBehaviour
{
	public float InMirrorDuration = 4;

	private const string k_SceneName = "Mirror";

	private Camera m_MirrorCamera;
	private Animator m_MirrorAnimator;
	private Animator m_BubblesAnimator;
	//private GameObject m_Player;
	private PlayerController m_Player;
	private GameObject m_UIInteractable;

	public void Play(DoorColor doorColor)
	{
		StopAllCoroutines();
		StartCoroutine(PlayCutscene(doorColor));
	}

	private IEnumerator PlayCutscene(DoorColor doorColor)
	{
		m_UIInteractable.SetActive(false);

		//m_Player.SetActive(false);
		m_Player.enabled = false;

		m_MirrorCamera.gameObject.SetActive(true);
		m_MirrorAnimator.Play("EnterInMirror");
		m_BubblesAnimator.Play($"Mirror_bubbles_{doorColor.name}");

		yield return new WaitForSeconds(InMirrorDuration);

		m_MirrorAnimator.SetTrigger("Exit");

		yield return new WaitForSeconds(1.3f);

		//var exitInMirrorClip = m_MirrorAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
		//yield return new WaitForSeconds(exitInMirrorClip.length);

		m_MirrorCamera.gameObject.SetActive(false);

		//m_Player.SetActive(true);
		m_Player.enabled = true;

		m_UIInteractable.SetActive(true);
	}

	private void Awake()
	{
		//m_Player = GameObject.FindGameObjectWithTag("Player");
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		m_UIInteractable = GameObject.Find("/UI/Interactable");
	}

	private void Start()
	{
		var asyncOperation = SceneManager.LoadSceneAsync(k_SceneName, LoadSceneMode.Additive);
		asyncOperation.completed += OnSceneLoaded;
	}

	private void OnSceneLoaded(AsyncOperation asyncOperation)
	{
		var mirrorScene = SceneManager.GetSceneByName(k_SceneName);

		var roots = mirrorScene.GetRootGameObjects();

		foreach (var root in roots)
		{
			root.transform.position += Vector3.right * 50;
		}

		m_MirrorCamera = GameObject.Find("/PostProcessA/Main Camera").GetComponent<Camera>();
		m_MirrorCamera.gameObject.SetActive(false);

		Destroy(m_MirrorCamera.GetComponent<StudioListener>());
		Destroy(m_MirrorCamera.GetComponent<StudioEventEmitter>());

		m_MirrorAnimator = GameObject.Find("/PostProcessA").GetComponent<Animator>();

		m_BubblesAnimator = GameObject.Find("/Mirror/BUBBLES").GetComponent<Animator>();
	}
}
