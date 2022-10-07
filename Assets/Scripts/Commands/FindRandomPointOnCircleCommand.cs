using UnityEngine;

namespace Commands
{
    public class FindRandomPointOnCircleCommand
    {
        public Vector3 FindRandomPointOnCircle(Vector3 currentTarget,float radius)
        {
            Vector3 _manipulatedTarget =currentTarget;
            _manipulatedTarget = TakeRandomPointOnCircleEdge(_manipulatedTarget,radius);
            currentTarget = _manipulatedTarget;
            return currentTarget;
        }
        private Vector3 TakeRandomPointOnCircleEdge(Vector3 manipulatedTarget,float radius)
        {
            var vector2 = Random.insideUnitCircle.normalized * radius;
            return new Vector3(manipulatedTarget.x+vector2.x, 0, manipulatedTarget.z+vector2.y);//+-li olacak
        }
    }
}