//  self

    #include "canvas-client.h"


//  app

    #include "vec-2.h"


//  c++

    #include <iostream>

    using namespace std;



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

    Vec2 q (20, 70);
    Vec2 v (1, -2);

    for (int lambda = 0; lambda <= 30; ++lambda)
    {
        Vec2 r = q + lambda * v;
        Pixel (r.x(), r.y(), 0, 0.5, 1);
    }

    //  p + lambda * u = q + mu * v
    //  lambda * u - mu * v = q - p = r

    Vec2 r = q - p;
    double d = Det (u, -v);
    double lambda = Det (r, -v) / d;
    double mu = Det (u, r) / d;

    Vec2 s1 = p + lambda * u;
    Vec2 s2 = q + mu * v;

    cerr << s1.x() << " " << s1.y() << endl;
    cerr << s2.x() << " " << s2.y() << endl;

    Pixel (s1.x(), s1.y(), 0, 1, 0);
}
