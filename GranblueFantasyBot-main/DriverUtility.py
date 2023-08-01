import random
import time
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from selenium.webdriver.support.select import Select

class DriverUtility:
    def __init__(self, driver):
        self.DEBUG = True
        self.ID = 'id'
        self.CLASS = 'class'
        self.NAME = 'name'
        self.X_PATH = 'x_path'
        self.CSSSELECTOR = 'css_selector'
        self.driver = driver

    # [driver]全てのエレメント取得
    def GetDriverElement(self, selectorType, selector, xPathSelectorType='class'):
        el = None
        try:
            if(selectorType == self.ID):
                el = self.driver.find_element_by_id(selector)
            elif(selectorType == self.CLASS):
                el = self.driver.find_element_by_class_name(selector)
            elif(selectorType == self.NAME):
                el = self.driver.find_element_by_name(selector)
            elif(selectorType == self.X_PATH):
                xpath = "//*[@"+ xPathSelectorType +"='"+ selector +"']"
                el = self.driver.find_element_by_xpath(xpath)
            elif(selectorType ==  self.CSSSELECTOR):
                el = self.driver.find_element_by_css_selector(selector)
        except:
            print('要素なし')
    
        return el
    
    def FindDriverElementText(self, elment, text):
        el = None
        xpath = "//*[contains(text(), '"+ text +"')]"
        try:
            el = elment.find_element_by_xpath(xpath)
        except:
            el = None
        return el 
    
    # [driver]エレメント座標取得
    def GetElementPos(self, selectorType, selector):
        el = None
        el = self.GetDriverElement(selectorType, selector)
        return el.location
    
    # [driver]エレメントサイズ取得
    def GetElementSize(self, selectorType, selector):
        el = None
        el = self.GetDriverElement(selectorType, selector)
        return el.size

    def SelectDropBox(self, selector):
        dropdown = self.driver.find_element_by_class_name(selector)
        select = Select(dropdown)
        select.select_by_index(len(select.options)-2)

    # 対象エレメントをクリック
    def ClickElementByRandomPos(self, selectorType, selector):
        actions = ActionChains(self.driver)
        whole_page = self.driver.find_element_by_tag_name("html")
        actions.move_to_element_with_offset(whole_page, 0, 0)
        pos = self.GetElementPos(selectorType, selector)
        size = self.GetElementSize(selectorType, selector)
        x_pos = pos['x'] + random.randint(0, int(size['width']/3))
        y_pos = pos['y'] + random.randint(0, int(size['height']/3))
        actions.move_by_offset(x_pos, y_pos)
        actions.click()
        actions.perform()
    
    def ClickByRandomPos(self, pos_x, pos_y ,min= -5, max= 5):
        actions = ActionChains(self.driver)
        whole_page = self.driver.find_element_by_tag_name("html")
        actions.move_to_element_with_offset(whole_page, 0, 0)
        x_pos = pos_x + random.randint(min, max)
        y_pos = pos_y + random.randint(min, max)
        actions.move_by_offset(x_pos, y_pos)
        actions.click()
        actions.perform()
    
    
    # 対象エレメントへスクロール
    def ScrollElementByRandomPos(self, selectorType, selector):
        el = None
        if(selectorType == id):
            el = self.driver.find_element_by_id(selector)
        elif(selectorType == self.CLASS):
            el = self.driver.find_element_by_class_name(selector)
        elif(selectorType == name):
            el = self.driver.find_element_by_name(selector)
    
        pos_y = el.location['y']
        if(pos_y > 900):
            pos_y = random.randint(pos_y - 100, pos_y + 100)
            self.driver.execute_script("window.scrollTo(0, " + str(pos_y) + ");")

    # 対象ページへアクセス
    def LoadPage(self, URL, selectorType='', selector='', wait=False):
        self.driver.get(URL)
        if(wait):
            if(selectorType == self.CLASS):
                WebDriverWait(self.driver, 30).until(EC.presence_of_element_located((By.CSS_SELECTOR, 'div.' + selector)))

    def FindWait(self, selector):
        WebDriverWait(self.driver, 30).until(EC.presence_of_element_located((By.CSS_SELECTOR, 'div.' + selector)))

    def BackClick(self, posConfig):
        pos = posConfig['btn_back']
        self.ClickByRandomPos(pos[0], pos[1])

    def RestartClick(self, posConfig):
        pos = posConfig['btn_restart']
        self.ClickByRandomPos(pos[0], pos[1])
    
    def AttackClick(self, posConfig):
        pos = posConfig['btn_attack']
        self.ClickByRandomPos(pos[0], pos[1])
    
    def ReloadClick(self):
        self.driver.refresh()
    
    def CharaClick(self, posConfig,index=1):
        if(index == 1):
            pos = posConfig['chara_1']
        if(index == 2):
            pos = posConfig['chara_2']
        if(index == 3):
            pos = posConfig['chara_3']
        if(index == 4):
            pos = posConfig['chara_4']
    
        self.ClickByRandomPos(pos[0], pos[1])
    
    def AbilityClick(self, posConfig, index=1):
        if(index == 1):
            pos = posConfig['ability_1']
        if(index == 2):
            pos = posConfig['ability_2']
        if(index == 3):
            pos = posConfig['ability_3']
        if(index == 4):
            pos = posConfig['ability_4']
    
        self.ClickByRandomPos(pos[0], pos[1])

