//  self

    #include "canvas.h"


//  app

    #include <QTimer>


//  c++

    using namespace std;



Canvas :: Canvas (QWidget * parent)
    : QLabel (parent)
    , DefaultColor (palette() .color (QPalette::Window))
    , Image (15, 10, QImage::Format_RGB32)
    , Changed (false)
    , TimePtr (new QTimer)
{
    Image.fill (DefaultColor);

    TimePtr->setInterval (100);
    TimePtr->setSingleShot (true);

    connect (TimePtr, & QTimer::timeout,
        this, & Canvas::SltShow,
            Qt::QueuedConnection);
}



Canvas :: ~ Canvas ()
{
    TimePtr->stop();
    delete TimePtr;
}



void Canvas :: MarkChanged ()
{
    if (! Changed)
    {
        Changed = true;
        TimePtr->start();
    }
}



void Canvas :: Show ()
{
    if (! Pixmap.isNull())
    {
        QPixmap pixmap = Pixmap.scaled (size(), Qt::KeepAspectRatio);
        setPixmap (pixmap);
    }
}



void Canvas :: resizeEvent (QResizeEvent * event)
{
    QLabel::resizeEvent (event);
    Show();
}



string Canvas :: info () const
{
    string w = to_string (Image.width());
    string h = to_string (Image.height());
    return w + " x " + h;
}



void Canvas :: SltShow ()
{
    Changed = false;
    Pixmap = QPixmap::fromImage (Image);
    Show();
}



void Canvas :: sltSize (const QSize s)
{
    Image = QImage (s, QImage::Format_RGB32);
    sltFill (DefaultColor);
    emit sigInfo();
}



void Canvas :: sltFill (const QColor c)
{
    Image.fill (c);
    MarkChanged();
}



void Canvas :: sltPixel (const QPoint p, const QColor c)
{
    if (Image.valid (p))
    {
        Image.setPixelColor (p, c);
        MarkChanged();
    }
}



void Canvas :: sltSave (const QString filename)
{
    Image.save (filename, "tiff");
}
