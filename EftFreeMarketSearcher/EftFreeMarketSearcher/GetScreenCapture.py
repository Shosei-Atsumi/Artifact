from PIL import ImageGrab
import pyautogui

class GetScreenCapture(object):
    def GetCapture(self):
        x, y = pyautogui.position()
        # cap save
        ImageGrab.grab().save(self.GetMousePos() + "capture.png")

    def GetCapture(self, name):
        x, y = pyautogui.position()
        # cap save
        ImageGrab.grab().save(self.GetMousePos() + name + ".png")

    def GetMousePos(self):
        x, y = pyautogui.position()
        return str(x) + "_" + str(y) + "_"