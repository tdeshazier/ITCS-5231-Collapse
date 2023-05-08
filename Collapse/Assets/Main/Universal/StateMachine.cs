using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Stack<State> states { get; set; }
    public string current_state = "";

    private void Awake()
    {
        states = new Stack<State>();
    }

    private void Update()
    {
        if(Get_Current_State() != null) 
            Get_Current_State().ActiveAction.Invoke(); 
        
    }

    public void Push_State(System.Action active, System.Action Enter, System.Action Exit)
    {
        if (Get_Current_State() != null)
             Get_Current_State().Exit_S();

        State state = new State(active, Enter, Exit);
        states.Push(state);

        Get_Current_State().Enter_S();

    }

    public void Pop_State() 
    {
        if (Get_Current_State() != null)
        { 
            Get_Current_State().Exit_S();
            Get_Current_State().ActiveAction = null;
            states.Pop();

            Get_Current_State().Enter_S();
        }

    }
    private State Get_Current_State() 
    {
        return states.Count > 0 ? states.Peek() : null;
    }
}
