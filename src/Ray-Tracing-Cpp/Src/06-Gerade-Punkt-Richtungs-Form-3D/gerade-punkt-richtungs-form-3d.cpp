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

    double zmin = 0;
    double zmax = 100;

    Vec3 p (40, 30, 10);
    Vec3 u (-3, 2, 2);

    for (int lambda = -9; lambda <= 9; ++lambda)
    {
        Vec3 s = p + lambda * u;
        double c = (zmax - s.z()) / (zmax - zmin);
        Pixel (s.x(), s.y(), c, 0, c);
    }
}
