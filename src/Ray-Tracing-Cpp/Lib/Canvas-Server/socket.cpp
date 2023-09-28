//  self

    #include "socket.h"


//  app

    #include "canvas.h"
    #include "connections.h"


//  c++

    #include <sstream>

    using namespace std;



Socket :: Socket (Connections * srvptr, Canvas * cnvptr)
{
    connect (this, & Socket::sigSize,
        cnvptr, & Canvas::sltSize,
        Qt::QueuedConnection);

    connect (this, & Socket::sigFill,
        cnvptr, & Canvas::sltFill,
        Qt::QueuedConnection);

    connect (this, & Socket::sigPixel,
        cnvptr, & Canvas::sltPixel,
        Qt::QueuedConnection);

    connect (this, & Socket::sigClose1,
        srvptr, & Connections::sltCloseSocket,
            Qt::QueuedConnection);
}



Socket :: ~ Socket ()
{
    emit sigClose0();
}



int Socket :: D2B (double d)
{
    int b = int (255.0 * d + 0.5);
    return clamp (b, 0, 255);
}



QColor Socket :: RGB2C (double r, double g, double b)
{
    return QColor (D2B (r), D2B (g), D2B (b));
}



void Socket :: ProcessCommand (const string & cmd)
{
    istringstream inp (cmd);
    string key;
    if (inp >> key)
        ProcessPixel (key, inp) ||
        ProcessFill (key, inp) ||
        ProcessSize (key, inp) ||
        ProcessQuit (key, inp);
}



bool Socket :: ProcessSize (const string & key, istream & par)
{
    if (key == "size" || key == "s")
    {
        int w, h;
        if (par >> w >> h)
        {
            QSize s (w, h);
            emit sigSize (s);
        }
        return true;
    }

    return false;
}



bool Socket :: ProcessFill (const string & key, istream & par)
{
    if (key == "fill" || key == "f")
    {
        double r, g, b;
        if (! (par >> r >> g >> b))
            r = g = b = 0;
        QColor c = RGB2C (r, g, b);
        emit sigFill (c);
        return true;
    }

    return false;
}



bool Socket :: ProcessPixel (const string & key, istream & par)
{
    if (key == "pixel" || key == "p")
    {
        int x, y;
        double r, g, b;
        if (par >> x >> y >> r >> g >> b)
        {
            QPoint p (x, y);
            QColor c = RGB2C (r, g, b);
            emit sigPixel (p, c);
        }
        return true;
    }

    return false;
}



bool Socket :: ProcessQuit (const string & key, std::istream &)
{
    if (key == "quit" || key == "q")
    {
        Close();
        return true;
    }

    return false;
}



void Socket :: ProcessBuffer (const char * buf)
{
    for (const char * p = buf; *p; ++p)
        switch (*p)
        {
            default:
                Buffer += *p;
                break;
            case '\r':
                ProcessCommand (Buffer);
                Buffer.clear();
                break;
            case '\n':
                break;
            }
}



void Socket :: Close ()
{
    emit sigClose1 (this);
}
