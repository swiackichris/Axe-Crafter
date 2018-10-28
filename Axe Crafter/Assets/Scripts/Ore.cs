using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ore : MonoBehaviour {

    // TODO organize SerializeFields, and remove what shouldn't be there.
    [SerializeField] float FullHealth = 100.0f;
    [SerializeField] float CurrentHealth = 100f;

    [SerializeField] float FullPickaxeDamage = 10.0f;
    [SerializeField] float CurrentPickaxeDamage = 10.0f;
    [SerializeField] GameObject OrePrefab;
    GameObject ore; // Required for Button to know which object should be destroyed.

    void Start ()
    {
        ore = Instantiate(
            OrePrefab,
            new Vector2(9,9), // Change later
            Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void MineOre()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    IEnumerator DestroyAndSpawn()
    {
        Destroy(ore); // Destroys ore created in void Start()
        print("Destroy(ore)");

        CurrentHealth = FullHealth; // Resets health for new ore
        print("CurrentHealth = FullHealth");

        CurrentPickaxeDamage = 0;

        yield return new WaitForSeconds(1); // Wait time before new ore spawns

        CurrentPickaxeDamage = FullPickaxeDamage;

        // Spawn new ore
        ore = Instantiate(
        OrePrefab,
        new Vector2(9, 9),
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");
    }

}
