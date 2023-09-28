//  self

    #include "vec-3.h"


//  c++

    #include <iomanip>

    using namespace std;



Vec3 :: Vec3 ()
    : Vec3 (0, 0, 0)
{
}



Vec3 :: Vec3 (double x, double y, double z)
    : X (x)
    , Y (y)
    , Z (z)
{
}



double Vec3 :: x () const
{
    return X;
}



double Vec3 :: y () const
{
    return Y;
}



double Vec3 :: z () const
{
    return Z;
}



Vec3 Vec3 :: operator - () const
{
    return Vec3 (-X, -Y, -Z);
}



Vec3 operator + (const Vec3 & u, const Vec3 & v)
{
    return Vec3
    (
        u.X + v.X,
        u.Y + v.Y,
        u.Z + v.Z
    );
}



Vec3 operator - (const Vec3 & u, const Vec3 & v)
{
    return Vec3
    (
        u.X - v.X,
        u.Y - v.Y,
        u.Z - v.Z
    );
}



Vec3 operator * (double s, const Vec3 & u)
{
    return Vec3
    (
        s * u.X,
        s * u.Y,
        s * u.Z
    );
}



ostream & operator << (ostream & stream, const Vec3 & v)
{
    auto w = setw (stream.width());
    return stream << w << v.x() << ' ' << w << v.y() << ' ' << w << v.z();
}



//  ux vx wx | ux vx
//  uy vy wy | uy vy
//  uz vz wz | uz vz

double Det (const Vec3 & u, const Vec3 & v, const Vec3 & w)
{
    return u.x() * v.y() * w.z() - u.z() * v.y() * w.x()
         + v.x() * w.y() * u.z() - v.z() * w.y() * u.x()
         + w.x() * u.y() * v.z() - w.z() * u.y() * v.x();
}
