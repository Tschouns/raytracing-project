//  self

    #include "canvas-client.h"


//  app

    #include "vec-3.h"


//  c++

    #include <iostream>

    using namespace std;



void main ()
{
    Size (100, 100);
    Fill (0.0, 0.0, 0.4);

    Vec3 p (50, 40, 10);
    Vec3 u (3, 2, 1);
    Vec3 v (2, -1, 1);

    double zmin = 0;
    double zmax = 100;

    double r [] = { 1, 1, 0, 1 };
    double g [] = { 1, 0, 1, 1 };
    double b [] = { 1, 0, 0, 0 };

    for (int lambda = -9; lambda <= 9; ++lambda)
        for (int mu = -9; mu <= 9; ++mu)
        {
            Vec3 s = p + lambda * u + mu * v;
            double c = (zmax - s.z()) / (zmax - zmin);
            int f = 0;
            if (lambda == 0 && mu >  0) f |= 1;
            if (lambda >  0 && mu == 0) f |= 2;
            double d = (lambda < 0 || mu < 0) ? 0 : c;

            Pixel (s.x(), s.y(), d * r [f], c * g [f], c * b [f]);
        }
}
