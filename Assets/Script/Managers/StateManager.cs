using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private State currState;

    public void SetState(State state)
    {
        currState = state;
        StartCoroutine(currState.Start());
    }
}
