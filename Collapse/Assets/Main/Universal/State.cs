using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State 
{
   public Action ActiveAction, EnterAction, ExitAction;

   public State(Action Active, Action Enter, Action Exit) 
    {
        ActiveAction = Active;
        EnterAction = Enter;
        ExitAction = Exit;
    }

    public void Execute_S() 
    {
        if (ActiveAction != null) 
        {
            ActiveAction.Invoke(); 
        }
    }

    public void Enter_S() 
    {
        if (EnterAction != null)
        {
            EnterAction.Invoke();
        }
    }

    public void Exit_S() 
    {
        if (ExitAction != null)
        {
            ExitAction.Invoke();
        }
    }
}
