using System.Linq;
..
// get list of objects with tag "Player"
GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
// get closest transform from gos[] array, into target variable, as transform object
var target = gos.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First().transform;
