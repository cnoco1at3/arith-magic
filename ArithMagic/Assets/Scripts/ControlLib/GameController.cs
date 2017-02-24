using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class GameController : GenericSingleton<GameController> {

    [SerializeField]
    private string next_scene_;

    public List<PartsAcceptor> acceptors_;

    void Awake() {
        acceptors_ = new List<PartsAcceptor>();
    }

    public void OnEndGame() {
        if (IsEndGame())
            SceneManager.LoadScene(next_scene_);
    }

    public bool IsEndGame() {
        foreach (PartsAcceptor acceptor in acceptors_)
            if (!acceptor.IsFinished())
                return false;
        return true;
    }
}
