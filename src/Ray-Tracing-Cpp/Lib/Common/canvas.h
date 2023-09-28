#ifndef CANVAS_H_INCLUDED
#define CANVAS_H_INCLUDED



//  qt

    #include <QLabel>



class Canvas
    : public QLabel
{
        Q_OBJECT

        QColor DefaultColor;
        QImage Image;
        QPixmap Pixmap;
        bool Changed;
        QTimer * TimePtr;


        void MarkChanged ();
        void Show ();


    protected:

        void resizeEvent (QResizeEvent * event) override;


    public:

        Canvas (QWidget * parent);
        ~ Canvas ();

        Canvas (const Canvas &) = delete;
        Canvas & operator = (const Canvas &) = delete;

        std::string info () const;


    private:

        void SltShow ();


    signals:

        void sigInfo ();


    public slots:

        void sltSize (const QSize s);
        void sltFill (const QColor c);
        void sltPixel (const QPoint p, const QColor c);
        void sltSave (const QString filename);
};



#endif
