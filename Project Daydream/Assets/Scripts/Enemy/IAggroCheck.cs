using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAggroCheck
{
    public void InAggro();
    public void OutAggro();
    public int PlayerDir { get; set; }
}
