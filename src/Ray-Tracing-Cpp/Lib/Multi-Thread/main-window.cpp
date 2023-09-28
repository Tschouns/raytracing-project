//  self

    #include "main-window.h"


//  app

    #include "thread.h"


//  qt designer

    #include "ui_main-window.h"



MainWindow :: MainWindow (QWidget * parent)
    : QMainWindow (parent)
    , UiPtr (new Ui::MainWindow)
{
    UiPtr->setupUi (this);

    connect (this, & MainWindow::sigCanvasSize,
        UiPtr->Cnv, & Canvas::sltSize,
        Qt::QueuedConnection);
}



MainWindow :: ~ MainWindow ()
{
    delete UiPtr;
}



void MainWindow :: setCanvasSize (int w, int h)
{
    QSize s (w, h);
    emit sigCanvasSize (s);
}



void MainWindow :: addThread (ThreadPtr tptr)
{
    AppPtrs.push_back (tptr);

    connect (tptr.get(), & Thread::sigFill,
        UiPtr->Cnv, & Canvas::sltFill,
        Qt::QueuedConnection);

    connect (tptr.get(), & Thread::sigPixel,
        UiPtr->Cnv, & Canvas::sltPixel,
        Qt::QueuedConnection);

    tptr->start();
}
