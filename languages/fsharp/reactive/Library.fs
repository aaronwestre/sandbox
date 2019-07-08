namespace reactive

module Vectors =
    type Vector2DInteger =
        int * int
    
    type Vector2DFloat =
        float * float
    
    type Vector2DDouble =
        double * double
    
    type Vector2D =
        | Vector2DInteger
        | Vector2DFloat
        | Vector2DDouble
    
    type Vector3DInteger =
        int * int * int
    
    type Vector3DFloat =
        float * float * float
    
    type Vector3DDouble =
        double * double * double
    
    type Vector3D =
        | Vector3DInteger
        | Vector3DFloat
        | Vector3DDouble
    
    type Vector =
        | Vector2D
        | Vector3D
    
