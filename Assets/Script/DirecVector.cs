using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirecVector : MonoBehaviour
{
   public Transform _target;
    public string targetTag = "Block";
     List<Transform> transformsList = new List<Transform>();

     public Transform Character;

     public Transform TargetZone;
     Vector3 targetZoneSize = new Vector3(10f, 10f, 10f);


   private void Start() {
       GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);
       
        // Duyệt qua mỗi GameObject để truy cập và thêm Transform vào danh sách
        foreach (GameObject obj in objectsWithTag)
        {
            Transform objTransform = obj.transform;

            // Thêm Transform vào danh sách
            transformsList.Add(objTransform);
        }

   }
   void Update()
   {
    
     foreach(Transform trans in transformsList){
      if (!trans.IsChildOf(Character) && Vector3.Distance(trans.position,TargetZone.position) > (targetZoneSize.magnitude / 2f)){
         this.transform.LookAt(trans);
         break;
              }
      else {
          this.transform.LookAt(_target);
      }
    }
   }
    //public IEnumerator DirectArrow (Transform trans) {

   //}
}


