import lxml
import re
from bs4 import BeautifulSoup

class BsUtility:
    def __init__(self):
        self.DEBUG = True
        self.ID = 'id'
        self.CLASS = 'class'
        self.NAME = 'name'
        self.X_PATH = 'x_path'

    # soup取得
    def GetSoup(self, driver):
        soup = BeautifulSoup(driver.page_source, features='lxml')
        return soup

    # [soup]単体エレメント取得
    def GetElement(self, soup, selectorType, selector):
        el = None
        if(selectorType == self.ID):
            el = soup.find(id=selector)
        elif(selectorType == self.CLASS):
            el = soup.find(class_=selector)
        return el

    # [soup]全てのエレメント取得
    def GetElementAll(self, soup, selectorType, selector):
        el = None
        if(selectorType == self.ID):
            el = soup.find_all(id=selector)
        elif(selectorType == self.CLASS):
            el = soup.find_all(class_=selector)
        return el

    def GetElementSearchInput(self, soup):
        el = soup.find("input")

        return el

