#ifndef VEC_2_INCLUDED
#define VEC_2_INCLUDED



class Vec2
{
        double X, Y;


    public:

        Vec2 ();
        Vec2 (double x, double y);

        double x () const;
        double y () const;

        Vec2 operator - () const;

        friend Vec2 operator + (const Vec2 & u, const Vec2 & v);
        friend Vec2 operator - (const Vec2 & u, const Vec2 & v);
        friend Vec2 operator * (double s, const Vec2 & u);
};



double Det (const Vec2 & u, const Vec2 & v);



#endif
