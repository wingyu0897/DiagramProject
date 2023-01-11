using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : UIComponent
{
	[SerializeField]
	private Button restartButton;
	[SerializeField]
	private Button returnMenuButton;

	private GameOverUIShowing overUIShow;

	private void Awake()
	{
		overUIShow = GetComponent<GameOverUIShowing>();
		restartButton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Standby));
		restartButton.onClick.AddListener(() => overUIShow.Init());
		returnMenuButton.onClick.AddListener(() => GameManager.Instance.UpdateState(GameState.Menu));
		returnMenuButton.onClick.AddListener(() => overUIShow.Init());
	}

	private void Start()
	{
		overUIShow.Init();
	}

	public override void UpdateUI()
	{
		overUIShow.StartShowUI();
	}
}
