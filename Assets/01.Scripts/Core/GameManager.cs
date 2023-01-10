using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	private List<ISystem> systems = new List<ISystem>();

	public int score;

	public UnityEvent OnStart;
	public UnityEvent OnStop;
	public UnityEvent OnInitialize;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("���� ���ӸŴ��� Ȱ��ȭ");
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		systems.Add(GetComponent<UISystem>());
		systems.Add(GetComponent<ThemeColorManager>());
		systems.Add(GetComponent<PlayerSystem>());
		systems.Add(GetComponent<ScoreSystem>());
		systems.Add(GetComponent<LevelSystem>());

		InitializeGame();
		UpdateState(GameState.Init);
	}

	public void UpdateState(GameState state)
	{
		for (int i = 0; i < systems.Count; i++)
		{
			systems[i].UpdateState(state);
		}

		if (state == GameState.Init)
		{
			UpdateState(GameState.Standby);
		}
	}

	public void InitializeGame()
	{
		UpdateState(GameState.Standby);
		OnInitialize?.Invoke();
	}
}
