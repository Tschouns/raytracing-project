//  self

    #include "thread.h"


//  qt

    #include <QThread>


//  c++

    using namespace std;
    
    
    
    Thread :: Thread ()
    : ThrdPtr (new QThread)
{
    connect (ThrdPtr, & QThread::started,
            this, & Thread::SltRun,
        Qt::QueuedConnection);

    // connect (ThrdPtr, & QThread::finished,
    //     this, & QObject::deleteLater,
    //     Qt::QueuedConnection);

    moveToThread (ThrdPtr);
}



Thread :: ~ Thread ()
{
    ThrdPtr->terminate();
    ThrdPtr->wait();
    delete ThrdPtr;
}



int Thread :: D2B (double d)
{
    int i = int (255.0 * d + 0.5);
    return clamp (i, 0, 255);
}



void Thread :: Fill (double r, double g, double b)
{
    QColor c (D2B (r), D2B (g), D2B (b));
    emit sigFill (c);
}



void Thread :: Pixel (int x, int y, double r, double g, double b)
{
    QPoint p (x, y);
    QColor c (D2B (r), D2B (g), D2B (b));
    emit sigPixel (p, c);
}



void Thread :: start ()
{
    ThrdPtr->start (QThread::IdlePriority);
}



void Thread :: SltRun ()
{
    run();
}
