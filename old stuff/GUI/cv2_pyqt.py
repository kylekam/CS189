import sys
from PyQt5 import QtWidgets
from PyQt5.QtWidgets import *
from PyQt5.QtGui import *
from PyQt5.QtCore import *
import cv2

class Worker1(QThread):
    ImageUpdate = pyqtSignal(QImage)
    def run(self):
        self.ThreadActive = True
        Capture = cv2.VideoCapture(0)
        while self.ThreadActive:
            ret, frame = Capture.read()
            if ret:
                Image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
                FlippedImage = cv2.flip(Image, 1)
                ConvertToQtFormat = QImage(FlippedImage.data, FlippedImage.shape[1], FlippedImage.shape[0], QImage.Format_RGB888)
                Pic = ConvertToQtFormat.scaled(640, 480, Qt.KeepAspectRatio)
                self.ImageUpdate.emit(Pic)

    def stop(self):
        self.ThreadActive = False
        self.quit()

class MainWindow(QWidget):
    def __init__(self):
        super(MainWindow, self).__init__()
        self.VBL = QVBoxLayout()
        self.FeedLabel = QLabel()
        self.VBL.addWidget(self.FeedLabel)
        self.setLayout(self.VBL)
        self.menu()
        
        self.setWindowTitle("Sea++")
        self.setGeometry(0, 0, 800, 500)

        # Add thread for video stream
        self.Worker1 = Worker1()
        self.Worker1.start()
        self.Worker1.ImageUpdate.connect(self.ImageUpdateSlot)


    def ImageUpdateSlot(self,Image):
        self.FeedLabel.setPixmap(QPixmap.fromImage(Image))
    
    def CancelFeed(self):
        self.Worker1.stop()
    
    def arrowBTNAction(self):
        print("Arrow Button Clicked!")
        self.FeedLabel.setCursor(QCursor(Qt.UpArrowCursor))

    def highlightBTNAction(self):
        print("Highlight Button Clicked!")
        self.FeedLabel.setCursor(QCursor(Qt.CrossCursor))
    
    def menu(self):
        self.container = QWidget()
        self.container.setLayout(QGridLayout())

        # Add cancel button
        self.CancelBTN = QPushButton("Cancel")
        self.CancelBTN.clicked.connect(self.CancelFeed)
        #self.VBL.addWidget(self.CancelBTN)

        # Add arrow button
        self.ArrowBTN = QPushButton("Arrow")
        #self.VBL.addWidget(self.b1)
        self.ArrowBTN.clicked.connect(self.arrowBTNAction)

        # Add highlight button
        self.HighlightBTN = QPushButton("Highlight")
        self.HighlightBTN.clicked.connect(self.highlightBTNAction)

        self.container.layout().addWidget(self.CancelBTN,0,0)
        self.container.layout().addWidget(self.ArrowBTN,0,1)
        self.container.layout().addWidget(self.HighlightBTN,0,2)
        self.layout().addWidget(self.container)

    def mousePressEvent(self, QMouseEvent):
        if self.FeedLabel.x() < QMouseEvent.x() and \
            QMouseEvent.x() - self.FeedLabel.x() < self.FeedLabel.size().width() and \
            self.FeedLabel.y() < QMouseEvent.y() and \
            QMouseEvent.y() - self.FeedLabel.y() < self.FeedLabel.size().height():
            
            # prints relative coordinates of mouse click
            # if inside the FeedLabel 
            print(QMouseEvent.pos() - self.FeedLabel.pos())


if __name__ == "__main__":
    App = QApplication(sys.argv)
    Root = MainWindow()
    Root.show()
    sys.exit(App.exec())
