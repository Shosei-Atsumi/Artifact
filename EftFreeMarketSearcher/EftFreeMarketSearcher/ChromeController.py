# coding: utf-8
import sys
import configparser
import lxml
import re
import time
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from bs4 import BeautifulSoup
import random

from LogController import LogController
from BsUtility import BsUtility
from DriverUtility import DriverUtility

class ChromeController(object):
    def __init__(self):
        self.DEBUG = True
        self.ID = 'id'
        self.CLASS = 'class'
        self.NAME = 'name'
        self.X_PATH = 'x_path'
        self.CSSSELECTOR = 'css_selector'
        
        self.driver = None
        self.generalConfig = None

        self.startTime = None
        self.logController = None

        self.argvs = sys.argv

        self.Initialize()
        self.driverUtility = DriverUtility(self.driver)

     # 初期化
    def DriverInitialize(self, profile_path):
        op = Options()
        op.add_argument("--disable-gpu")
        op.add_argument("--disable-extensions")
        op.add_argument("--proxy-server='direct://'")
        op.add_argument("--proxy-bypass-list=*")
        op.add_argument("--start-maximized")
        op.add_argument("--user-agent=Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36")
        op.add_argument("--user-data-dir=" + profile_path)
        op.add_argument("--ignore-certificate-errors-spki-list")
        op.add_argument("--ignore-ssl-errors")
        op.add_experimental_option("excludeSwitches", ["enable-automation"])
        op.add_experimental_option('useAutomationExtension', False)
       
        
        capabilities = DesiredCapabilities.CHROME.copy()
        capabilities['acceptInsecureCerts'] = True
        if(len(sys.argv) == 1):
            self.driver = webdriver.Chrome(options=op, desired_capabilities=capabilities)
        else:
            self.driver = webdriver.Chrome(executable_path=sys.argv[1], options=op, desired_capabilities=capabilities)
        self.driver.execute_script('const newProto = navigator.__proto__;delete newProto.webdriver;navigator.__proto__ = newProto;')
        self.driver.execute_cdp_cmd("Page.addScriptToEvaluateOnNewDocument", {
            "source": """
                Object.defineProperty(navigator, 'webdriver', {
                    get: () => undefined
                })
            """
        })

        self.driver.set_window_position(0,0)
        self.driver.set_window_size(1024, 1024)
        self.driver.implicitly_wait(10)

    def Initialize(self):
        # 設定読み込み
        config_ini = configparser.ConfigParser()
        if(len(sys.argv) == 1):
            config_ini.read('config.ini', encoding='shift_jis')
        else:
            config_ini.read(sys.argv[2], encoding='shift_jis')
        self.LoadGeneralConfig(config_ini)
        self.logController = LogController(self.generalConfig['profile_name'], self.generalConfig['line_notify_token'])
        
        # driver初期化
        self.DriverInitialize(self.generalConfig['profile_path'])

        # 開始時間取得
        self.startTime = time.time()

     # general設定読み込み
    def LoadGeneralConfig(self, config_ini):
        # profile name
        profile_name = config_ini['General']['profile_name']
        # line notify token
        line_notify_token = config_ini['General']['line_notify_token']
        # chrome profile path
        profilePath = config_ini['General']['profile_path']
        # Search page
        searchPageUrl = config_ini['General']['searchPageUrl']

    
        if(profilePath == ''):
            self.logController.InfoLog('profile_pathが未設定です。')
            sys.exit()
    
        config_dic = {
            'profile_name': profile_name,
            'line_notify_token': line_notify_token,
            'profile_path': profilePath,
            'search_page_url': searchPageUrl,
        }
    
        self.generalConfig = config_dic

    def Main(self):
        self.Initialize()
        self.driverUtility = DriverUtility(self.driver)

    def Search(self, value):
        self.driverUtility = DriverUtility(self.driver)
        self.driverUtility.LoadPage(self.generalConfig['search_page_url'],  self.CLASS, "search", True)

        el = self.driverUtility.GetElementSearchInput()
        el.send_keys(value)



# エントリーポイント
if __name__ == '__main__':
    try:
        chromeController = ChromeController()
        chromeController.Main()
    except Exception as e:
        tb = sys.exc_info()[2]
        chromeController.logController.ExceptLog(0, str(e.with_traceback(tb)))
        sys.exit( )
    
    nowTime = time.time()
    nowSec = nowTime - chromeController.startTime
    nomMinutes = int(nowSec/60)
    chromeController.logController.InfoLog("RunTime" + str(nomMinutes) + "分")



