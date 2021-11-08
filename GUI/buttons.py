# basic GUI with button
# prints message when you click the button

import sys
from PyQt5.QtCore import *
from PyQt5.QtGui import *
from PyQt5.QtWidgets import *

def window():
    app = QApplication(sys.argv)
    w = QWidget()
    b = QLabel(w)

    b.setText("Hello World!")
    w.setGeometry(100,100,400,400)
    b.move(50,20)
    w.setWindowTitle("PyQt5")

    button = QPushButton(w)
    button.setText("Click me!")
    button.move(100,100)
    button.clicked.connect(button_click)
    
    w.show()
    sys.exit(app.exec_())

def button_click():
    print("We're no strangers to love")

if __name__ == '__main__':
    window()