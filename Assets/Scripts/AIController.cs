using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    #region MemberFields
    private Character opponent;

    #endregion MemberFields

    #region MonoBehaviour Methods

    private void Awake()
    {
        opponent = this.GetComponent<Character>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (GameMode.start && !GameMode.isFinished)
        {
            if (opponent.mode == Character.characterMode.Idle)
            {
                opponent.mode = Character.characterMode.Run;
            }
        }
        else if (GameMode.isFinished)
        {
            opponent.mode = Character.characterMode.Idle;
        }
        
    }

    #endregion MonoBehaviour Methods
}
