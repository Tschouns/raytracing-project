#ifndef CONFIG_H_INCLUDED
#define CONFIG_H_INCLUDED



//  qt

    #include <QRect>



class Config;

typedef std::shared_ptr <Config> ConfigPtr;



class Config
{
        static ConfigPtr InstancePtr;
        static const std::string Filename;

        QPoint Position;
        QSize Size;
        int TcpPort;
        int WebPort;
        QString ImgPath;


        void ReadPosition (std::istream & file);
        void ReadSize (std::istream & file);
        void ReadPort (std::istream & file, int & port);
        void ReadImgPath (std::istream & file);


    public:

        Config ();
        ~ Config ();

        static ConfigPtr get ();

        QRect geometry () const;
        void setGeometry (const QRect & geometry);

        int tcpPort () const;
        void setTcpPort (int port);

        int webPort () const;
        void setWebPort (int port);

        QString imgPath () const;
        void setImgPath (const QString & imgpath);
};



#endif
