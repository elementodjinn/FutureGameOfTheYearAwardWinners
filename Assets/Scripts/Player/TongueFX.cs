using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TongueFX : MonoBehaviour
{
    public enum effectTypes { poison, speed,ichor }
    public GameObject[] effectPrefabs;
    private Dictionary<effectTypes, GameObject> effectInsts;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void effectOn(effectTypes whichType)
    {
        if (!effectInsts.ContainsKey(whichType))
        {
            GameObject toBeAdded = Instantiate(effectPrefabs[(int)whichType], transform);
            effectInsts.Add(whichType, toBeAdded);
        }
    }
    public void effectOff(effectTypes whichType)
    {
        if (effectInsts.ContainsKey(whichType))
        {
            Destroy(effectInsts[whichType], 0.01f);
            effectInsts.Remove(whichType); }
    }
    public void oneTimeEffect(effectTypes whichType, Vector3 pos, Quaternion facing)
    {
        GameObject tempEffect = Instantiate(effectPrefabs[(int)whichType], pos, facing);
        Destroy(tempEffect, tempEffect.GetComponent<ParticleSystem>().main.duration);
    }
}
