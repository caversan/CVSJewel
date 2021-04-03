/*
CVSJewel ver:0.01b

Author: Adriano Caversan
E-mail: adriano.caversan@gmail.com
Date: Mar/30/2021

Unity3D ver: 2020.3.1f1 Personal

Description: Simple match 3 game made with Unity3D for study and portifolio purposes, the commercial use is not allowed.

3rd party assets:
- Gems: "2D FREE Crystal Set" by diluck from Unity Asset Store "thank you for this great assets" (https://assetstore.unity.com/packages/2d/textures-materials/2d-free-crystal-set-175156)
- Background: "Futuristic technological wallpaper Free Vector" by @coolvector from FreePik  "It's hard to find cool images in REAL totally free image banks, great job @coolvector" (https://www.freepik.com/vectors/background)

*** More info about the 3rd party license can be found in /Assets/3rdParty folder.

*/





using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{
    //public static MainScript instance;

    //column size, row size, col length, row lenght, crystal identifier, background cell indentfier, distance of crystal fall;
    public int colW,rowH,rowLen,colLen,crystalId,cellId,distanceY;

    //Cell prefab loader
    public List<GameObject> Cells = new List<GameObject>();
    
    
    //List for loading the possible crystals prefabs
    public List<GameObject> Crystals = new List<GameObject>();



    //List for populate the canvas with the crystals grid
    public GameObject [,] CrystalsList;
    
    public void prefabLoader (string folder, List<GameObject> thisList)
    {   
        //Prefab loader, load all prefabs from the target folder and populate the target
        GameObject objToLoad = null;
        int counter = 0;
        bool done = false;

        while(!done)
        {
                objToLoad = Resources.Load(folder + counter) as GameObject;               

                if(objToLoad == null)
                {
                    done = true;
                } else{
                    thisList.Add(objToLoad);
                }
                ++counter;
        }
    }



    void Start()
    {
        //Distance of crystal fall
        distanceY=2; 
        //cell width
        colW=1;
        //cell height
        rowH=1;
        //number of rows
        rowLen=8;
        //number of columns
        colLen=8;
        //declarate an initial background cell
        cellId=0;
        //Determining the grid size
        CrystalsList = new GameObject[rowLen,colLen];


        //Prefab loader, load all cells prefabs from the Prefab/Crystals folder
        prefabLoader ("Prefabs/UI/Cells" , Cells);       
        //Prefab loader, load all crystals prefabs from the Prefab/Crystals folder
        prefabLoader ("Prefabs/Crystals", Crystals);

        //This 'for' bellow instantiate the background cells and the crystals in the canvas preventing a repetitive crystals sequence in the first turn.
        
        //There we increment lines
        for (int i=0; i<rowLen; i++)
        {
            //There increment columns
            for(int j = 0; j<colLen ; j++)
            {
                if(cellId==0)
                {
                    Instantiate(Cells[cellId], new Vector3(colW*j,rowH*i,1), Quaternion.identity);  
                    if(j<colLen-1)
                    {
                        cellId++;
                    }
                }else{
                    Instantiate(Cells[cellId], new Vector3(colW*j,rowH*i,1), Quaternion.identity);  
                    if(j<colLen-1)
                    {
                        cellId=0;
                    }
                }

                //This is a cristalId, this number will be cached for comparsion reasons.
                crystalId=Random.Range(0, 5);

                //The crystal is loaded and positioned in the canvas using a temporary crystalId
                CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector3(colW*j,rowH*i+distanceY,0), Quaternion.identity);
                
                if(j>1)
                {
                    //After the second iteration starts a test if exists two left crystals with a same name, never before this iteration because we whould get an 'Out of Bounds Error' in Array.
                    if(CrystalsList[i,j].name==CrystalsList[i,j-1].name&&CrystalsList[i,j].name==CrystalsList[i,j-2].name){
                        //If the gameobjects have the same name the gameoobject will be destroyed, the crystalId will be changed and a new crystal will be loaded.
                        Destroy(CrystalsList[i,j]);
                        crystalId = (crystalId==0 ? crystalId=Crystals.Count-1 : crystalId-1);                        
                        CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector3(colW*j,rowH*i+distanceY,0), Quaternion.identity);
    
                    }
                }
                //Now, after the second line interation, we need find the repeated crystals vertically
                if(i>1)
                {
                    if(CrystalsList[i,j].name==CrystalsList[i-1,j].name&&CrystalsList[i,j].name==CrystalsList[i-2,j].name){
                        Destroy(CrystalsList[i,j]);
                        //No chance to sort a same crystal that was destroyed before because I walk in the list step by step from the end to the beginning. Certally, in this case, the destroyed crystal had another index.
                        crystalId = (crystalId==0 ? crystalId=Crystals.Count-1 : crystalId-1);                        
                        CrystalsList[i,j]=(GameObject)Instantiate(Crystals[crystalId], new Vector3(colW*j,rowH*i+distanceY,0), Quaternion.identity);
    
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
