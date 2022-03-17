using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBackGround : MonoBehaviour
{
    [SerializeField]
    GameObject[] BackGroundGameObject;

    float speed = 0.5f;
    float backGroundRange = 0;
    float backGroundRepeatX = 0;

    // Start is called before the first frame update

    public bool IsMove = true;

    void Start()
    {
        backGroundRange = BackGroundGameObject[1].transform.position.x - BackGroundGameObject[0].transform.position.x;
        backGroundRepeatX = BackGroundGameObject[1].transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMove)
        {
            foreach (var i in BackGroundGameObject)
            {
                i.transform.position += new Vector3(Time.deltaTime * speed, 0);
                if (backGroundRepeatX <= i.transform.position.x)
                {
                    i.transform.position += new Vector3(-backGroundRepeatX * 2, 0);
                }
            }
        }
    }
}
