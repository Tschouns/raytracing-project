#ifndef VEC_3_INCLUDED
#define VEC_3_INCLUDED



//  c++

    #include <ostream>



class Vec3
{
        double X, Y, Z;


    public:

        Vec3 ();
        Vec3 (double x, double y, double z);

        double x () const;
        double y () const;
        double z () const;

        Vec3 operator - () const;

        friend Vec3 operator + (const Vec3 & u, const Vec3 & v);
        friend Vec3 operator - (const Vec3 & u, const Vec3 & v);
        friend Vec3 operator * (double s, const Vec3 & u);
};



std::ostream & operator << (std::ostream & stream, const Vec3 & v);

double Det (const Vec3 & u, const Vec3 & v, const Vec3 & w);



#endif
