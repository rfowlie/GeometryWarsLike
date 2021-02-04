using UnityEngine;


//hides Shapes API from pattern creator process
public static class PatternHelper
{
    //takes in patterInfo and calls all relevant shape calls
    public static Vector3[] CreatePattern(PatternCreator.PatternInfo p)
    {
        switch (p.shape)
        {
            case SpawnShape.CIRCLE:
                return Shapes.Circle(p.radius, p.angleOffset, p.circlePoints);
            case SpawnShape.STAR:
                return Shapes.Star(p.radius, p.radiusSecond, p.fillerAmount);
            case SpawnShape.X:
                return Shapes.Cross(p.radius, p.angleOffset, p.fillerAmount);
            case SpawnShape.CheckMark:
                return Shapes.CheckMark(p.radius, p.radiusSecond, p.angleOffset, p.fillerAmount);
            default:
                return Shapes.Simple(p.shape, p.radius, p.angleOffset, p.fillerAmount);
        }
    }
}
