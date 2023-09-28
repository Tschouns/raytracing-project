#ifndef MAIN_WINDOW_H_INCLUDED
#define MAIN_WINDOW_H_INCLUDED



//  app

    #include "thread-ptr.h"


//  qt designer

    namespace Ui { class MainWindow; }


//  qt

    #include <QMainWindow>



class MainWindow
    : public QMainWindow
{
        Q_OBJECT


        Ui::MainWindow * UiPtr;
        std::vector <ThreadPtr> AppPtrs;


    public:

        MainWindow (QWidget * parent = nullptr);
        ~ MainWindow ();

        MainWindow (const MainWindow &) = delete;
        MainWindow & operator = (const MainWindow &) = delete;

        void setCanvasSize (int w, int h);
        void addThread (ThreadPtr tptr);


    signals:

        void sigCanvasSize (const QSize s);
};



#endif
