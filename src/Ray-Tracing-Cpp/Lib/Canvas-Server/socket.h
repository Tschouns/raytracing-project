#ifndef SOCKET_H_INCLUDED
#define SOCKET_H_INCLUDED



//  app

    class Canvas;
    class Connections;


//  qt

    #include <QColor>
    #include <QObject>
    #include <QPoint>
    #include <QSize>



class Socket
    : public QObject
{
        Q_OBJECT


        std::string Buffer;


        static int D2B (double d);
        static QColor RGB2C (double r, double g, double b);

        void ProcessCommand (const std::string & cmd);
        bool ProcessSize (const std::string & key, std::istream & par);
        bool ProcessFill (const std::string & key, std::istream & par);
        bool ProcessPixel (const std::string & key, std::istream & par);
        bool ProcessQuit (const std::string & key, std::istream & par);


    protected:

        void ProcessBuffer (const char * buffer);
        void Close ();


    public:

        Socket (Connections * srvptr, Canvas * cnvptr);
        virtual ~ Socket ();


    signals:

        void sigSize (QSize s);
        void sigFill (QColor c);
        void sigPixel (QPoint p, QColor c);
        void sigClose0 ();
        void sigClose1 (Socket * sockptr);
};



#endif
