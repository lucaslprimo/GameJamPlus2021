using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sounds : MonoBehaviour
{
    public void deadPlayer()
    {
        //Ativa o Som do FMOD
        //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);
    }
}
