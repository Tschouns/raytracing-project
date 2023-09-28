//  self

    #include "canvas-client.h"


//  app

    #include "vec-2.h"



void main ()
{
    Size (100, 100);

    Vec2 p (10, 10);
    Vec2 u (2, 1);

    for (int lambda = 0; lambda <= 30; ++lambda)
    {
        Vec2 r = p + lambda * u;
        Pixel (r.x(), r.y(), 1, 0.5, 0);
    }
}
