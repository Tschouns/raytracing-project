//  self

    #include "main.h"


//  app

    #include "thread.h"
    #include "main-window.h"



//  qt

    #include <QApplication>
    #include <QStyleFactory>


//  c++

    #include <iostream>

    using namespace std;



MainWindow * MainWindowPtr;



void Size (int w, int h)
{
    MainWindowPtr->setCanvasSize (w, h);
}



void AddThread (Thread * tptr)
{
    ThreadPtr sp (tptr);
    MainWindowPtr->addThread (sp);
}



int main (int argc, char * argv [])
{
    qRegisterMetaType <std::string> ("std::string");

    QApplication::setStyle (QStyleFactory::create ("Fusion"));
    QApplication app (argc, argv);

    MainWindowPtr = new MainWindow;
    QApplication::setFont (MainWindowPtr->font());
    MainWindowPtr->setGeometry (600, 100, 800, 600);
    MainWindowPtr->show();

    MyMain();
    int res = app.exec();

    QRect r = MainWindowPtr->geometry();
    cerr << "geometry (" << r.x() << ", " << r.y() << ", " << r.width() << ", " << r.height() << ")" << endl;
    delete MainWindowPtr;

    return res;
}
