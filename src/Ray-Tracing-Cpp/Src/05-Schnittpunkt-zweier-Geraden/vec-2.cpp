//  self

    #include "vec-2.h"



Vec2 :: Vec2 ()
    : Vec2 (0, 0)
{
}



Vec2 :: Vec2 (double x, double y)
    : X (x)
    , Y (y)
{
}



double Vec2 :: x () const
{
    return X;
}



double Vec2 :: y () const
{
    return Y;
}



Vec2 Vec2 :: operator - () const
{
    return Vec2 (-X, -Y);
}



Vec2 operator + (const Vec2 & u, const Vec2 & v)
{
    return Vec2
    (
        u.X + v.X,
        u.Y + v.Y
    );
}



Vec2 operator - (const Vec2 & u, const Vec2 & v)
{
    return Vec2
    (
        u.X - v.X,
        u.Y - v.Y
    );
}



Vec2 operator * (double s, const Vec2 & u)
{
    return Vec2
    (
        s * u.X,
        s * u.Y
    );
}



double Det (const Vec2 & u, const Vec2 & v)
{
    return u.x() * v.y() - u.y() * v.x();
}
