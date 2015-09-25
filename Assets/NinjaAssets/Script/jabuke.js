#pragma strict

var theObject:GameObject;
 var maxPos:float = 30.0;
 var minPos:float = 8.0;
 var maxPosy:float = 26.4;
 var minPosy:float = 0.8;
 var maxPosz:float = 41.0;
 var minPosz:float = 11.8;
 var max = 1;
 
 function Start(){
     StartCoroutine(spawn());
 }
 
     function spawn() : IEnumerator {
       //  for (var i = 0; i < max; i++) {
             yield WaitForSeconds(1.0);
             var theNewPos= new Vector3 (Random.Range(minPos,maxPos),Random.Range(minPosy,maxPosy),Random.Range(minPosz,maxPosz));
             var go : GameObject = Instantiate(theObject);
             go.transform.position = theNewPos;
             
        // }
   }
