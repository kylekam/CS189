# basic GUI with button
# prints message when you click the button

import sys
from PyQt5.QtCore import *
from PyQt5.QtGui import *
from PyQt5.QtWidgets import *

def window():
    app = QApplication(sys.argv)
    window = QWidget()
    msg = QLabel(window)

    msg.setText("Hello World!")
    window.setGeometry(100,100,400,400)
    msg.move(50,20)
    window.setWindowTitle("PyQt5")

    button = QPushButton(window)
    button.setText("Click me!")
    button.move(100,100)
    button.clicked.connect(button_click)
    
    window.show()
    sys.exit(app.exec_())

def button_click():
    print("We're no strangers to love")

if __name__ == '__main__':
    window()