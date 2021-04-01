using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{
    
    /*public GameObject hitNeighbor (GameObject obj)
    {   
        return obj;
    }*/

    //column size, row size, col length, row lenght, crystal identifier;
    public int colW,rowH,colLen,rowLen,crystalId;
    public GameObject [,] CrystalsList;
    public List<GameObject> Crystals = new List<GameObject>();
    


    void Start()
    {
        //cell width
        colW=1;
        //cell height
        rowH=1;
        //number of columns
        colLen=8;
        //number of rows
        rowLen=8;
        CrystalsList = new GameObject[colLen,rowLen];


        //Resource loader, load all crystals prefabs from the Prefab/Crystals folder
        GameObject crystal = null;
        int counter = 0;
        bool done = false;

        while(!done)
        {
                crystal = Resources.Load("Crystals" + counter) as GameObject;
                if(crystal == null)
                {
                    done = true;
                } else{
                    Crystals.Add(crystal);
                }
                ++counter;
        }


        //This 'for' bellow instantiate the crystals in the canvas preventing a repetitive crystals sequence in the first turn.
        
        //There we increment lines
        for (int i=0; i<colLen; i++)
        {
            //There increment columns
            for(int j = 0; j<rowLen ; j++)
            {
                //This is a cristalId, this number will be cached for comparsion reasons.
                crystalId=Random.Range(0, 5);

                //The crystal is loaded and positioned in the canvas using a temporary crystalId
                CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector2(colW*j,rowH*i), Quaternion.identity);
                
                if(j>1){
                    //After the second iteration I start to test the two left neighbour cristals, never before this iteration because we whould get an 'Out of Bounds Error' in Array.
                    if(CrystalsList[i,j].name==CrystalsList[i,j-1].name&&CrystalsList[i,j].name==CrystalsList[i,j-2].name){
                        //If the gameobjects have the same name the gameoobject will be destroyed, the crystalId will be changed and a new crystal will be loaded.
                        Destroy(CrystalsList[i,j]);
                        crystalId = (crystalId==0 ? crystalId=Crystals.Count-1 : crystalId-1);                        
                        CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector2(colW*j,rowH*i), Quaternion.identity);
                    }
                }
                //Now, after the second line interation we need find the repeated crystals vertically
                if(i>1){
                    if(CrystalsList[i,j].name==CrystalsList[i-1,j].name&&CrystalsList[i,j].name==CrystalsList[i-2,j].name){
                        Destroy(CrystalsList[i,j]);
                        //No chance to sort a same crystal that was destroyed before because I walk in the list step by step from the end to the beginning. Certally, in this case, the destroyed crystal had another index.
                        crystalId = (crystalId==0 ? crystalId=Crystals.Count-1 : crystalId-1);                        
                        CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector2(colW*j,rowH*i), Quaternion.identity);
                        Debug.Log("passei por aqui");
                    }
                }



                //Debug.Log(hitNeighbor(crystal).ToString());
                //Debug.Log(CrystalsList[i,j].name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
