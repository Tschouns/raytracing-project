#ifndef THREAD_H_INCLUDED
#define THREAD_H_INCLUDED



//  qt

    #include <QColor>
    #include <QObject>
    #include <QPoint>



class Thread
    : public QObject
{
        Q_OBJECT


        QThread * ThrdPtr;


        static int D2B (double d);


    protected:

        void Fill (double r, double g, double b);
        void Pixel (int x, int y, double r, double g, double b);


    public:

        Thread ();
        virtual ~ Thread ();

        Thread (const Thread &) = delete;
        Thread & operator = (const Thread &) = delete;

        void start ();
        virtual void run () = 0;


    signals:

        void sigFill (const QColor c);
        void sigPixel (QPoint p, QColor c);


    private slots:

        void SltRun ();
};



#endif
