//  self

    #include "vec-3.h"



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
    return Y;
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
