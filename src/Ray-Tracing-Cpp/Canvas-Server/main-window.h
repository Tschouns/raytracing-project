#ifndef MAIN_WINDOW_H_INCLUDED
#define MAIN_WINDOW_H_INCLUDED



//  app

    class Connections;
    class PortDialog;


//  qt

    class QLabel;


//  qt creator

    namespace Ui
    {
        class MainWindow;
    }


//  qt

    class QFileDialog;

    #include <QMainWindow>



class MainWindow
    : public QMainWindow
{
        Q_OBJECT

        Ui::MainWindow * UiPtr;
        QLabel * LblInfoPtr;
        Connections * ConnPtr;
        QFileDialog * FileDlgPtr;
        PortDialog * PortDlgPtr;


    public:

         MainWindow (QWidget * parent = nullptr);
        ~MainWindow ();

        MainWindow (const MainWindow &) = delete;
        MainWindow & operator = (const MainWindow &) = delete;


    private slots:

        void SltRaise ();
        void SltSave ();
        void SltPorts ();
        void SltInfo ();
};



#endif
