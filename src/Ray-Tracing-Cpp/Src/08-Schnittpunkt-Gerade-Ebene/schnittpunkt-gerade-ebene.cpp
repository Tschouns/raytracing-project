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


    Vec3 q (50, 40, 10);
    Vec3 v (3, 2, 1);
    Vec3 w (2, -1, 1);

    double r [] = { 1, 1, 0, 1 };
    double g [] = { 1, 0, 1, 1 };
    double b [] = { 1, 0, 0, 0 };

    for (int mu = -9; mu <= 9; ++mu)
        for (int nu = -9; nu <= 9; ++nu)
        {
            Vec3 s = q + mu * v + nu * w;
            double c = (zmax - s.z()) / (zmax - zmin);
            int f = 0;
            if (mu == 0 && nu >  0) f |= 1;
            if (mu >  0 && nu == 0) f |= 2;

            double d = (mu < 0 || nu < 0) ? 0 : c;

            Pixel (s.x(), s.y(), d * r [f], c * g [f], c * b [f]);
        }

    Vec3 t = q - p;
    double D = Det (u, -v, -w);
    cerr << D << endl;
    double Lambda = Det (t, -v, -w) / D;
    double Mu = Det (u, t, -w) / D;
    double Nu = Det (u, -v, t) / D;

    Vec3 s1 = p + Lambda * u;
    Vec3 s2 = q + Mu * v + Nu * w;
    cerr << s1 << endl;
    cerr << s2 << endl;

    Pixel (s1.x(), s1.y(), 1, 0, 0);
}
