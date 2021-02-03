using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character //inheriting from character class
{
    public virtual void DeSelect()
    {
      
    }

    public virtual Transform Select()
    {
        return hitBox;
    }
}
