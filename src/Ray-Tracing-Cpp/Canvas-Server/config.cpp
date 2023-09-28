//  self

    #include "config.h"


//  c++

    #include <fstream>
    #include <iostream>

    using namespace std;



const string Config::Filename = "config.txt";
ConfigPtr Config::InstancePtr = nullptr;



Config :: Config ()
    : Position (16, 64)
    , Size (600, 400)
    , TcpPort (9012)
    , WebPort (9013)
    , ImgPath (".")
{
    ifstream file (Filename);
    string key;

    while (file >> key)
        if (key == "position")
            ReadPosition (file);
        else if (key == "size")
            ReadSize (file);
        else if (key == "tcp-port")
            ReadPort (file, TcpPort);
        else if (key == "web-port")
            ReadPort (file, WebPort);
        else if (key == "image-path")
            ReadImgPath (file);

    if (TcpPort == WebPort)
        ++WebPort;
}



Config :: ~ Config ()
{
    ofstream file (Filename);

    file
    << "position     " << Position.x() << " " << Position.y() << endl
    << "size         " << Size.width() << " " << Size.height() << endl
    << "tcp-port     " << TcpPort << endl
    << "web-port     " << WebPort << endl
    << "image-path   " << ImgPath.toStdString() << endl
    ;
}



void Config :: ReadPosition (istream & file)
{
    int x = 0;
    int y = 0;

    if (file >> x >> y)
    {
        Position.setX (x);
        Position.setY (y);
    }
}



void Config :: ReadSize (istream & file)
{
    int w = 0;
    int h = 0;

    if (file >> w >> h)
    {
        Size.setWidth (w);
        Size.setHeight (h);
    }
}



void Config :: ReadPort (istream & file, int & port)
{
    int p;

    if (file >> p)
        port = p;
}



void Config :: ReadImgPath (istream & file)
{
    string str;
    getline (file, str);

    auto nospace = [] (char c) -> bool { return ! isspace (c); };
    auto pos0 = find_if (str.begin(), str.end(), nospace);
    if (pos0 != str.end())
    {
        auto pos1 = find_if (str.rbegin(), str.rend(), nospace);
        if (pos1 != str.rend())
        {
            ImgPath.clear();
            copy (pos0, pos1.base(), back_inserter (ImgPath));
        }
    }
}



ConfigPtr Config :: get ()
{
    if (InstancePtr == nullptr)
    {
        InstancePtr = make_shared <Config> ();
        if (InstancePtr == nullptr)
        {
            cerr << "Config: can't allocate Instance" << endl;
            exit (1);
        }
    }

    return InstancePtr;
}



QRect Config :: geometry() const
{
    return QRect (Position, Size);
}



void Config :: setGeometry (const QRect & geometry)
{
    Position = geometry.topLeft();
    Size = geometry.size();
}



int Config :: tcpPort () const
{
    return TcpPort;
}



void Config :: setTcpPort (int port)
{

    TcpPort = port == WebPort ? WebPort + 1 : port;
}



int Config :: webPort () const
{
    return WebPort;
}



void Config :: setWebPort (int port)
{
    WebPort = port == TcpPort ? TcpPort - 1 : port;
}



QString Config :: imgPath () const
{
    return ImgPath;
}



void Config :: setImgPath (const QString & imgpath)
{
    ImgPath = imgpath;
}
