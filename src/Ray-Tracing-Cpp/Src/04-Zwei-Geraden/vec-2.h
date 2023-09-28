#ifndef VEC_2_H_INCLUDED
#define VEC_2_H_INCLUDED



class Vec2
{
        double X, Y;


    public:

        Vec2 ();
        Vec2 (double x, double y);

        double x () const;
        double y () const;

        friend Vec2 operator + (const Vec2 & u, const Vec2 & v);
        friend Vec2 operator * (double s, const Vec2 & u);
};



#endif
