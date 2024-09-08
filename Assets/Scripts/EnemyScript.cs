using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FlyForward");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
    }
    IEnumerator FlyForward()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
    }
}
