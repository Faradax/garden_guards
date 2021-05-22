using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buffable : MonoBehaviour
{

    private record AppliedBuff(Aura Source, Buff Buff);
    
    private HashSet<AppliedBuff> activeBuffs = new();

    public void ReceiveBuff(Aura source, Buff buff)
    {
        activeBuffs.Add(new AppliedBuff(source, buff));
        Debug.Log($"Got Buff, now have {activeBuffs.Count}");
        foreach (AppliedBuff appliedBuff in activeBuffs)
        {
            Debug.Log(appliedBuff);
        }
    }

    public void RemoveBuffs(Aura source)
    {
        activeBuffs.RemoveWhere(buff => buff.Source == source);
    }
}
