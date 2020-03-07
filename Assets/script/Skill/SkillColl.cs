using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillColl : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "NPC")
        {
            Debug.LogError("1");
        }
    }
}
