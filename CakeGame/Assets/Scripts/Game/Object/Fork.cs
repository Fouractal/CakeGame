using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : MonoBehaviour
{
    public Vector2 target;

    public IEnumerator FallDown()
    {
        float fallDownDelay = Random.Range(GameManager.instance.balancingSO.attackDelayMin, GameManager.instance.balancingSO.attackDelayMax);
        yield return new WaitForSeconds(fallDownDelay);

        gameObject.AddComponent<Rigidbody>();
    }
    
}
