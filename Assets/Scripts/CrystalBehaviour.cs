using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CrystalBehaviour : MonoBehaviour
{
    public MainScript ImportedMainScript;

    int rayIdx,destroyIdx;
    bool done;

   

    private bool dragTest;



    public void OnMouseDown()
    {
        dragTest = true;
    }

    public void OnMouseUp()
    {
        dragTest = false;
    }
    void Start()
    {
        ImportedMainScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainScript>();
    }
    void Update()
    {

        if (dragTest)
        {
            for(int i = 0; i<ImportedMainScript.rowLen; i++)
            {
                for (int j= 0; j<ImportedMainScript.colLen;j++)
                {
                    ImportedMainScript.CrystalsList[i,j].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }
            }

            Vector2 mousePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
            
            if(mousePosition.x>0.5f)
            {
                Debug.Log("direita");
                RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y), Vector2.right,1);
                RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x+1,transform.position.y), Vector2.right,1);
                Debug.Log(hitA.transform.position.x);
                Debug.Log(hitB.transform.position.x);

                
                hitA.transform.position=new Vector3(hitA.transform.position.x+1,hitA.transform.position.y,hitA.transform.position.z);
                hitB.transform.position=new Vector3(hitB.transform.position.x-1,hitB.transform.position.y,hitB.transform.position.z);


                rayIdx=0;
                done=false;

                while(!done)
                {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x+rayIdx,transform.position.y), Vector2.right,1);   

                        if(hit)
                        {                               
                            Debug.Log(hit.collider.name+" in destroy");
                        }
                        else
                        {

                            if (rayIdx>2)
                            {
                                while(!done)
                                {
                                    RaycastHit2D destroy = Physics2D.Raycast(new Vector2(transform.position.x+rayIdx,transform.position.y), Vector2.right,1);
                                    if(hit)
                                    {          
                                        Debug.Log("destroy");           
                                        Destroy(hit.collider);
                                    } else {
                                        done = true;
                                    }
                                    --rayIdx;
                                }
                            }
                            
                        }
                        ++rayIdx;
                }






                /*RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x+1,transform.position.y), Vector2.right,1);

                if(hit)
                {
                    print(hit.collider.name);
                }
                else
                {
                    print("No hit");
                }*/



                
                dragTest = false;
                //transform.Translate(mousePosition.x,0,0);
                //transform.Translate(1,0,0);
                //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1,0,0), 25 * Time.deltaTime);
            } else if (mousePosition.y>0.5f){
                Debug.Log("cima");
                RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y), Vector2.right,1);
                RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y+1), Vector2.right,1);
                Debug.Log(hitA.transform.position.x);
                Debug.Log(hitB.transform.position.x);

                
                hitA.transform.position=new Vector3(hitA.transform.position.x,hitA.transform.position.y+1,hitA.transform.position.z);
                hitB.transform.position=new Vector3(hitB.transform.position.x,hitB.transform.position.y-1,hitB.transform.position.z);
                
                dragTest = false;
                //transform.Translate(0,1,0);
                //transform.Translate(0,mousePosition.y,0);
            } else if(mousePosition.x<-0.5f){
                Debug.Log("esquerda");


                RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y), Vector2.right,1);
                RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x-1,transform.position.y), Vector2.right,1);
                Debug.Log(hitA.transform.position.x);
                Debug.Log(hitB.transform.position.x);

                
                hitA.transform.position=new Vector3(hitA.transform.position.x-1,hitA.transform.position.y,hitA.transform.position.z);
                hitB.transform.position=new Vector3(hitB.transform.position.x+1,hitB.transform.position.y,hitB.transform.position.z);


                dragTest = false;
                //transform.Translate(-1,0,0);
                //transform.Translate(mousePosition.x,0,0);
                //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1,0,0), 25 * Time.deltaTime);
            } else if (mousePosition.y<-0.5f){
                Debug.Log("baixo");

                RaycastHit2D hitA = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y), Vector2.right,1);
                RaycastHit2D hitB = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-1), Vector2.right,1);
                Debug.Log(hitA.transform.position.x);
                Debug.Log(hitB.transform.position.x);

                
                hitA.transform.position=new Vector3(hitA.transform.position.x,hitA.transform.position.y-1,hitA.transform.position.z);
                hitB.transform.position=new Vector3(hitB.transform.position.x,hitB.transform.position.y+1,hitB.transform.position.z);


                dragTest = false;
                //transform.Translate(0,-1,0);
                //transform.Translate(0,mousePosition.y,0);
            }
        }
    }
}