using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    #region MemberFields

    [SerializeField]
    private GameObject endGame = null;

    private GameMode gameMode;
    #endregion MemberFields

    #region MonoBehaviour Methods

    private void Awake()
    {
        gameMode = FindObjectOfType<GameMode>();
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Character") && endGame)
        {
            other.GetComponent<Character>().mode = Character.characterMode.Idle;
            other.GetComponent<Character>().mode = Character.characterMode.endGame;
            Vector3 pos = this.transform.position;
            other.transform.position = new Vector3(pos.x, other.transform.position.y, pos.z);
            other.GetComponent<Character>().checkPoint = this.GetComponent<Collider>();
            IPlayAble tem = endGame.GetComponent<IPlayAble>();
            if ( tem != null)
            {
            endGame.GetComponent<IPlayAble>().SetCharacter(other.GetComponent<Character>());
                
            }
            
        }
        else if (!endGame )
        {
            other.GetComponent<Character>().mode = Character.characterMode.endGame;
            Vector3 pos = this.transform.position;
            other.transform.position = new Vector3(pos.x, other.transform.position.y, pos.z);
            gameMode.AddWhoFinished(other.GetComponent<Character>());
        }
    }

    #endregion MonoBehaviour Methods


    #region Public Methods

    #endregion Public Methods


    #region Private Methods

    #endregion Private Methods
}
