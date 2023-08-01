import time
import datetime
import requests

class LogController():
    """description of class"""
    def __init__(self, profileName='' ,lineNotifyToken=''):
        self.f = None
        self.profileName = profileName
        self.lineNotifyToken = lineNotifyToken

    def Open(self):
        self.f = open('Log.txt', 'a', encoding='UTF-8')

    def Close(self):
        self.f.close()

    def WriteLines(self, Log):
        self.Open()
        self.f.write(Log + '\n')
        self.Close()
        if(self.lineNotifyToken != ''):
            self.sendLineNotify(Log)

    def StartLog(self, runTime):
        Log = '[%s]\nSTATE:START\nDETAILS:指定実行時間%s分' % (self.NowGetDate(), runTime)
        self.WriteLines(Log)

    def EndLog(self, num):
        Log = '[%s]\nSTATE:END\nDETAILS:実行回数%s回' % (self.NowGetDate(), num)
        self.WriteLines(Log)

    def ExceptLog(self, nowTime, exceptInfo):
        Log = '[%s]\nSTATE:EXCEPT\nDETAILS:経過時間%s分\nエラー内容:%s' % (self.NowGetDate(), nowTime, exceptInfo)
        self.WriteLines(Log)

    def InfoLog(self, Info):
        Log = '[%s]\nSTATE:INFO\nDETAILS:%s' % (datetime.datetime.now(), Info)
        self.WriteLines(Log)

    def sendLineNotify(self, Log):
        line_notify_api = 'https://notify-api.line.me/api/notify'
        headers = {'Authorization': 'Bearer %s' % self.lineNotifyToken}
        data = {'message':' PROFILE_NAME:%s\n%s' % (self.profileName, Log)}
        requests.post(line_notify_api, headers = headers, data = data)

    def NowGetDate(self):
        return datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")

