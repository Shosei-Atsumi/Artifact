from PIL import Image
import pyocr
import pytesseract

class OcrUtility(object):
    def __init__(self):
        # Set the path to Tesseract-OCR
        pyocr.tesseract.TESSERACT_CMD = r'C:\\Program Files\\Tesseract-OCR\\tesseract.exe'

        engines = pyocr.get_available_tools()
        self.engine = engines[0]

    def Read(self):
        txt = self.engine.image_to_string(Image.open('cap.png'), lang="eng")
        return txt


