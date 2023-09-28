#ifndef VEC_2_H_INCLUDED
#define VEC_2_H_INCLUDED



class Vec2
{
        double X, Y;


    public:

        Vec2 (double x, double y)
            : X (x)
            , Y (y)
        {
        }


        Vec2 ()
            : Vec2 (0, 0)
        {
        }


        double x () const
        {
            return X;
        }


        double y () const
        {
            return Y;
        }


        friend Vec2 operator + (const Vec2 & u, const Vec2 & v)
        {
            return Vec2 (u.X + v.X, u.Y + v.Y);
        };


        friend Vec2 operator * (double s, const Vec2 & u)
        {
            return Vec2 (s * u.X, s * u.Y);
        };
};



#endif
