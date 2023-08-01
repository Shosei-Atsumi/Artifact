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

class Main:
    def __init__(self):
        self.DEBUG = True
        self.ID = 'id'
        self.CLASS = 'class'
        self.NAME = 'name'
        self.X_PATH = 'x_path'
        self.CSSSELECTOR = 'css_selector'
        
        self.driver = None
        self.generalConfig = None
        self.configQuest = None
        self.posConfig = None

        self.startTime = None
        self.logController = None
        self.count = 0

        self.argvs = sys.argv

    # 初期化
    def init(self, profile_path):
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
        self.driver.set_window_size(500, 1024)
        self.driver.implicitly_wait(10)
    
    # general設定読み込み
    def LoadGeneralConfig(self, config_ini):
        # profile name
        profile_name = config_ini['General']['profile_name']
        # line notify token
        line_notify_token = config_ini['General']['line_notify_token']
        # chrome profile path
        profilePath = config_ini['General']['profile_path']
        # 対象クエスト
        quest = config_ini['General']['quest']
        # 最大実行時間
        runTimeStr = config_ini['General']['time']
        runTime = 0
    
        if(profilePath == ''):
            self.logController.InfoLog('profile_pathが未設定です。')
            sys.exit()
    
        if(quest == ''):
            self.logController.InfoLog('questが未設定です。')
            sys.exit()
        
        # 最大実行時間が未入力の場合
        if(runTimeStr == '' or runTimeStr == None):
            runTime = 0
        else:
            runTime = int(runTimeStr)
    
        config_dic = {
            'profile_name': profile_name,
            'line_notify_token': line_notify_token,
            'profile_path': profilePath,
            'quest': quest,
            'run_time': runTime
        }
    
        self.generalConfig = config_dic
    
    # クエスト設定読み込み
    def LoadQuestConfig(self, config_ini):
        # サポート石属性
        summon_attribute = config_ini['Quest']['summon_attribute']
        # サポート石名
        summon_name = config_ini['Quest']['summon_name']
        # 行動パターンモード
        action_mode =  config_ini['Quest']['action_mode']

        questAction = {}
        try:
            nowTurn = ''
            f = None
            if(len(sys.argv) == 1):
                f = open('QuestAction.txt', 'r', encoding='UTF-8')
            else:
                f = open(sys.argv[3], 'r', encoding='UTF-8')
            for s_line in f:
                if(re.search('^\s*\n', s_line)):
                    continue
                if(s_line == ''):
                    continue
                m = re.search('Turn+\d', s_line)
                if(m):
                    turn = {m.group(0): []}
                    questAction.update(turn)
                    nowTurn = re.search('\d+', s_line).group(0)
                    continue
                action = s_line.split(',')
                questAction['Turn'+nowTurn].append(int(action[0]))
                questAction['Turn'+nowTurn].append(int(action[1]))


        except:
            self.logController.InfoLog('行動設定ファイルなし')

        
        # サポート石の属性に合わせてセレクターセット
        if(summon_attribute == '火'):
            summon_attribute = 'icon-supporter-type-1'
        elif(summon_attribute == '水'):
            summon_attribute = 'icon-supporter-type-2'
        elif(summon_attribute == '土'):
            summon_attribute = 'icon-supporter-type-3'
        elif(summon_attribute == '風'):
            summon_attribute = 'icon-supporter-type-4'
        elif(summon_attribute == '光'):
            summon_attribute = 'icon-supporter-type-5'
        elif(summon_attribute == '闇'):
            summon_attribute = 'icon-supporter-type-6'
        elif(summon_attribute == 'フリー' or summon_attribute == ''):
            summon_attribute = 'icon-supporter-type-7'
    
        config_dic = {
            'summon_attribute': summon_attribute,
            'summon_name': summon_name,
            'action_mode': action_mode,
            'QuestAction': questAction
        }
        
        self.configQuest = config_dic
    
    def LoadPositionConfig(self, config_ini):
        # ボタン
        btn_back_pos_x = int(config_ini['Position']['btn_back_pos_x'])
        btn_back_pos_y = int(config_ini['Position']['btn_back_pos_y'])
        btn_attack_pos_x = int(config_ini['Position']['btn_attack_pos_x'])
        btn_attack_pos_y = int(config_ini['Position']['btn_attack_pos_y'])
        btn_reload_pos_x = int(config_ini['Position']['btn_reload_pos_x'])
        btn_reload_pos_y = int(config_ini['Position']['btn_reload_pos_y'])
        btn_restart_pos_x = int(config_ini['Position']['btn_restart_pos_x'])
        btn_restart_pos_y = int(config_ini['Position']['btn_restart_pos_y'])
    
        # キャラ
        chara_pos_x_1 = int(config_ini['Position']['chara_pos_x_1'])
        chara_pos_x_2 = int(config_ini['Position']['chara_pos_x_2'])
        chara_pos_x_3 = int(config_ini['Position']['chara_pos_x_3'])
        chara_pos_x_4 = int(config_ini['Position']['chara_pos_x_4'])
        chara_pos_y = int(config_ini['Position']['chara_pos_y'])
    
        # アビリティ
        ability_pos_x_1 = int(config_ini['Position']['ability_pos_x_1'])
        ability_pos_x_2 = int(config_ini['Position']['ability_pos_x_2'])
        ability_pos_x_3 = int(config_ini['Position']['ability_pos_x_3'])
        ability_pos_x_4 = int(config_ini['Position']['ability_pos_x_4'])
        ability_pos_y = int(config_ini['Position']['ability_pos_y'])
    
        config_dic = {
            'btn_back': [btn_back_pos_x, btn_back_pos_y],
            'btn_attack': [btn_attack_pos_x, btn_attack_pos_y],
            'btn_reload': [btn_reload_pos_x, btn_reload_pos_y],
            'btn_restart': [btn_restart_pos_x, btn_restart_pos_y],
            'chara_1': [chara_pos_x_1, chara_pos_y],
            'chara_2': [chara_pos_x_2, chara_pos_y],
            'chara_3': [chara_pos_x_3, chara_pos_y],
            'chara_4': [chara_pos_x_4, chara_pos_y],
            'ability_1': [ability_pos_x_1, ability_pos_y],
            'ability_2': [ability_pos_x_2, ability_pos_y],
            'ability_3': [ability_pos_x_3, ability_pos_y],
            'ability_4': [ability_pos_x_4, ability_pos_y],
        }
        
        self.posConfig = config_dic
    
    # wait param未設定時 最小1秒、最大3秒
    def RandomWait(self, min = 1, max = 3):
        waitTime = random.randint(min, max)
        time.sleep(waitTime)
    
    # waitPoint param未設定時 最小0.1秒、最大0.3秒
    def RandomWaitPoint(self, min = 1, max = 3):
        waitTime = random.uniform(min, max)
        time.sleep(waitTime)

    def initialize(self):
        # 設定読み込み
        config_ini = configparser.ConfigParser()
        if(len(sys.argv) == 1):
            config_ini.read('config.ini', encoding='shift_jis')
        else:
            config_ini.read(sys.argv[2], encoding='shift_jis')
        self.LoadGeneralConfig(config_ini)
        self.logController = LogController(self.generalConfig['profile_name'], self.generalConfig['line_notify_token'])
        self.LoadQuestConfig(config_ini)
        self.LoadPositionConfig(config_ini)
        
        # driver初期化
        self.init(self.generalConfig['profile_path'])

        # 開始時間取得
        self.startTime = time.time()

    def ReplenishmentAp(self, driverUtility):
        driverUtility.LoadPage('http://game.granbluefantasy.jp/#item', self.CLASS, 'prt-item-filter', True)
        self.RandomWaitPoint()
        driverUtility.ClickByRandomPos(225, 120)
        self.RandomWaitPoint()
        driverUtility.ClickByRandomPos(145, 195)
        self.RandomWaitPoint(2, 3)
        driverUtility.SelectDropBox('num-set')
        self.RandomWaitPoint()
        driverUtility.ClickElementByRandomPos(self.CLASS, 'btn-usual-use')
        self.RandomWaitPoint()
    
    def SelectSummon(self, driverUtility):
        bsUtility = BsUtility()
        driverUtility.LoadPage(self.generalConfig['quest'], self.CLASS, self.configQuest['summon_attribute'], True)
        summon_attribute = self.configQuest['summon_attribute']
        summon_name = self.configQuest['summon_name']

        driverUtility.ClickElementByRandomPos(self.CLASS, summon_attribute)
        self.RandomWaitPoint()
        driverUtility.ClickByRandomPos(250, 250, -30, 30)
        for i in range(3):
            self.RandomWaitPoint(2, 3)
            soup = bsUtility.GetSoup(self.driver)
            elAuthentication = None
            elAuthentication = bsUtility.GetElement(soup, self.CLASS, 'prt-popup-header')
            if(elAuthentication != None):
                self.logController.InfoLog('画像認証が発生したためプログラムを停止します。')
                self.logController.EndLog(self.count)
                while(True):
                    time.sleep(30)

            el = bsUtility.GetElement(soup, self.CLASS, 'txt-stamina-after')
            nowAp = 0
            if(el):
                m = re.search('\d+', str(el))
                if(m):
                    nowAp = m.group(0)
            else:
                continue

            if(int(nowAp) > 50):
                driverUtility.ClickByRandomPos(300, 430)
                self.RandomWaitPoint(1, 2)
                break
            else:
                self.ReplenishmentAp(driverUtility)
                break

    def GetNowTurn(self, bsUtility):
        while(True):
            soup = bsUtility.GetSoup(self.driver)  
            turnEl = bsUtility.GetElement(soup, self.CLASS, 'prt-turn-num')
            if(turnEl):
                m = re.search('\d+', str(turnEl))
                if(m):
                    turn = m.group(0)
                    return turn

    def GetExistResultBtn(self, bsUtility):
        while(True):
            soup = bsUtility.GetSoup(self.driver)  
            turnEl = bsUtility.GetElement(soup, self.CLASS, 'btn-result')
            for i in range(3):
                if(turnEl):
                    return True
            return False

    def GetExistRestartTxt(self, bsUtility):
        while(True):
            soup = bsUtility.GetSoup(self.driver)  
            turnEl = bsUtility.GetElement(soup, self.CLASS, 'txt-restart')
            for i in range(3):
                if(turnEl):
                    return True
            return False

    def RunQuest(self, driverUtility):
        turn = 0
        bsUtility = BsUtility()
        raidUrl = ''
        while(True):
            if(self.CheckNowPage('#raid')):
                raidUrl = self.driver.current_url
                #if(self.GetExistResultBtn(bsUtility)):
                #    driverUtility.ResultClick(self.posConfig)
                #    self.RandomWaitPoint(7, 9)
                #    self.RandomWaitPoint(7, 9)
                if(self.configQuest['action_mode'] == '0'):
                    turn = self.GetNowTurn(bsUtility)
                    if('Turn'+ str(turn) in self.configQuest['QuestAction']):
                        nowTurnAction = self.configQuest['QuestAction']['Turn'+ str(turn)]
                        i = 0
                        for ac in range(0, int(len(nowTurnAction)/2)):
                            if(nowTurnAction[i] != 0):
                                self.RandomWaitPoint()
                                driverUtility.CharaClick(self.posConfig, nowTurnAction[i])
                                i += 1
                                self.RandomWaitPoint()
                                driverUtility.AbilityClick(self.posConfig, nowTurnAction[i])
                                i += 1
                                self.RandomWaitPoint()
                                driverUtility.BackClick(self.posConfig)
                            else:
                                self.RandomWaitPoint(7, 9)
                elif(self.configQuest['action_mode'] == '1'):
                    turn = self.GetNowTurn(bsUtility)
                    if('Turn'+ str(turn) in self.configQuest['QuestAction']):
                        nowTurnAction = self.configQuest['QuestAction']['Turn'+ str(turn)]
                        i = 0
                        for ac in range(0, int(len(nowTurnAction)/2)):
                            if(i != 0):
                                driverUtility.BackClick(self.posConfig)
                                self.RandomWaitPoint()                       
                            driverUtility.CharaClick(self.posConfig, nowTurnAction[i])
                            i += 1
                            self.RandomWaitPoint(1, 2)
                            driverUtility.AbilityClick(self.posConfig, nowTurnAction[i])
                            i += 1
                            self.RandomWaitPoint(1, 2)
                        
                        driverUtility.ReloadClick()
                        self.RandomWaitPoint()
                        if(self.CheckNowPage('#raid')):
                            driverUtility.AttackClick(self.posConfig)
                            self.RandomWaitPoint()
                        continue
                
                driverUtility.AttackClick(self.posConfig)
                self.RandomWaitPoint(4, 6)
                driverUtility.ReloadClick()
                self.RandomWaitPoint()

            elif(self.CheckNowPage('#result')):
                break
            else:
                self.RandomWaitPoint()
                if(self.CheckNowPage('#result') or self.CheckNowPage('#raid')):
                    continue           
                if(self.GetExistRestartTxt(bsUtility)):
                    driverUtility.RestartClick(self.posConfig)
                self.RandomWaitPoint(6,8)
                if(self.CheckNowPage('#result') or self.CheckNowPage('#raid')):
                    continue
                self.logController.InfoLog('不明なページに遷移しています。')
                self.Exit()

    # 現在いるページを判定
    def CheckNowPage(self, key):
        if(re.search(key, self.driver.current_url)):
            return True
        else:
            return False

    def Exit(self):
        self.logController.EndLog(self.count)
        self.driver.quit()
        sys.exit(0)

    # クエスト実行部
    def Run(self):
        driverUtility = DriverUtility(self.driver)
        for i in range(5):
            if(i == 4):
                self.Exit()
            self.SelectSummon(driverUtility) 
            if(not self.CheckNowPage('#raid')):
                continue

            break

        self.RunQuest(driverUtility)

    def Main(self):
        self.initialize()
        self.logController.StartLog(self.generalConfig['run_time'])
        # guard: BOT検知回避
        if(self.driver.execute_script('return navigator.webdriver')):
            self.logController.InfoLog('navigator.webdriver:Trueのため')
            self.Exit()

        # MainLoop
        while(True):
            try:
                # クエスト実行
                self.Run()
                
                # 経過時間
                nowTime = time.time()
                nowSec = nowTime - self.startTime
                nomMinutes = int(nowSec/60)
                
                # 経過時間 >= 最大実行時間
                if(nomMinutes >= int(self.generalConfig['run_time'])):
                    self.Exit()
                # Waitさせて次のループへ
                self.count += 1
                self.RandomWait()
            except Exception as e:
                nowTime = time.time()
                nowSec = nowTime - self.startTime
                nomMinutes = int(nowSec/60)
                tb = sys.exc_info()[2]
                self.logController.ExceptLog(nomMinutes, str(e.with_traceback(tb)))
                self.Exit()

# エントリーポイント
if __name__ == '__main__':
    try:
        main = Main()
        main.Main()
    except Exception as e:
        tb = sys.exc_info()[2]
        self.logController.ExceptLog(0, str(e.with_traceback(tb)))
        self.Exit()
    
    nowTime = time.time()
    nowSec = nowTime - self.startTime
    nomMinutes = int(nowSec/60)
    self.logController.ExceptLog(0, str(e.with_traceback(tb)))
    self.Exit()
