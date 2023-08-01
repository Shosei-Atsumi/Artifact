from GetScreenCapture import GetScreenCapture
from ChromeController import ChromeController
from OcrUtility import OcrUtility

getScreenCapture = GetScreenCapture();
chromeController = ChromeController();
ocrUtility = OcrUtility();

class Main:
    def Main(self):
        #getScreenCapture.GetCapture("cap1")
        txt = ocrUtility.Read()
        chromeController.Search(txt)
        var = input("Please input variable : ")


if __name__ == '__main__':
    main = Main()
    main.Main()