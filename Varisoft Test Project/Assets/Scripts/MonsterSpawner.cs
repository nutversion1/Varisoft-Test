using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Monster monsterPrefab;

    bool hasCreatedMonster = false;
    Monster currentMonster;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster(1f));
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMonster == null)
        {
            if (!hasCreatedMonster)
            {
                StartCoroutine(CreateMonster(5f));
            }
        }
      
    }

    private IEnumerator CreateMonster(float delay)
    {
        hasCreatedMonster = true;

        yield return new WaitForSeconds(delay);

        Monster newMonster = Instantiate(monsterPrefab);
        newMonster.transform.position = transform.position;
        currentMonster = newMonster;

        hasCreatedMonster = false;
    }
}
