#ifndef PORT_DIALOG_H_INCLUDED
#define PORT_DIALOG_H_INCLUDED



//  qt designer


    namespace Ui
    {
        class PortDialog;
    }


//  qt

    #include <QDialog>



class PortDialog
    : public QDialog
{
        Q_OBJECT


        Ui::PortDialog *UiPtr;


    public:

        explicit PortDialog (QWidget * parent = nullptr);
        ~ PortDialog ();

        int tcpPort () const;
        void setTcpPort (int port);

        int webPort () const;
        void setWebPort (int port);
};



#endif
