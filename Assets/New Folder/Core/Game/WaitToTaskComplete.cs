﻿using System.Threading.Tasks;
using UnityEngine;

public class WaitToTaskComplete : CustomYieldInstruction
{
    private readonly Task task;

    public WaitToTaskComplete(Task task)
    {
        this.task = task;
    }

    public override bool keepWaiting
    {
        get
        {
            if (!task.IsCompleted) return true;
            if (task.IsFaulted) throw task.Exception;
            return false;
        }
    }
}