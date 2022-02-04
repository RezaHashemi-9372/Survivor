using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region MemberFields
    private Character player;
    #endregion MemberFields


    #region MonoBehaviour Methods

    private void Awake()
    {
        player = GetComponent<Character>();
    }
    void Update()
    {
        if (GameMode.start)
        {
            if (player.mode == Character.characterMode.endGame)
            {
                return;
            }
            if (Input.GetMouseButton(0) && player.mode == Character.characterMode.Idle)
            {
                if (player.mode == Character.characterMode.Idle)
                {
                    player.mode = Character.characterMode.Run;
                }
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                player.mode = Character.characterMode.Idle;
            }

        }
    }

    #endregion MonoBehaviour Methods


    #region Public Methods

    #endregion Public Methods


    #region Private Methods

    #endregion Private Methods
}
