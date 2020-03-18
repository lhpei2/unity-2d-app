using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameObject piecePrefab;

    GameObject selected=null;
    Vector2 diffPos;

    List<GameObject> pieceList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] sprite=Resources.LoadAll<Sprite>("Tangrams/sprTangram");

        for(int i=0;i<sprite.Length;i++)
        {
            GameObject inst=Instantiate(piecePrefab, new Vector3(0,0,0), Quaternion.identity);
            inst.GetComponent<SpriteRenderer>().sprite=sprite[i];
            inst.AddComponent<PolygonCollider2D>();
            pieceList.Add(inst);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            Touch touch=Input.GetTouch(0);
            Vector2 touchPosition=Camera.main.ScreenToWorldPoint(touch.position);
            
            if (touch.phase==TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit)
                {
                    selected=hit.transform.gameObject;
                    diffPos=new Vector2(selected.transform.position.x,selected.transform.position.y)-touchPosition;
                }
            }

            if (selected)
            {
                selected.transform.position=diffPos+touchPosition;
                if (touch.phase==TouchPhase.Ended)
                {
                    selected=null;
                }
            }
        }
        else
        {   
            bool isPlaced=true;

            for(int i=0;i<pieceList.Count;i++) 
            {
                GameObject inst=pieceList[i];
                Vector3 v=inst.GetComponent<TouchMove>().startPos;
                if((v-inst.transform.position).magnitude>0.7)
                {
                    isPlaced=false;
                    break;
                }
            }

            if (isPlaced)
            {
                for(int i=0;i<pieceList.Count;i++) 
                {
                    GameObject inst=pieceList[i];
                    Vector3 v=inst.GetComponent<TouchMove>().startPos;
                    inst.transform.position=v;
                }
            }
        }
    }
}
