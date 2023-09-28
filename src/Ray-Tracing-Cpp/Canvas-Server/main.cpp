//  app

    #include "config.h"
    #include "main-window.h"


//  qt

    #include <QApplication>
    #include <QStyleFactory>



int main (int argc, char * argv [])
{
    qRegisterMetaType <std::string> ("std::string");

    QApplication::setStyle (QStyleFactory::create ("Fusion"));
    QApplication app (argc, argv);

    MainWindow win;
    QApplication::setFont (win.font());
    ConfigPtr cfgptr = Config::get();
    win.setGeometry (cfgptr->geometry());
    win.show();

    int res = app.exec();
    cfgptr->setGeometry (win.geometry());

    return res;
}
